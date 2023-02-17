using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
namespace PlaywrightWithSpecflowIntegration.Hooks
{
    [Binding]
    public class Hook
    {
    }
}
