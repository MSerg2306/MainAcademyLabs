using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ArvitumNew.Service.CountExamination
{
    public class CountMakeExaminationMiddleware
    {
        private readonly RequestDelegate _next;

        public CountMakeExaminationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, CountMakeExamination countMakeExamination)
        {
            switch(context.Request.Path.Value.ToLower())
            {
                case "/makecount":
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(countMakeExamination?.Count.ToString());
                        break;
                    }
                case "/makecolor":
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(countMakeExamination?.Color);
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
