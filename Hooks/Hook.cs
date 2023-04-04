using NUnit.Framework;
using PlaywrightWithSpecflowIntegration.Drivers;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)]
namespace PlaywrightWithSpecflowIntegration.Hooks;

[Binding, Scope(Tag = "regression")]
public class Hook
{
    private readonly ScenarioContext _scenarioContext;
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

    public Hook(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
    {
        _scenarioContext = scenarioContext;
        _specFlowOutputHelper = outputHelper;
    }

    [BeforeTestRun]
    public static void RemovePreviousTraceFiles()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        foreach (string sFile in System.IO.Directory.GetFiles(currentDirectory, "*.zip"))
        {
            System.IO.File.Delete(sFile);
        }
    }

    [AfterScenario]
    public async Task ShowTestStatus(Driver driver)
    {
        await driver.Dispose();
        _specFlowOutputHelper.AddAttachment(driver.GetFilePath());
    }
}
