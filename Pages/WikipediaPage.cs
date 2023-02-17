using Microsoft.Playwright;

namespace PlaywrightWithSpecflowIntegration.Pages
{
    internal class WikipediaPage
    {
        private IPage _page;

        public WikipediaPage(IPage page) => _page = page;

        private ILocator _firstLink => _page.Locator("a").Nth(0);

        public async Task AccessANewPage()
        {
            await _firstLink.ClickAsync();
        }

        public async Task<string> GetTitle()
        {
            return await _page.TitleAsync();
        }
    }
}
