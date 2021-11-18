using System;
using System.Collections.Generic;
using MBXmlCore.Core;
using MBXmlCore.Enums;

namespace MBXmlCore.ClassicTest
{
    internal class Program
    {
        private static readonly List<Order> Orders = new()
                                                     {
                                                               new Order(1,  "Ennasiri Ali",    "PRD-1", 1500),
                                                               new Order(2,  "Badaoui Inas",    "PRD-1", 2000),
                                                               new Order(3,  "Baddouh Ali",     "PRD-3", 1000),
                                                               new Order(4,  "Mouslim Kawtar",  "PRD-2", 3500),
                                                               new Order(5,  "Essalmi Karim",   "PRD-1", 2000),
                                                               new Order(6,  "Nousayr Ahmed",   "PRD-1", 2000),
                                                               new Order(7,  "Mersaoui Fatima", "PRD-3", 1000),
                                                               new Order(8,  "Fanar Adil",      "PRD-1", 2200),
                                                               new Order(9,  "Eddawdi Nawal",   "PRD-2", 3200),
                                                               new Order(10, "Houmam Karim",    "PRD-1", 2400),
                                                               new Order(11, "Ennasiri Ali",    "PRD-2", 2000),
                                                               new Order(12, "Ennasiri Ali",    "PRD-3", 3500),
                                                               new Order(13, "Essalmi Karim",   "PRD-2", 1500),
                                                               new Order(14, "Eddawdi Nawal",   "PRD-1", 2000)
                                                           };

        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath( Environment.SpecialFolder.Desktop ) + "\\XDocument.xml";
            var expo = new Exporter();

            expo.Export( Orders , path , "Orders" , "Order" , DataInsertionType.Attributes );

            Console.WriteLine( "Done!" );
        }
    }
}
