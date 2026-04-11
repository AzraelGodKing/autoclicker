# AutoClicker

Windows desktop app that automates mouse buttons at the cursor (or a **captured screen position**), on a timer with optional **random jitter**, or in **hold** mode. Use **Start** / **Stop** or a global **F1–F12** hotkey (default **F6**). Settings are saved under `%LocalAppData%\AutoClicker\settings.json`.

## Requirements

- Windows
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Build and run

From this folder:

```powershell
dotnet run
```

Release build:

```powershell
dotnet build -c Release
```

The executable is under `bin\Release\net8.0-windows\AutoClicker.exe`.

### Optional: single-file publish

```powershell
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

Output is under `bin\Release\net8.0-windows\win-x64\publish\`.

## Features

| Option | Description |
|--------|-------------|
| **Interval (ms)** | Base delay between clicks in **Click** mode (1–3,600,000). |
| **Jitter max (+ms)** | Each interval adds random **0…jitter** milliseconds (0 = fixed interval). |
| **Mouse button** | Left, right, or middle. |
| **Mode: Click** | Repeated down/up at the interval (with jitter). |
| **Mode: Hold** | Presses the button down on Start and releases on Stop (no timer). |
| **Fixed position** | When enabled, moves the cursor to the captured point before each click or before hold. Click **Capture in 3s…**: you get a **3-second countdown**—move the mouse to the target before it hits zero (the app samples the cursor then, not when you press the button). Click **Cancel capture** to abort. While counting down, the toggle hotkey is ignored so you do not accidentally start clicking. |
| **Toggle hotkey** | Choose **F1–F12**; registers globally when possible. |
| **Minimize to tray** | When checked, minimizing hides the window; use the tray icon (double-click or **Show**) to restore. |

## Usage

1. Choose options (interval, jitter, button, mode, optional fixed position and hotkey).
2. If **Fixed position** is on, click **Capture in 3s…**, then move the pointer to the exact spot before the countdown ends.
3. Press **Start** or the hotkey to begin; **Stop** or the hotkey to end.

If the hotkey cannot be registered, you will see a warning at startup and can still use **Start** / **Stop**.

## Disclaimer

Use this tool only where it is **allowed**: respect game terms of service, workplace policies, and applicable law. The authors are not responsible for misuse. Intended for learning and legitimate automation (for example testing or accessibility).

## Changelog

See [CHANGELOG.md](CHANGELOG.md).
