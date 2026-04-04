# Changelog

All notable changes to this project are documented in this file.

## [Unreleased]

### Added

- **Mouse button** choice: left, right, or middle (`SendInput`).
- **Jitter**: optional random 0…N ms added to each click interval.
- **Modes**: **Click** (repeated clicks) and **Hold** (button down until Stop).
- **Fixed position**: capture screen coordinates; optionally move the cursor there before each action.
- **Toggle hotkey** picker (**F1–F12**), with re-registration when the selection changes.
- **Minimize to tray** with tray menu (**Show** / **Exit**) and double-click to restore.
- **Settings persistence** to `%LocalAppData%\AutoClicker\settings.json` (JSON).

### Changed

- Startup hotkey default remains **F6** but is now configurable in the UI.

## [0.1.0]

### Added

- Initial WinForms MVP: configurable click interval (ms), left click at current cursor via `SendInput`.
- **Start** / **Stop** controls and **F6** global hotkey toggle (`RegisterHotKey` / `WM_HOTKEY`).
- Warning when the hotkey cannot be registered; in-window controls still work.
- `.gitignore` for `bin/`, `obj/`, and common Visual Studio junk files.
