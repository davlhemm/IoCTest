
public class BaseOptions 
{
    public const string Base = "Base";

    public string Title     { get; set; }
    public string Name      { get; set; }
    public string Privacy   { get; set; }
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