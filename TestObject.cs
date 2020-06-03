using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreJsonTests
{
    public class TestObject
    {
        public string StringValue { get; set; }

        public IEnumerable<string> IEnumerableString_ListString { get; set; }
            = new List<string>();

        public IEnumerable<string> IEnumerableString_EmptyStringArray { get; set; }
            = new string[] { };

        public IEnumerable<string> IEnumerableString_ArrayEmptyString { get; set; }
            = Array.Empty<string>();

        public IEnumerable<string> IEnumerableString_EnumerableEmptyString { get; set; }
            = Enumerable.Empty<string>();

        public IEnumerable<TestSubObject> IEnumerableTestSubObject_ListString { get; set; }
            = new List<TestSubObject>();

        public IEnumerable<TestSubObject> IEnumerableTestSubObject_EmptyStringArray { get; set; }
            = new TestSubObject[] { };

        public IEnumerable<TestSubObject> IEnumerableTestSubObject_ArrayEmptyString { get; set; }
            = Array.Empty<TestSubObject>();

        public IEnumerable<TestSubObject> IEnumerableTestSubObject_EnumerableEmptyString { get; set; }
            = Enumerable.Empty<TestSubObject>();
    }

    public class TestSubObject
    {
        public string StringValue { get; set; }
    }
}
