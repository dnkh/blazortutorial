using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// Credits to: https://stackoverflow.com/questions/54024644/how-do-i-create-a-cookie-client-side-using-blazor
namespace BlazorTutorial.Core.Services
{
    public interface ICookieService
    {
        public Task SetValue(string key, string value, int? days = null);

        public Task<string> GetValue(string key, string def = "");
    }

    public class CookieService : ICookieService
    {
        readonly IJSRuntime JSRuntime;

        string expires = "";

        public CookieService(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
            ExpireDays = 300;
        }

        public async Task SetValue(string key, string value, int? days = null)
        {
            var curExp = (days != null) ? (days > 0 ? DateToUTC(days.Value) : "") : expires;

            await SetCookie($"{key}={value}; expires={curExp}; path=/");
        }

        public async Task<string> GetValue(string key, string def = "")
        {
            var cValue = await GetCookie();

            if (string.IsNullOrEmpty(cValue))
            {
                return def;
            }

            var vals = cValue.Split(';');

            foreach (var val in vals)
            {
                if (!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
                {
                    var name = val.Substring(0, val.IndexOf('=')).Trim();

                    if (name.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        return val.Substring(val.IndexOf('=') + 1);
                    }
                }
            }

            return def;
        }

        private async Task SetCookie(string value)
        {
            await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
        }

        private async Task<string> GetCookie()
        {
            return await JSRuntime.InvokeAsync<string>("eval", $"document.cookie");
        }

        public int ExpireDays
        {
            set => expires = DateToUTC(value);
        }

        private static string DateToUTC(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
    }
}
