using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace TechTalk.SpecFlow.RuntimeTests.AssistTests.ValueRetrieverTests
{
    public class ReadOnlyCollectionRetrieverTests : EnumerableRetrieverTests
    {
        protected override EnumerableValueRetriever CreateTestee() => new ReadOnlyCollectionRetriever();

        protected override IEnumerable<Type> BuildPropertyTypes(Type valueType)
        {
            var propertyTypeDefinitions = new[]
            {
                typeof(IReadOnlyCollection<>),
            };
            
            return propertyTypeDefinitions.Select(x => x.MakeGenericType(valueType));
        }
    }
}
