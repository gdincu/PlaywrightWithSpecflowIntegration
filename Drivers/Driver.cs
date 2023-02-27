
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightWithSpecflowIntegration.Drivers
{
    public class Driver
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

            var context = await _browser.NewContextAsync();

            // Start tracing before creating / navigating a page.
            await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            return await context.NewPageAsync();
        }

        public async Task Dispose()
        {
            IBrowserContext context = _browser.Contexts.FirstOrDefault<IBrowserContext>();

            //Only records a trace file when a test fails
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                await context.Tracing.StopAsync();
                return;
            }
            else
            {
                await context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = TestContext.CurrentContext.Test.MethodName + ".zip"
                });
            }

            await context.CloseAsync();

            await _browser?.CloseAsync();

        }

        public string GetFilePath()
        {
            return TestContext.CurrentContext.Test.MethodName + ".zip";
        }

    }
}
