// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingService.cs" company="">
//   
// </copyright>
// Developer: Marcus Kempf
// <summary>
//   The SettingService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlazorTutorial.Core.Services
{
    using Microsoft.Extensions.Configuration;

    /// <summary> The SettingService interface. </summary>
    public interface ISettingService
    {
        /// <summary>The get value. </summary>
        /// <param name="name">The name. </param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="Task"/>. </returns>
        public T GetValue<T>(string name);
    }

    /// <summary> The setting service. </summary>
    public class SettingService : ISettingService
    {
        /// <summary> The _config. </summary>
        private readonly IConfiguration _config;

        /// <summary>Initializes a new instance of the <see cref="SettingService"/> class. </summary>
        /// <param name="config">The config. </param>
        public SettingService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>The get value. </summary>
        /// <param name="name">The name. </param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="Task"/>. </returns>
        public T GetValue<T>(string name)
        {
            return _config.GetValue<T>(name);
        }
    }
}