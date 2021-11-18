using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MBXmlCore.Enums;

namespace MBXmlCore.Core
{
    /// <summary>
    /// XML exporter middleware
    /// </summary>
    public class Exporter
    {
        #region Properties

        public string            Path              { get; set; }
        public string            RootElementName   { get; set; }
        public string            ChildElementsName { get; set; }
        public DataInsertionType DataInsertionType { get; set; }

        #endregion

        #region Constructors

        public Exporter()
        {
            
        }

        public Exporter(string path, string rootElementName, string childElementsName, DataInsertionType insertionType)
        {
            _SetProps( path , rootElementName , childElementsName , insertionType );
        }

        #endregion

        #region Private

        private void _SetProps(string path, string rootElementName, string childElementsName, DataInsertionType insertionType)
        {
            Path              = path;
            RootElementName   = rootElementName;
            ChildElementsName = childElementsName;
            DataInsertionType = insertionType;
        }       
        private void _SetProps(string path)
        {
            Path = path;
        }

        private void _Export<T>(IEnumerable<T> data)
        {
            if ( DataInsertionType == DataInsertionType.Elements )
                _ExportAsElements( data );
            else
                _ExportAsAttributes( data );
        }

        private void _ExportAsElements<T>(IEnumerable<T> data)
        {
            var dataProperties      = typeof(T).GetProperties();

            // Create the XDocument and add the root element to it
            var xDoc = new XDocument(new XElement(RootElementName));

            foreach (T obj in data)
            {
                xDoc.Root?.AddFirst(new XElement(ChildElementsName));

                foreach (var prop in dataProperties)
                    xDoc.Root?.Element( ChildElementsName )?.Add( new XElement( prop.Name , prop.GetValue( obj , null ) ) );
            }

            xDoc.Save(Path);
        }
        private void _ExportAsAttributes<T>(IEnumerable<T> data)
        {
            var dataProperties      = typeof(T).GetProperties();

            // Create the XDocument and add the root element to it
            var xDoc = new XDocument(new XElement(RootElementName));

            foreach (T obj in data)
            {
                xDoc.Root?.AddFirst(new XElement(ChildElementsName));

                foreach (var prop in dataProperties)
                    xDoc.Root?.Element( ChildElementsName )?.Add( new XAttribute( prop.Name , prop.GetValue( obj , null ) ) );
            }

            xDoc.Save(Path);
        }

        #endregion

        #region Public

        /// <summary>
        /// Export an <see cref="IEnumerable{T}"/> as an XML file
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="data">Data to be exported</param>
        public void Export<T>(IEnumerable<T> data)
        {
            _Export(data);
        }

        /// <inheritdoc cref = "Export{T}(System.Collections.Generic.IEnumerable{T})" />
        /// <returns><see cref="Task"/></returns>
        public Task ExportAsync<T>(IEnumerable<T> data)
        {
            return Task.Run(() => Export(data));
        }


        /// <summary>
        /// Export an <see cref="IEnumerable{T}"/> as an XML file
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="data">Data to be exported</param>
        /// <param name="path">File path</param>
        public void Export<T>(IEnumerable<T> data, string path)
        {
            _SetProps(path);

            _Export(data);
        }

        /// <inheritdoc cref = "Export{T}(System.Collections.Generic.IEnumerable{T},string)" />
        /// <returns><see cref="Task"/></returns>
        public Task ExportAsync<T>(IEnumerable<T> data, string path)
        {
            return Task.Run( () => Export( data , path ) );
        }


        /// <summary>
        /// Export an <see cref="IEnumerable{T}"/> as an XML file
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="data">Data to be exported</param>
        /// <param name="path">File path</param>
        /// <param name="rootElementName">Root element name</param>
        /// <param name="childElementsName">Child element name</param>
        /// <param name="insertionType">Determine if the data should be inserted as <see cref="XElement"/> or <see cref="XAttribute"/></param>
        public void Export<T>(IEnumerable<T> data, string path, string rootElementName, string childElementsName, DataInsertionType insertionType)
        {
            _SetProps(path, rootElementName, childElementsName, insertionType);

            _Export(data);
        }

        /// <inheritdoc cref = "Export{T}(System.Collections.Generic.IEnumerable{T},string,string,string,MBXmlCore.Enums.DataInsertionType)" />
        /// <returns><see cref="Task"/></returns>
        public Task ExportAsync<T>(IEnumerable<T> data, string path, string rootElementName, string childElementsName, DataInsertionType insertionType)
        {
            return Task.Run(() => Export(data, path, rootElementName, childElementsName, insertionType));
        }

        #endregion
    }
}
