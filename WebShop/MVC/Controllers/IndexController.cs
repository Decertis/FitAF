using System.Net;
using System.Text;

namespace WebShop.MVC.Controllers
{
    public class IndexController : Controller
    {
        public IndexController(HttpListenerContext context) : base(context) { }

        public override HttpListenerResponse GenerateHttpListenerResponse()
        {
            return _context.Response;
        }
        public override byte[] GenerateResponseContent()
        {
            return Encoding.UTF8.GetBytes("<center><h1>Index page</h1></center>");
        }
    }
}
