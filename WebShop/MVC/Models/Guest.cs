using System;
namespace WebShop.MVC.Models
{

public class Guest : Client
    {

        public Guest(string client_ip) : base(client_ip)
        {

        }
}

}