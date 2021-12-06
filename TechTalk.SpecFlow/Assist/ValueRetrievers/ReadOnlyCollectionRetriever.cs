using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace TechTalk.SpecFlow.Assist.ValueRetrievers
{
    public class ReadOnlyCollectionRetriever : EnumerableValueRetriever
    {
        private MethodInfo toListMethodInfo;
        public override bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (!propertyType.IsGenericType)
            {
                return false;
            }

            var genericType = propertyType.GetGenericTypeDefinition();
            return genericType == typeof(IReadOnlyCollection<>);
        }

        protected override Type GetActualValueType(Type propertyType)
        {
            return propertyType.GetGenericArguments()[0];
        }

        protected override object BuildInstance(int count, IEnumerable values, Type valueType)
        {
            var parameters = new object[]{values};
            return GetMethod().MakeGenericMethod(valueType).Invoke(this, parameters);
        }
        
        private MethodInfo GetMethod() => toListMethodInfo ??= typeof(ReadOnlyCollectionRetriever).GetMethod(nameof(ToReadOnlyCollection), BindingFlags.NonPublic | BindingFlags.Instance);

        private IReadOnlyCollection<T> ToReadOnlyCollection<T>(IEnumerable values)
        {
            var temp = values.Cast<T>().ToList();
            return new ReadOnlyCollection<T>(temp);
        }
    }
}
