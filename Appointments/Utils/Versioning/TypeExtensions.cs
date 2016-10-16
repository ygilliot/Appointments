using System;
using System.Collections.Generic;
using System.Linq;

namespace Appointments.Api.Versioning
{
    public static class TypeExtensions
    {
        public static Type GetEnumerableType( this Type type )
        {
            return IsIEnumerable( type ) ? 
                type.GetGenericArguments()[0] : 
                ( from i in type.GetInterfaces() where IsIEnumerable( i ) select i.GetGenericArguments()[0] ).FirstOrDefault();
        }

        private static bool IsIEnumerable( Type type )
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof( IEnumerable<> );
        }
    }
}
