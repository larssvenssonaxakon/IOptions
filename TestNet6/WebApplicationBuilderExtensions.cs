using Microsoft.Extensions.DependencyInjection;

namespace TestNet6
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure appsettings and return instance.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sectionName"></param>
        /// <returns>AppSettings</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T AddAppSettingsSection<T>(this WebApplicationBuilder builder, string sectionName) where T : class
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var appSettingsSection = builder.Configuration.GetSection(sectionName);
            builder.Services.Configure<T>(appSettingsSection);
            var appSettings = appSettingsSection.Get<T>();

            if (appSettings == null) throw new ArgumentException(nameof(appSettings));

            return appSettings;
        }
    }
}
