using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        static async Task Main(string[] args)
        {
            var path = Environment.GetFolderPath( Environment.SpecialFolder.Desktop ) + "\\XDocument.xml";

            //------------------------------
            /* Export data as an XML file  */
            //------------------------------
            //var expo = new Exporter();
            //expo.Export( Orders , path , "Orders" , "Order" , DataInsertionType.Attributes );

            //Console.WriteLine( "Done!" );




            var importer     = new Importer();

            //---------------------------------------------------
            /* Import XML data from local XML file || EXAMPLE 1 */
            //---------------------------------------------------
            //var importedDocument = await importer.ImportAsync( path );
            //var selectedData = importedDocument.Root.Elements( "Order" ).Select( x => new
            //                                                                        {
            //                                                                            Id = x.Attribute( "Id" ).Value ,
            //                                                                            Client = x.Attribute( "Client" ).Value ,
            //                                                                            Product = x.Attribute( "Product" ).Value ,
            //                                                                            Total = x.Attribute( "Total" ).Value
            //                                                                        } );

            //foreach (var order in selectedData )
            //{
            //    Console.WriteLine( $"{order.Id}, {order.Client}, {order.Product}, {order.Total}" );
            //}

            //---------------------------------------------------
            /* Import XML data from local XML file || EXAMPLE 2 */
            //---------------------------------------------------
            //var importedData = await importer.ImportAsync<Order>( path , DataSelectionType.Attributes );

            //foreach (var order in importedData)
            //{
            //    Console.WriteLine( $"{order.Id}, {order.Client}, {order.Product}, {order.Total}" );
            //}  
            
            //---------------------------------------------------
            /* Import XML data from local XML file || EXAMPLE 3 */
            //---------------------------------------------------
            //var webPath      = "https://ia601501.us.archive.org/12/items/XDocument/XDocument.xml";
            //var importedData = await importer.ImportAsync<Order>( webPath , DataSelectionType.Attributes );

            //foreach (var order in importedData)
            //{
            //    Console.WriteLine( $"{order.Id}, {order.Client}, {order.Product}, {order.Total}" );
            //}
        }
    }
}
