using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ArvitumNew.Service.CountExamination
{
    public class CountPayExaminationMiddleware
    {
        private readonly RequestDelegate _next;

        public CountPayExaminationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, CountPayExamination countPayExamination)
        {
            switch(context.Request.Path.Value.ToLower())
            {
                case "/paycount":
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(countPayExamination?.Count.ToString());
                        break;
                    }
                case "/paycolor":
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(countPayExamination?.Color);
                        break;
                    }
                default:
                    {
                        await _next.Invoke(context);
                        break;
                    }
            }
        }
    }
}
