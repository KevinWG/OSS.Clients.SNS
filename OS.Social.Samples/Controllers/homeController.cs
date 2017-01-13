using System.Web.Mvc;

namespace OS.Social.Samples.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index()
        {
            return Content("social项目示例");
        }
    }
}