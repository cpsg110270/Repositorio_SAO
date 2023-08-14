using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SAO.Pages;

public class Index_Tests : SAOWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
