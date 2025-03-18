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
            public const string Update = $"{Base}";
            public const string Delete = $"{Base}/{{id}}";
        }
        public static class Category
        {
            private const string Base = $"{ApiBase}/categories";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}";
            public const string Delete = $"{Base}/{{id}}";
        }
        public static class Cart
        {
            private const string Base = $"{ApiBase}/cart";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
        }
        public static class Wishlist
        {
            private const string Base = $"{ApiBase}/wishlist";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
        }

        public static class Customer
        {
            private const string Base = $"{ApiBase}/customers";
            public const string Get = $"{Base}/{{id}}";

            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
            public const string GetCartItems = $"{Base}/Cart";
            public const string AddCartItem = $"{Base}/Cart";
            public const string DeleteCartItem = $"{Base}/Cart/{{id}}";
            public const string UpdateCartItemQuantity = $"{Base}/Cart/{{id}}";
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
