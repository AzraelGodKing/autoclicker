using System.Runtime.InteropServices;

namespace AutoClicker;

public partial class MainForm : Form
{
    private const int HotKeyId = 1;
    private const int WM_HOTKEY = 0x0312;
    private const uint ModNorepeat = 0x4000;
    private const uint VkF1 = 0x70;
    private const int CaptureCountdownSeconds = 3;

    private bool _hotkeyRegistered;
    private bool _holdRunning;
    private Point? _fixedPoint;
    private bool _captureCountdownActive;
    private int _captureSecondsRemaining;

    public MainForm()
    {
        InitializeComponent();
        trayIcon.Icon = (Icon)SystemIcons.Application.Clone();
        trayIcon.DoubleClick += (_, _) => RestoreFromTray();
        var trayMenu = new ContextMenuStrip();
        trayMenu.Items.Add("Show", null, (_, _) => RestoreFromTray());
        trayMenu.Items.Add("Exit", null, (_, _) => Application.Exit());
        trayIcon.ContextMenuStrip = trayMenu;

        clickTimer.Tick += ClickTimer_Tick;
        captureCountdownTimer.Tick += CaptureCountdownTimer_Tick;
        modeClickRadio.CheckedChanged += (_, _) => UpdateUiState();
        modeHoldRadio.CheckedChanged += (_, _) => UpdateUiState();
        hotkeyCombo.SelectedIndexChanged += (_, _) => ReregisterHotKey();
        fixedPositionCheckBox.CheckedChanged += (_, _) =>
        {
            if (!fixedPositionCheckBox.Checked)
                CancelCaptureCountdown();
            UpdateUiState();
        };

        ApplySettings(AppSettingsStore.Load());
    }

    private void ApplySettings(AppSettings s)
    {
        intervalNumericUpDown.Value = Math.Clamp(s.IntervalMs, intervalNumericUpDown.Minimum, intervalNumericUpDown.Maximum);
        jitterNumericUpDown.Value = Math.Clamp(s.JitterMaxMs, jitterNumericUpDown.Minimum, jitterNumericUpDown.Maximum);
        mouseButtonCombo.SelectedIndex = Math.Clamp(s.MouseButton, 0, mouseButtonCombo.Items.Count - 1);
        if (s.HoldMode)
            modeHoldRadio.Checked = true;
        else
            modeClickRadio.Checked = true;
        fixedPositionCheckBox.Checked = s.FixedPosition;
        _fixedPoint = s.FixedX is { } fx && s.FixedY is { } fy ? new Point(fx, fy) : null;
        hotkeyCombo.SelectedIndex = Math.Clamp(s.HotkeyFKeyIndex, 0, hotkeyCombo.Items.Count - 1);
        minimizeToTrayCheckBox.Checked = s.MinimizeToTray;
        RefreshPositionLabel();
    }

    private void SaveSettings()
    {
        AppSettingsStore.Save(new AppSettings
        {
            IntervalMs = intervalNumericUpDown.Value,
            JitterMaxMs = (int)jitterNumericUpDown.Value,
            MouseButton = mouseButtonCombo.SelectedIndex,
            HoldMode = modeHoldRadio.Checked,
            FixedPosition = fixedPositionCheckBox.Checked,
            FixedX = _fixedPoint?.X,
            FixedY = _fixedPoint?.Y,
            HotkeyFKeyIndex = hotkeyCombo.SelectedIndex,
            MinimizeToTray = minimizeToTrayCheckBox.Checked,
        });
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        TryRegisterHotKey(showWarningOnFailure: true);
        UpdateUiState();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        CancelCaptureCountdown();
        clickTimer.Stop();
        if (_holdRunning)
        {
            MouseInput.SendButtonUp(CurrentMouseButton());
            _holdRunning = false;
        }

        SaveSettings();
        if (_hotkeyRegistered)
            UnregisterHotKey(Handle, HotKeyId);
        base.OnFormClosing(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (minimizeToTrayCheckBox.Checked && WindowState == FormWindowState.Minimized)
        {
            Hide();
            trayIcon.Visible = true;
        }
    }

    private void RestoreFromTray()
    {
        Show();
        WindowState = FormWindowState.Normal;
        trayIcon.Visible = false;
        Activate();
    }

    private void TryRegisterHotKey(bool showWarningOnFailure)
    {
        if (!IsHandleCreated)
            return;

        if (_hotkeyRegistered)
        {
            UnregisterHotKey(Handle, HotKeyId);
            _hotkeyRegistered = false;
        }

        var vk = VkFromFKeyIndex(hotkeyCombo.SelectedIndex);
        _hotkeyRegistered = RegisterHotKey(Handle, HotKeyId, ModNorepeat, vk)
            || RegisterHotKey(Handle, HotKeyId, 0, vk);

        UpdateHotkeyStatusLabel();

        if (!_hotkeyRegistered && showWarningOnFailure)
        {
            MessageBox.Show(
                this,
                $"Could not register global hotkey {hotkeyCombo.Text}. Another app may be using it. Use Start and Stop in this window.",
                "Hotkey unavailable",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void ReregisterHotKey()
    {
        if (!IsHandleCreated)
            return;
        TryRegisterHotKey(showWarningOnFailure: false);
    }

    private static uint VkFromFKeyIndex(int index) => VkF1 + (uint)Math.Clamp(index, 0, 11);

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HotKeyId)
        {
            if (!_captureCountdownActive)
                ToggleRunning();
            return;
        }

        base.WndProc(ref m);
    }

    private void startButton_Click(object sender, EventArgs e) => Start();

    private void stopButton_Click(object sender, EventArgs e) => Stop();

    private void setPositionButton_Click(object sender, EventArgs e)
    {
        if (_captureCountdownActive)
        {
            CancelCaptureCountdown();
            return;
        }

        _captureCountdownActive = true;
        _captureSecondsRemaining = CaptureCountdownSeconds;
        positionLabel.Text = $"Move the mouse to the target… {_captureSecondsRemaining}";
        setPositionButton.Text = "Cancel capture";
        captureCountdownTimer.Start();
        UpdateUiState();
    }

    private void CaptureCountdownTimer_Tick(object? sender, EventArgs e)
    {
        _captureSecondsRemaining--;
        if (_captureSecondsRemaining > 0)
        {
            positionLabel.Text = $"Move the mouse to the target… {_captureSecondsRemaining}";
            return;
        }

        captureCountdownTimer.Stop();
        _captureCountdownActive = false;
        _fixedPoint = Cursor.Position;
        ResetCaptureButtonText();
        RefreshPositionLabel();
        UpdateUiState();
    }

    private void CancelCaptureCountdown()
    {
        if (!_captureCountdownActive)
            return;
        captureCountdownTimer.Stop();
        _captureCountdownActive = false;
        ResetCaptureButtonText();
        RefreshPositionLabel();
        UpdateUiState();
    }

    private void ResetCaptureButtonText() => setPositionButton.Text = "Capture in 3s…";

    private void intervalNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (clickTimer.Enabled)
            ApplyJitterInterval();
    }

    private void jitterNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (clickTimer.Enabled)
            ApplyJitterInterval();
    }

    private void ClickTimer_Tick(object? sender, EventArgs e)
    {
        if (!modeClickRadio.Checked)
            return;
        MoveToFixedIfNeeded();
        MouseInput.SendClick(CurrentMouseButton());
        ApplyJitterInterval();
    }

    private void ApplyJitterInterval()
    {
        var baseMs = Math.Max(1, (int)intervalNumericUpDown.Value);
        var jitter = (int)jitterNumericUpDown.Value;
        clickTimer.Interval = jitter > 0
            ? baseMs + Random.Shared.Next(0, jitter + 1)
            : baseMs;
    }

    private void MoveToFixedIfNeeded()
    {
        if (fixedPositionCheckBox.Checked && _fixedPoint is { } p)
            MouseInput.MoveCursorTo(p.X, p.Y);
    }

    private ClickMouseButton CurrentMouseButton() =>
        (ClickMouseButton)Math.Clamp(mouseButtonCombo.SelectedIndex, 0, 2);

    private void ToggleRunning()
    {
        if (clickTimer.Enabled || _holdRunning)
            Stop();
        else
            Start();
    }

    private void Start()
    {
        if (_captureCountdownActive)
            return;

        if (fixedPositionCheckBox.Checked && _fixedPoint is null)
        {
            MessageBox.Show(
                this,
                "Turn off fixed position, or use \"Capture in 3s…\" first (move the mouse to the spot before the countdown ends).",
                "Fixed position",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        if (modeHoldRadio.Checked)
        {
            MoveToFixedIfNeeded();
            MouseInput.SendButtonDown(CurrentMouseButton());
            _holdRunning = true;
            UpdateUiState();
            return;
        }

        ApplyJitterInterval();
        clickTimer.Start();
        UpdateUiState();
    }

    private void Stop()
    {
        if (_holdRunning)
        {
            MouseInput.SendButtonUp(CurrentMouseButton());
            _holdRunning = false;
        }

        clickTimer.Stop();
        UpdateUiState();
    }

    private void RefreshPositionLabel()
    {
        positionLabel.Text = _fixedPoint is { } p
            ? $"Captured: {p.X}, {p.Y}"
            : "Captured: (none)";
    }

    private void UpdateHotkeyStatusLabel()
    {
        var key = hotkeyCombo.Text;
        hotkeyStatusLabel.Text = _hotkeyRegistered
            ? $"Hotkey: {key} (global)"
            : $"Hotkey: unavailable ({key})";
    }

    private void UpdateUiState()
    {
        var running = clickTimer.Enabled || _holdRunning;
        var clickMode = modeClickRadio.Checked;
        var capturing = _captureCountdownActive;

        startButton.Enabled = !running && !capturing;
        stopButton.Enabled = running;
        statusLabel.Text = running ? "Status: Running" : "Status: Stopped";

        intervalNumericUpDown.Enabled = clickMode && !running && !capturing;
        intervalLabel.Enabled = clickMode;
        jitterNumericUpDown.Enabled = clickMode && !running && !capturing;
        jitterLabel.Enabled = clickMode;

        mouseButtonCombo.Enabled = !running && !capturing;
        mouseButtonLabel.Enabled = !running && !capturing;
        modeClickRadio.Enabled = !running && !capturing;
        modeHoldRadio.Enabled = !running && !capturing;
        modeLabel.Enabled = !running && !capturing;

        fixedPositionCheckBox.Enabled = !running && !capturing;
        setPositionButton.Enabled = !running && fixedPositionCheckBox.Checked;
        positionLabel.Enabled = true;

        hotkeyCombo.Enabled = !running && !capturing;
        hotkeyPickLabel.Enabled = !running && !capturing;
        minimizeToTrayCheckBox.Enabled = !running && !capturing;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
}
