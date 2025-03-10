namespace Gymnastic.API.Middlewares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationMiddleware>();
        }
    }
}
