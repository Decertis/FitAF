using System;
using System.Net;
using System.Text;
using System.IO;

namespace WebShop.MVC.Controllers
{
    public class ResourcesController : Controller
    {
        public ResourcesController(HttpListenerContext context) : base(context) { }
        byte[] _file_to_attach;
        public override HttpListenerResponse GenerateHttpListenerResponse()
        {
            return _context.Response;
        }
        public void GetFile()
        {
            string requested_file_name = _context.Request.RawUrl.Split('/')[_context.Request.RawUrl.Split('/').Length - 1];
            Console.WriteLine("Requested file : " + requested_file_name);
            try
            {
                _file_to_attach = File.ReadAllBytes(@"/home/decertis/Projects/WebShop/WebShop/resources/" + requested_file_name);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(FileNotFoundException) || ex.GetType() == typeof(FieldAccessException))
                    Console.WriteLine("File : \"" + requested_file_name + "\" was not found.");
                _file_to_attach = Encoding.UTF8.GetBytes("<center><h1>404</h1></center>");
            }
        }
        public override byte[] GenerateResponseContent()
        {
            return _file_to_attach;
        }
    }
}
