namespace DigestTools
{
    public struct DigestResponse
    {
        public string username;
        public string realm;
        public string nonce;
        public string uri;
        public string qop;
        public string nc;
        public string cnonce;
        public string response;
        public string opaque;
        public string algorithm;

        public string method;
        public string entityBody;
    }
}
