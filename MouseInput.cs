using System.Runtime.InteropServices;

namespace AutoClicker;

internal enum ClickMouseButton
{
    Left,
    Right,
    Middle,
}

internal static class MouseInput
{
    private const uint InputMouse = 0;
    private const uint MouseEventfLeftDown = 0x0002;
    private const uint MouseEventfLeftUp = 0x0004;
    private const uint MouseEventfRightDown = 0x0008;
    private const uint MouseEventfRightUp = 0x0010;
    private const uint MouseEventfMiddleDown = 0x0020;
    private const uint MouseEventfMiddleUp = 0x0040;

    internal static void SendClick(ClickMouseButton button)
    {
        SendButtonDown(button);
        SendButtonUp(button);
    }

    internal static void SendButtonDown(ClickMouseButton button)
    {
        SendInput(1, new[] { MakeMouseInput(FlagsForDown(button)) }, Marshal.SizeOf<INPUT>());
    }

    internal static void SendButtonUp(ClickMouseButton button)
    {
        SendInput(1, new[] { MakeMouseInput(FlagsForUp(button)) }, Marshal.SizeOf<INPUT>());
    }

    internal static void MoveCursorTo(int x, int y) => _ = SetCursorPos(x, y);

    private static uint FlagsForDown(ClickMouseButton button) => button switch
    {
        ClickMouseButton.Left => MouseEventfLeftDown,
        ClickMouseButton.Right => MouseEventfRightDown,
        ClickMouseButton.Middle => MouseEventfMiddleDown,
        _ => MouseEventfLeftDown,
    };

    private static uint FlagsForUp(ClickMouseButton button) => button switch
    {
        ClickMouseButton.Left => MouseEventfLeftUp,
        ClickMouseButton.Right => MouseEventfRightUp,
        ClickMouseButton.Middle => MouseEventfMiddleUp,
        _ => MouseEventfLeftUp,
    };

    private static INPUT MakeMouseInput(uint dwFlags)
    {
        return new INPUT
        {
            type = InputMouse,
            Union = new InputUnion
            {
                mi = new MOUSEINPUT
                {
                    dx = 0,
                    dy = 0,
                    mouseData = 0,
                    dwFlags = dwFlags,
                    time = 0,
                    dwExtraInfo = UIntPtr.Zero,
                },
            },
        };
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        internal int dx;
        internal int dy;
        internal uint mouseData;
        internal uint dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        internal uint type;
        internal InputUnion Union;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)] internal MOUSEINPUT mi;
    }
}
