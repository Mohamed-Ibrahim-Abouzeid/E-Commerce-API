using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class RedisCasheAttribute:ActionFilterAttribute
    {
        private readonly int _durationInSeconds;
        public RedisCasheAttribute(int durationInSeconds=90)
        {
            _durationInSeconds = durationInSeconds;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheservice = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();
            var casheKey = CreateCasheKey(context.HttpContext.Request);
            var cashed=await casheservice.GetAsync(casheKey);
            if (!string.IsNullOrEmpty(cashed))
            {
                context.Result = new ContentResult
                {
                    Content = cashed,
                    ContentType = "application/json",
                    StatusCode=StatusCodes.Status200OK
                };
                return;
            }
            var executed = await next.Invoke();
            if (executed.Result is OkObjectResult { Value:not null }ok)
                
            {
                await casheservice.SetAsync(casheKey, ok.Value, TimeSpan.FromSeconds(_durationInSeconds));
            }
        }


        private string CreateCasheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path).Append("?");
            foreach (var (k, v) in request.Query.OrderBy(x => x.Key))
            {
                key.Append(k).Append("=").Append(v).Append("&");
            }
            return key.ToString();
        }
    }
}
