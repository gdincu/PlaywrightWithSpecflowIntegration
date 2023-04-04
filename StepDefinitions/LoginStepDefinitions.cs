using NUnit.Framework;
using PlaywrightWithSpecflowIntegration.Drivers;
using PlaywrightWithSpecflowIntegration.Pages;

namespace PlaywrightWithSpecflowIntegration.StepDefinitions;

[Binding]
public class LoginStepDefinitions
{
    private readonly Driver _driver;
    private readonly LoginPage _loginPage;

    public LoginStepDefinitions(Drivers.Driver driver)
    {
        _driver = driver;
        _loginPage = new LoginPage(_driver.Page);
    }

    [Given(@"I navigate to the app")]
    public void GivenINavigateToTheApp()
    {
        _driver.Page.GotoAsync("https://www.stealmylogin.com/demo.html");
    }

    [When(@"I enter the following username: (.*) and password: (.*)")]
    public async Task WhenIEnterTheFollowingLoginDetails(string Username,string Password)
    {
        await _loginPage.Login(Username, Password);
    }

    [Then(@"I get to examplecom")]
    public async Task ThenIGetToExamplecom()
    {
        var title = await _loginPage.GetTitle();
        Assert.True(title.Equals("Example Domain"));
    }
}
