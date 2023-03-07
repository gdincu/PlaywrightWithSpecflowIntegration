
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightWithSpecflowIntegration.StepDefinitions
{
    [Binding]
    public class APITestingStepDefinitions
    {
        private string? _response;

        [Given(@"I send a get request to (.*)")]
        public async Task GivenISendAGetRequest(string url)
        {
            var playwright = await Playwright.CreateAsync();

            var requestContext = await playwright.APIRequest.NewContextAsync();

            var response = await requestContext.GetAsync(url);
        
            this._response = response.ToString();
        }

        [When(@"I receive a response")]
        public void WhenIReceiveAResponse()
        {
            throw new PendingStepException();
        }

        [Then(@"it contains (.*)")]
        public async void ThenItContainsCapsule_Serial(string response)
        {
            Assert.That(_response, Does.Contain(response));
        }
    }
}
