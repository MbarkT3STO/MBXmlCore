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
    /// XML importer middleware
    /// </summary>
    public class Importer
    {
        #region Properties

        public string Path { get; private set; }

        #endregion

        #region Private

        private XDocument _Import()
        {
            var xDoc = XDocument.Load( Path );

            return xDoc;
        }
        private IEnumerable<T> _Import<T>(DataSelectionType dataSelectionType) where T : class , new()
        {
            return dataSelectionType == DataSelectionType.Elements ? _ImportFromElements<T>() : _ImportFromAttributes<T>();
        }

        private IEnumerable<T> _ImportFromAttributes<T>() where T : class , new()
        {
            var xDoc       = XDocument.Load( Path );
            var properties = typeof( T ).GetProperties();

            foreach ( var element in xDoc.Root?.Elements() )
            {
                var obj = new T();
                foreach ( var prop in properties )
                {
                    var currentAttributeValue = element.Attribute( prop.Name )?.Value;
                    var convertedCurrentAttributeValue = prop.PropertyType.IsEnum
                                                             ? Enum.Parse( prop.PropertyType , currentAttributeValue )
                                                             : Convert.ChangeType( currentAttributeValue , prop.PropertyType );

                    obj.GetType().GetProperty( prop.Name )?.SetValue( obj , convertedCurrentAttributeValue, null );
                }

                yield return obj;
            }
        }
        private IEnumerable<T> _ImportFromElements<T>() where T : class , new()
        {
            var xDoc       = XDocument.Load( Path );
            var properties = typeof( T ).GetProperties();

            if ( xDoc.Root?.Elements() == null ) yield break;
            foreach ( var element in xDoc.Root.Elements() )
            {
                var obj = new T();
                foreach ( var prop in properties )
                {
                    var currentElementValue = element.Element( prop.Name )?.Value;
                    var convertedCurrentElementValue = prop.PropertyType.IsEnum
                                                           ? Enum.Parse( prop.PropertyType, currentElementValue )
                                                           : Convert.ChangeType( currentElementValue , prop.PropertyType );

                    obj.GetType().GetProperty( prop.Name )?.SetValue( obj , convertedCurrentElementValue , null );
                }

                yield return obj;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Import an XML file as an original <see cref="XDocument"/>
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns><see cref="XDocument"/></returns>
        public XDocument Import(string path)
        {
            Path = path;
            return _Import();
        }

        /// <summary>
        /// Asynchronously <inheritdoc cref="Import(string)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public Task<XDocument> ImportAsync(string path)
        {
            return Task.Run( () => Import( path ) );
        }


        /// <summary>
        /// Import an XML file as an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="selectionType">Determine if the data should be selected from Elements or Attributes</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public IEnumerable<T> Import<T>(string path, DataSelectionType selectionType = DataSelectionType.Elements) where T : class, new()
        {
            Path = path;

            return _Import<T>( selectionType );
        }

        /// <summary>
        /// Asynchronously <inheritdoc cref="Import{T}(string, DataSelectionType)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public Task<IEnumerable<T>> ImportAsync<T>(string path, DataSelectionType selectionType = DataSelectionType.Elements) where T : class, new()
        {
            return Task.Run( () => Import<T>( path , selectionType ) );
        }

        #endregion
    }
}
