using Newtonsoft.Json;

namespace TestNet6;

public abstract class AppSettingsBase : IAppSetting
{
    public abstract string SectionName();
}

public interface IAppSetting
{
    public string SectionName();
}

public class AppSettings : AppSettingsBase
{
    //public const string SectionName = nameof(AppSettings);

    public SubSettings? SubSettings {  get; set; }
    public Logging? Logging { get; set; }
    public override string SectionName()
    {
        return nameof(AppSettings);
       
    }
}

public class LogLevel
{
    public string? Default { get; set; }

    [JsonProperty("Microsoft.AspNetCore")]
    public string? MicrosoftAspNetCore { get; set; }
}

public class Logging
{
    public LogLevel? LogLevel { get; set; }
}

public class SubSettings : AppSettingsBase
{
    //public const string SectionName = nameof(SubSettings);

    public override string SectionName()
    {
        return nameof(SubSettings);
    }
    public string? MySubSetting { get; set; }
}