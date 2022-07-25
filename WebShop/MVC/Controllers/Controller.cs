using System.Net;
using System.Text;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public abstract class Controller
    {
        protected HttpListenerContext _context;
        public Controller(HttpListenerContext context)
        {
            this._context = context;
            Client client = new Guest(_context.Request.UserHostAddress);
        }
        public virtual HttpListenerResponse GenerateHttpListenerResponse()
        {
            return _context.Response;
        }
        public virtual byte[] GenerateResponseContent()
        {
            return Encoding.UTF8.GetBytes("<h1>Vibe check passed</h1>");
        }

    }
}
