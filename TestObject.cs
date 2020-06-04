using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AspNetCoreJsonTests
{
    public class TestObject
    {
        // Uncomment attributes as needed:

        // [JsonPropertyName("stringValue")]
        public string StringValue { get; set; }

        // [JsonPropertyName("iEnumerableString_ListString")]
        public IEnumerable<string> IEnumerableString_ListString { get; set; }
            = new List<string>();

        // [JsonPropertyName("iEnumerableString_EmptyStringArray")]
        public IEnumerable<string> IEnumerableString_EmptyStringArray { get; set; }
            = new string[] { };

        // [JsonPropertyName("iEnumerableString_ArrayEmptyString")]
        public IEnumerable<string> IEnumerableString_ArrayEmptyString { get; set; }
            = Array.Empty<string>();

        // [JsonPropertyName("iEnumerableString_EnumerableEmptyString")]
        public IEnumerable<string> IEnumerableString_EnumerableEmptyString { get; set; }
            = Enumerable.Empty<string>();

        // [JsonPropertyName("iEnumerableTestSubObject_ListString")]
        public IEnumerable<TestSubObject> IEnumerableTestSubObject_ListString { get; set; }
            = new List<TestSubObject>();

        // [JsonPropertyName("iEnumerableTestSubObject_EmptyStringArray")]
        public IEnumerable<TestSubObject> IEnumerableTestSubObject_EmptyStringArray { get; set; }
            = new TestSubObject[] { };

        // [JsonPropertyName("iEnumerableTestSubObject_ArrayEmptyString")]
        public IEnumerable<TestSubObject> IEnumerableTestSubObject_ArrayEmptyString { get; set; }
            = Array.Empty<TestSubObject>();

        // [JsonPropertyName("iEnumerableTestSubObject_EnumerableEmptyString")]
        public IEnumerable<TestSubObject> IEnumerableTestSubObject_EnumerableEmptyString { get; set; }
            = Enumerable.Empty<TestSubObject>();
    }

    public class TestSubObject
    {
        // [JsonPropertyName("stringValue")]
        public string StringValue { get; set; }
    }
}
