
using Microsoft.Playwright;

namespace PlaywrightWithSpecflowIntegration.Drivers
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;

        public Driver()
        {
            _page = InitialisePlaywright(); 
        }
        public IPage Page => _page.Result;

        private async Task<IPage> InitialisePlaywright()
        {
            var playwright = await Playwright.CreateAsync();

            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
                //SlowMo = 2000
            });

            return await _browser.NewPageAsync();
        }

        public void Dispose() => _browser?.CloseAsync();
 
    }
}
