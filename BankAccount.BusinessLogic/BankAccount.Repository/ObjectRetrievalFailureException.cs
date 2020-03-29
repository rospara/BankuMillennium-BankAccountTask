using System;
using System.Linq;
using System.Collections.Generic;

namespace BankAccount.Repository
{
    /// <summary>
    /// Specialised exception to signify that an object could not be found. Keeps the exception
    /// text in a single place to aid localisation later. If you're fed up writing:
    /// <para>if (obj == null) { throw new ArgumentException(string.Format("Item {0} not found", id)); }</para>
    /// <para>Use ObjectRetrievalFailureException.ThrowIfNull&lt;T&gt;(object, identifier) instead!</para>
    /// </summary>
    public class ObjectRetrievalFailureException : ApplicationException
    {
        /// <summary>
        /// The type of object that could not be found
        /// </summary>
        private readonly Type objectType;

        /// <summary>
        /// The identifier of the object that could not be found
        /// </summary>
        private readonly object identifier;

        /// <summary>
        /// Creates a new instance of a ObjectRetrievalFailureException
        /// </summary>
        /// <param name="objectType">The type of object that could not be found</param>
        /// <param name="identifier">Identifier of the object that could not be found</param>
        /// <param name="message">Custom message for the exception or NULL to use the default
        /// text (recommended)</param>
        public ObjectRetrievalFailureException(Type objectType, object identifier, string message)
            : base(message != null ? message : string.Format("{0} [{1}] not found", objectType.Name, identifier.ToString()))
        {
            this.identifier = identifier;
            this.objectType = objectType;
        }

        /// <summary>
        /// Creates a new instance of a ObjectRetrievalFailureException
        /// </summary>
        /// <param name="objectType">The type of object that could not be found</param>
        /// <param name="identifier">Identifier of the object that could not be found</param>
        public ObjectRetrievalFailureException(Type objectType, object identifier)
            : this(objectType, identifier, null)
        {

        }

        /// <summary>
        /// The type of object that could not be found
        /// </summary>
        public Type ObjectType { get { return this.objectType; } }

        /// <summary>
        /// Identifier of the object that could not be found
        /// </summary>
        public object Identifier { get { return this.identifier; } }

        /// <summary>
        /// Helper method to throw an ObjectRetrievalFailureException if obj
        /// is NULL
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <param name="identifier">The object's identifier</param>
        /// <param name="message">Exception message</param>
        public static void ThrowIfNull<T>(T obj, object identifier, string message = null)
        {
            if (obj == null)
            {
                throw new ObjectRetrievalFailureException(typeof(T), identifier, message);
            }
        }

        /// <summary>
        /// Helper method to throw an ObjectRetrievalFailureException
        /// </summary>
        /// <param name="identifier">The object's identifier</param>
        /// <param name="message">Exception message</param>
        public static void Throw<T>(object identifier, string message = null)
        {
            throw new ObjectRetrievalFailureException(typeof(T), identifier, message);
        }
    }
}
