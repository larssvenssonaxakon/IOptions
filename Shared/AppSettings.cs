using Newtonsoft.Json;

namespace TestNet6;


public class AppSettings
{
    public const string SectionName = nameof(AppSettings);

    public SubSettings? SubSettings {  get; set; }
    public Logging? Logging { get; set; }
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

public class SubSettings
{
    public const string SectionName = $"{nameof(AppSettings)}:{nameof(SubSettings)}";

    public string? MySubSetting { get; set; }
}