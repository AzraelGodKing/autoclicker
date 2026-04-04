namespace AutoClicker;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    private System.Windows.Forms.Timer clickTimer;
    private Label intervalLabel;
    private NumericUpDown intervalNumericUpDown;
    private Label jitterLabel;
    private NumericUpDown jitterNumericUpDown;
    private Label mouseButtonLabel;
    private ComboBox mouseButtonCombo;
    private Label modeLabel;
    private RadioButton modeClickRadio;
    private RadioButton modeHoldRadio;
    private CheckBox fixedPositionCheckBox;
    private Button setPositionButton;
    private Label positionLabel;
    private Label hotkeyPickLabel;
    private ComboBox hotkeyCombo;
    private CheckBox minimizeToTrayCheckBox;
    private Button startButton;
    private Button stopButton;
    private Label statusLabel;
    private Label hotkeyStatusLabel;
    private NotifyIcon trayIcon;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        clickTimer = new System.Windows.Forms.Timer(components);
        intervalLabel = new Label();
        intervalNumericUpDown = new NumericUpDown();
        jitterLabel = new Label();
        jitterNumericUpDown = new NumericUpDown();
        mouseButtonLabel = new Label();
        mouseButtonCombo = new ComboBox();
        modeLabel = new Label();
        modeClickRadio = new RadioButton();
        modeHoldRadio = new RadioButton();
        fixedPositionCheckBox = new CheckBox();
        setPositionButton = new Button();
        positionLabel = new Label();
        hotkeyPickLabel = new Label();
        hotkeyCombo = new ComboBox();
        minimizeToTrayCheckBox = new CheckBox();
        startButton = new Button();
        stopButton = new Button();
        statusLabel = new Label();
        hotkeyStatusLabel = new Label();
        trayIcon = new NotifyIcon(components);
        ((System.ComponentModel.ISupportInitialize)intervalNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)jitterNumericUpDown).BeginInit();
        SuspendLayout();
        //
        // intervalLabel
        //
        intervalLabel.AutoSize = true;
        intervalLabel.Location = new Point(16, 18);
        intervalLabel.Name = "intervalLabel";
        intervalLabel.Size = new Size(95, 15);
        intervalLabel.TabIndex = 0;
        intervalLabel.Text = "Interval (ms):";
        //
        // intervalNumericUpDown
        //
        intervalNumericUpDown.Location = new Point(132, 16);
        intervalNumericUpDown.Maximum = new decimal(new int[] { 3600000, 0, 0, 0 });
        intervalNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        intervalNumericUpDown.Name = "intervalNumericUpDown";
        intervalNumericUpDown.Size = new Size(100, 23);
        intervalNumericUpDown.TabIndex = 1;
        intervalNumericUpDown.Value = new decimal(new int[] { 500, 0, 0, 0 });
        intervalNumericUpDown.ValueChanged += intervalNumericUpDown_ValueChanged;
        //
        // jitterLabel
        //
        jitterLabel.AutoSize = true;
        jitterLabel.Location = new Point(16, 50);
        jitterLabel.Name = "jitterLabel";
        jitterLabel.Size = new Size(110, 15);
        jitterLabel.TabIndex = 2;
        jitterLabel.Text = "Jitter max (+ms):";
        //
        // jitterNumericUpDown
        //
        jitterNumericUpDown.Location = new Point(132, 48);
        jitterNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        jitterNumericUpDown.Name = "jitterNumericUpDown";
        jitterNumericUpDown.Size = new Size(100, 23);
        jitterNumericUpDown.TabIndex = 3;
        jitterNumericUpDown.ValueChanged += jitterNumericUpDown_ValueChanged;
        //
        // mouseButtonLabel
        //
        mouseButtonLabel.AutoSize = true;
        mouseButtonLabel.Location = new Point(16, 82);
        mouseButtonLabel.Name = "mouseButtonLabel";
        mouseButtonLabel.Size = new Size(86, 15);
        mouseButtonLabel.TabIndex = 4;
        mouseButtonLabel.Text = "Mouse button:";
        //
        // mouseButtonCombo
        //
        mouseButtonCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        mouseButtonCombo.FormattingEnabled = true;
        mouseButtonCombo.Items.AddRange(new object[] { "Left", "Right", "Middle" });
        mouseButtonCombo.Location = new Point(132, 79);
        mouseButtonCombo.Name = "mouseButtonCombo";
        mouseButtonCombo.Size = new Size(100, 23);
        mouseButtonCombo.TabIndex = 5;
        //
        // modeLabel
        //
        modeLabel.AutoSize = true;
        modeLabel.Location = new Point(16, 114);
        modeLabel.Name = "modeLabel";
        modeLabel.Size = new Size(41, 15);
        modeLabel.TabIndex = 6;
        modeLabel.Text = "Mode:";
        //
        // modeClickRadio
        //
        modeClickRadio.AutoSize = true;
        modeClickRadio.Checked = true;
        modeClickRadio.Location = new Point(132, 112);
        modeClickRadio.Name = "modeClickRadio";
        modeClickRadio.Size = new Size(51, 19);
        modeClickRadio.TabIndex = 7;
        modeClickRadio.TabStop = true;
        modeClickRadio.Text = "Click";
        modeClickRadio.UseVisualStyleBackColor = true;
        //
        // modeHoldRadio
        //
        modeHoldRadio.AutoSize = true;
        modeHoldRadio.Location = new Point(200, 112);
        modeHoldRadio.Name = "modeHoldRadio";
        modeHoldRadio.Size = new Size(52, 19);
        modeHoldRadio.TabIndex = 8;
        modeHoldRadio.Text = "Hold";
        modeHoldRadio.UseVisualStyleBackColor = true;
        //
        // fixedPositionCheckBox
        //
        fixedPositionCheckBox.AutoSize = true;
        fixedPositionCheckBox.Location = new Point(16, 144);
        fixedPositionCheckBox.Name = "fixedPositionCheckBox";
        fixedPositionCheckBox.Size = new Size(95, 19);
        fixedPositionCheckBox.TabIndex = 9;
        fixedPositionCheckBox.Text = "Fixed position";
        fixedPositionCheckBox.UseVisualStyleBackColor = true;
        //
        // setPositionButton
        //
        setPositionButton.Location = new Point(132, 140);
        setPositionButton.Name = "setPositionButton";
        setPositionButton.Size = new Size(120, 26);
        setPositionButton.TabIndex = 10;
        setPositionButton.Text = "Capture position";
        setPositionButton.UseVisualStyleBackColor = true;
        setPositionButton.Click += setPositionButton_Click;
        //
        // positionLabel
        //
        positionLabel.AutoSize = true;
        positionLabel.Location = new Point(260, 145);
        positionLabel.Name = "positionLabel";
        positionLabel.Size = new Size(96, 15);
        positionLabel.TabIndex = 11;
        positionLabel.Text = "Captured: (none)";
        //
        // hotkeyPickLabel
        //
        hotkeyPickLabel.AutoSize = true;
        hotkeyPickLabel.Location = new Point(16, 178);
        hotkeyPickLabel.Name = "hotkeyPickLabel";
        hotkeyPickLabel.Size = new Size(78, 15);
        hotkeyPickLabel.TabIndex = 12;
        hotkeyPickLabel.Text = "Toggle hotkey:";
        //
        // hotkeyCombo
        //
        hotkeyCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        hotkeyCombo.FormattingEnabled = true;
        hotkeyCombo.Items.AddRange(new object[]
        {
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
        });
        hotkeyCombo.Location = new Point(132, 175);
        hotkeyCombo.Name = "hotkeyCombo";
        hotkeyCombo.Size = new Size(60, 23);
        hotkeyCombo.TabIndex = 13;
        //
        // minimizeToTrayCheckBox
        //
        minimizeToTrayCheckBox.AutoSize = true;
        minimizeToTrayCheckBox.Location = new Point(16, 208);
        minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
        minimizeToTrayCheckBox.Size = new Size(112, 19);
        minimizeToTrayCheckBox.TabIndex = 14;
        minimizeToTrayCheckBox.Text = "Minimize to tray";
        minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
        //
        // startButton
        //
        startButton.Location = new Point(16, 240);
        startButton.Name = "startButton";
        startButton.Size = new Size(100, 28);
        startButton.TabIndex = 15;
        startButton.Text = "Start";
        startButton.UseVisualStyleBackColor = true;
        startButton.Click += startButton_Click;
        //
        // stopButton
        //
        stopButton.Enabled = false;
        stopButton.Location = new Point(132, 240);
        stopButton.Name = "stopButton";
        stopButton.Size = new Size(100, 28);
        stopButton.TabIndex = 16;
        stopButton.Text = "Stop";
        stopButton.UseVisualStyleBackColor = true;
        stopButton.Click += stopButton_Click;
        //
        // statusLabel
        //
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(16, 284);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(89, 15);
        statusLabel.TabIndex = 17;
        statusLabel.Text = "Status: Stopped";
        //
        // hotkeyStatusLabel
        //
        hotkeyStatusLabel.AutoSize = true;
        hotkeyStatusLabel.Location = new Point(16, 308);
        hotkeyStatusLabel.Name = "hotkeyStatusLabel";
        hotkeyStatusLabel.Size = new Size(115, 15);
        hotkeyStatusLabel.TabIndex = 18;
        hotkeyStatusLabel.Text = "Hotkey: F6 (global)";
        //
        // trayIcon
        //
        trayIcon.Text = "AutoClicker";
        trayIcon.Visible = false;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(404, 344);
        Controls.Add(hotkeyStatusLabel);
        Controls.Add(statusLabel);
        Controls.Add(stopButton);
        Controls.Add(startButton);
        Controls.Add(minimizeToTrayCheckBox);
        Controls.Add(hotkeyCombo);
        Controls.Add(hotkeyPickLabel);
        Controls.Add(positionLabel);
        Controls.Add(setPositionButton);
        Controls.Add(fixedPositionCheckBox);
        Controls.Add(modeHoldRadio);
        Controls.Add(modeClickRadio);
        Controls.Add(modeLabel);
        Controls.Add(mouseButtonCombo);
        Controls.Add(mouseButtonLabel);
        Controls.Add(jitterNumericUpDown);
        Controls.Add(jitterLabel);
        Controls.Add(intervalNumericUpDown);
        Controls.Add(intervalLabel);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "AutoClicker";
        ((System.ComponentModel.ISupportInitialize)intervalNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)jitterNumericUpDown).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
