using DigestTools;
using System;

namespace ValueTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //Simple test with wikipedia page : https://en.wikipedia.org/wiki/Digest_access_authentication

            DigestResponse resp = new DigestResponse
            {
                username = "Mufasa",
                realm = "testrealm@host.com",
                nonce = "dcd98b7102dd2f0e8b11d0f600bfb0c093",
                uri = "/dir/index.html",
                qop = "auth",
                nc = "00000001",
                cnonce = "0a4f113b",
                opaque = "5ccc069c403ebaf9f0171e9517f40e41",
                algorithm = null,
                method = "GET"
            };

            Console.WriteLine(DigestTool.GetExpectedResponse(resp, "Circle Of Life"));

            Console.ReadKey();
        }
    }
}
