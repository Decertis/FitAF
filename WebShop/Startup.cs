using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace WebShop
{
    class Startup
    {
        public static void Main(string[] args)
        {
            Listen();

        }
        static void Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8888/");
            listener.Start();
            while (true)
            {
                var _context = listener.GetContext();

                Console.WriteLine("Request Url : " + _context.Request.Url);
                RequestHandler requestHandler = new RequestHandler(_context);
                Task task = new Task(requestHandler.HandleRequest);
                task.Start();
            }
        }
    }
}
