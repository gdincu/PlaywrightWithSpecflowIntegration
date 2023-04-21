using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightWithSpecflowIntegration.Drivers;

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
            SlowMo = 2000,
            Headless = false,
            Args = new string[]
                                   {
                                        "--disable-gpu", "--disable-extensions", "--no-sandbox", "-incognito"
                                   }
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

        // Check if the context is not null before proceeding
        if (context != null)
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                await context.Tracing.StopAsync();
            }
            else
            {
                await context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = TestContext.CurrentContext.Test.MethodName + ".zip"
                });
            }

            // Close the browser context
            await context.CloseAsync();
        }

        await _browser.CloseAsync();
    }

    public string GetFilePath()
    {
        return TestContext.CurrentContext.Test.MethodName + ".zip";
    }

}
