using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ArvitumNew.Service.CountExamination
{
    public class CountDeliveryExaminationMiddleware
    {
        private readonly RequestDelegate _next;

        public CountDeliveryExaminationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, CountDeliveryExamination countDeliveryExamination)
        {
            switch (context.Request.Path.Value.ToLower())
            {
                case "/deliverycount":
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(countDeliveryExamination?.Count.ToString());
                        break;
                    }
                case "/deliverycolor":
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(countDeliveryExamination?.Color);
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
