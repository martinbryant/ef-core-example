using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ef_core_example.Models
{
    public static class LoggingMiddlewareApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogger(this IApplicationBuilder builder, ILogger logger)
        {
            builder.Use(async (context, next) =>
            {
                // context.Request.EnableBuffering();
                // var body = await ReadStream(context.Request.Body);

                var requestId = context.Request.HttpContext.TraceIdentifier.Split(":")[0];

                logger.LogError("middleware");
                try
                {
                    await next();

                }
                catch (Exception ex)
                {
                    logger.LogError(ex.InnerException.Message);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(ex.Message);

                }
            });

            return builder;
        }

        private async static Task<string> ReadStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }

    // public static class
}