using NUnit.Framework;
using PlaywrightWithSpecflowIntegration.Drivers;
using PlaywrightWithSpecflowIntegration.Pages;
using TechTalk.SpecFlow.Assist;

namespace PlaywrightWithSpecflowIntegration.StepDefinitions
{
    [Binding]
    public class EAAppTestStepDefinitions
    {
        private readonly Driver _driver;
        private readonly LoginPage _loginPage;

        public EAAppTestStepDefinitions(Drivers.Driver driver)
        {
            _driver = driver;
            _loginPage = new LoginPage(_driver.Page);
        }

        [Given(@"I navigate to the app")]
        public void GivenINavigateToTheApp()
        {
            _driver.Page.GotoAsync("https://www.stealmylogin.com/demo.html");
        }

        [When(@"I enter the following login details")]
        public async Task WhenIEnterTheFollowingLoginDetails(Table table)
        {
            //Read the table dynamically
            dynamic data = table.CreateDynamicInstance();
            await _loginPage.Login((string)data.Username, (string)data.Password);
        }

        [Then(@"I get to examplecom")]
        public async Task ThenIGetToExamplecom()
        {
            var title = await _loginPage.GetTitle();
            Assert.True(title.Equals("Example Domain"));
        }
    }
}
