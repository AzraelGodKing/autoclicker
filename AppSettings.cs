using System.Text.Json;

namespace AutoClicker;

internal sealed class AppSettings
{
    public decimal IntervalMs { get; set; } = 500;
    public int JitterMaxMs { get; set; }
    public int MouseButton { get; set; }
    public bool HoldMode { get; set; }
    public bool FixedPosition { get; set; }
    public int? FixedX { get; set; }
    public int? FixedY { get; set; }
    public int HotkeyFKeyIndex { get; set; } = 5;
    public bool MinimizeToTray { get; set; }
}

internal static class AppSettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    internal static string SettingsFilePath { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AutoClicker",
        "settings.json");

    internal static AppSettings Load()
    {
        try
        {
            if (!File.Exists(SettingsFilePath))
                return new AppSettings();
            var json = File.ReadAllText(SettingsFilePath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    internal static void Save(AppSettings settings)
    {
        try
        {
            var dir = Path.GetDirectoryName(SettingsFilePath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings, JsonOptions));
        }
        catch
        {
            // ignore persistence failures
        }
    }
}
