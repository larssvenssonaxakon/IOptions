namespace TestNet6
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure appsettings and return instance.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>AppSettings</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AppSettings AddAppSettings(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var appSettingsSection = builder.Configuration.GetSection(AppSettings.SectionName);
            builder.Services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            if (appSettings == null) throw new ArgumentException(nameof(AppSettings));

            return appSettings;
        }
    }
}
