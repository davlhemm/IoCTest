
namespace IoCTest
{
    public class BaseOptions
    {
        public const string Base = "Base";

        public string Title { get; set; }
        public string Name { get; set; }
        public string Privacy { get; set; }
    }

    public class OtherOptions : BaseOptions
    {
        public const string Other = "Other";

        public string Item { get; set; }
    }

    public class SettingsOptions : OtherOptions
    {
        public const string Settings = "Settings";

        public string Test { get; set; }
    }

    public class TestingOptions : SettingsOptions
    {
        public const string Testing = "Testing";

        public string Dummy1 { get; set; }
        public string Dummy2 { get; set; }
        public string Dummy3 { get; set; }
        public string Dummy4 { get; set; }
        public string Dummy5 { get; set; }
    }

    public class FileLoggerOptions
    {
        public const string FileLogger = nameof(FileLogger);

        public string FileName { get; set; }
    }

    public class BackupOptions
    {
        public const string Backup = nameof(Backup);

        public string FileName { get; set; }
    }
}