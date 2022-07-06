using System;
using System.Net;
using System.IO;
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
    class RequestHandler
    {
        public RequestHandler(HttpListenerContext context)
        {
            _context = context;
        }

        HttpListenerContext _context;
        string requested_file_name;

        public void HandleRequest()
        {
           requested_file_name = _context.Request.RawUrl.Split('/')[_context.Request.RawUrl.Split('/').Length-1];
            Console.WriteLine(requested_file_name);
            try
            {
                byte[] resoponse_byte_array = File.ReadAllBytes(@"/home/decertis/Projects/WebShop/WebShop/resources/" + requested_file_name);

                using (Stream output = _context.Response.OutputStream)
                {
                    output.Write(resoponse_byte_array,0,resoponse_byte_array.Length);
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File : \"" + requested_file_name + "\" was not found.");
            }
        }

    }
}
