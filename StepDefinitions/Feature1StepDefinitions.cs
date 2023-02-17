using NUnit.Framework;
using PlaywrightWithSpecflowIntegration.Drivers;
using PlaywrightWithSpecflowIntegration.Pages;
using System;
using TechTalk.SpecFlow;

namespace PlaywrightWithSpecflowIntegration.StepDefinitions
{
    [Binding]
    public class Feature1StepDefinitions
    {
        private readonly Driver _driver;
        private readonly WikipediaPage _wikipediaPage;

        public Feature1StepDefinitions(Driver driver)
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
            Assert.True(newTitle.Equals("Wikipedia, the free encyclopedia"));
        }
    }
}
