using NUnit.Framework;
using PlaywrightWithSpecflowIntegration.Drivers;
using PlaywrightWithSpecflowIntegration.Pages;

namespace PlaywrightWithSpecflowIntegration.StepDefinitions;

[Binding]
public class NewPageAccessedStepDefinitions
{
    private readonly Driver _driver;
    private readonly WikipediaPage _wikipediaPage;

    public NewPageAccessedStepDefinitions(Driver driver)
    {
        _driver = driver;
        _wikipediaPage = new WikipediaPage(_driver.Page);
    }

    [Given(@"I access the main Wikipedia page")]
    public void GivenIAccessTheMainWikipediaPage()
    {
        _driver.Page.GotoAsync("https://www.wikipedia.org/");
    }

    [When(@"I tap on the first article")]
    public async Task WhenITapOnTheFirstArticle()
    {
        await _wikipediaPage.AccessANewPage();
    }

    [Then(@"I get to a different Wikipedia page")]
    public async Task ThenIGetToADifferentWikipediaPage()
    {
        var newTitle = await _wikipediaPage.GetTitle();
        //Incorrect value used so that the test fails
        Assert.True(newTitle.Equals("Wikipediaaaa, the free encyclopedia"));
    }
}
