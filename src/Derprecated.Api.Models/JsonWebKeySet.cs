namespace Derprecated.Api.Models
{
    using System.Collections.Generic;

    public class JsonWebKeySet
    {
        public List<JsonWebKey> Keys { get; set; }
    }

    public class JsonWebKey
    {
        public string Alg { get; set; }
        public string E { get; set; }
        public string Kid { get; set; }
        public string Kty { get; set; }
        public string N { get; set; }
        public string Use { get; set; }
        public string[] X5C { get; set; }
        public string X5T { get; set; }
    }
}
