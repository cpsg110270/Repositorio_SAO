using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SAO.Web.Pages;

[Authorize]
public class IndexModel : SAOPageModel
{

}
