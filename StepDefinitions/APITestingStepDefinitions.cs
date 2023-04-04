using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.Json;

namespace PlaywrightWithSpecflowIntegration.StepDefinitions;

[Binding]
public class APITestingStepDefinitions
{
    private JsonElement? _responseJson;
    private string? AuthToken;

    [When(@"I send a (.*) request to (.*)")]
    public async Task GivenISendAGetRequest(string requestType,string url)
    {
        var playwright = await Playwright.CreateAsync();

        var requestContext = await playwright.APIRequest.NewContextAsync();

        switch(requestType)
        {
            case "get":
                var response = await requestContext.GetAsync(url);
                _responseJson = await response.JsonAsync();
                break;
            case "post":
                var response2 = await requestContext.PostAsync(url,new APIRequestContextOptions()
                {
                    DataObject = new
                    {
                        username = "admin",
                        password = "password123"
                    }
                });
                _responseJson = await response2.JsonAsync();
            break;
            default:
                break;
        }
        
    }

    [When(@"I retrieve the auth token")]
    public void WhenIRetrieveTheAuthToken()
    {
        AuthToken = _responseJson.Value.GetProperty("token").ToString();
    }

    [Then(@"the response contains (.*)")]
    public void ThenItContainsCapsule_Serial(string response)
    {
        Assert.That(_responseJson.ToString(), Does.Contain(response));
    }

    [Then(@"the token is (.*) characters long")]
    public void ThenTheTokenHasACertainLength(int tokenLength)
    {
        Assert.That(_responseJson.Value.GetProperty("token").ToString().Length,Is.EqualTo(tokenLength));
    }
}

public class Authenticate
{
    public string token { get;set; }
}
