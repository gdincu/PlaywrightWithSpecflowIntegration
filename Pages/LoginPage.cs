using Microsoft.Playwright;

namespace PlaywrightWithSpecflowIntegration.Pages
{
    internal class LoginPage
    {
        private IPage _page;

        public LoginPage(IPage page) => _page = page;

        private ILocator _txtUsername => _page.Locator("//input[@name=\"username\"]");
        private ILocator _txtPassword => _page.Locator("//input[@name=\"password\"]");
        private ILocator _btnLogin => _page.Locator("//input[@type=\"submit\"]");

        public async Task Login(string username, string password)
        {
            await _txtUsername.FillAsync(username);
            await _txtPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
        }

        public async Task<string> GetTitle()
        {
            return await _page.TitleAsync();
        }
    }
}
