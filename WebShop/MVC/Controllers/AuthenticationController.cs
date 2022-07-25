using System.Net;

namespace WebShop.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController(HttpListenerContext context) : base(context) { }

        public override HttpListenerResponse GenerateHttpListenerResponse()
        {
            return _context.Response;
        }
    }
}
