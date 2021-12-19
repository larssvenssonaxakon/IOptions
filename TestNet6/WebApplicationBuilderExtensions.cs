using Microsoft.Extensions.DependencyInjection;

namespace TestNet6
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure appsettings and return instance.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settingsSection"></param>
        /// <returns>AppSettings</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAppSetting AddAppSettingsSection<T>(this WebApplicationBuilder builder, T settingsSection) where T : AppSettingsBase
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var appSettingsSection = builder.Configuration.GetSection(settingsSection.SectionName());
            builder.Services.Configure<T>(appSettingsSection);
            var appSettings = appSettingsSection.Get<T>();

            if (appSettings == null) throw new ArgumentException(nameof(T));

            return appSettings;
        }
    }
}
