namespace Gymnastic.API.APIEndpoints
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";
        public static class Products
        {
            private const string Base = $"{ApiBase}/products";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }
        public static class Auth
        {
            private const string Base = $"{ApiBase}/Auth";

            public const string Register = $"{Base}/Register";
            public const string Login = $"{Base}/Login";
            public const string VerifyEmail = $"{Base}/verify-email";
            public const string SendEmailVerificaiton = $"{Base}/send-email-verification";
        }
    }
}
