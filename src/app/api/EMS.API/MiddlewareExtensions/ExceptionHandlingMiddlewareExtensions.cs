namespace EMS.API.MiddlewareExtensions
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static void UseGlobalExceptionErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}