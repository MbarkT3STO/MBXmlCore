using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBXmlCore.ClassicTest
{
    internal class Order
    {
        public int    Id      { get; set; }
        public string Client  { get; set; }
        public string Product { get; set; }
        public int    Total   { get; set; }

        public Order(int id, string client, string product, int total)
        {
            Id      = id;
            Client  = client;
            Product = product;
            Total   = total;
        }

        public Order()
        {
            
        }

    }
}
