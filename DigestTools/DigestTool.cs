namespace DigestTools
{
    public static class DigestTool
    {

        private delegate string HA1_Method(DigestResponse parameters, string password);
        private static HA1_Method HA1Method;

        private delegate string HA2_Method(DigestResponse parameters);
        private static HA2_Method HA2Method;

        private delegate string Response_Method(DigestResponse parameters, string HA1, string HA2);
        private static Response_Method ResponseMethod;

        public static bool ResponseIsCorrect(DigestResponse parameters, string password)
        {
            return (string.Equals(GetExpectedResponse(parameters, password), parameters.response));
        }

        public static string GetExpectedResponse(DigestResponse parameters, string password)
        {
            SetDelegates(parameters);

            return ResponseMethod(parameters, HA1Method(parameters, password), HA2Method(parameters));
        }

        private static void SetDelegates(DigestResponse parameters)
        {
            if (parameters.algorithm == "MD5-sess")
                HA1Method = HA1_MD5SESS_Method;
            else
                HA1Method = HA1_MD5_Method;

            if (parameters.qop == "auth-int")
                HA2Method = HA2_AuthInt_Method;
            else
                HA2Method = HA2_Auth_Method;

            if (parameters.qop == null)
                ResponseMethod = Response_UnspecifiedQoP_Method;
            else
                ResponseMethod = Response_SpecifiedQoP_Method;
        }


        private static string HA1_MD5_Method(DigestResponse p, string password) => $"{p.username}:{p.realm}:{password}".ToMD5Hash();
        private static string HA1_MD5SESS_Method(DigestResponse p, string password) => $"{HA1_MD5_Method(p, password)}:{p.nonce}:{p.cnonce}".ToMD5Hash();

        private static string HA2_Auth_Method(DigestResponse p) => $"{p.method}:{p.uri}".ToMD5Hash();
        private static string HA2_AuthInt_Method(DigestResponse p) => $"{p.method}:{p.uri}:{p.entityBody.ToMD5Hash()}".ToMD5Hash();

        private static string Response_UnspecifiedQoP_Method(DigestResponse p, string HA1, string HA2) => $"{HA1}:{p.nonce}:{HA2}".ToMD5Hash();
        private static string Response_SpecifiedQoP_Method(DigestResponse p, string HA1, string HA2) => $"{HA1}:{p.nonce}:{p.nc}:{p.cnonce}:{p.qop}:{HA2}".ToMD5Hash();

    }
}
