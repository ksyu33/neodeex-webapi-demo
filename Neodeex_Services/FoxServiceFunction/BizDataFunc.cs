//
// BizDataFunc.cs
//  : Fox Biz/Data Service 를 위한 Function 구현.
//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using NeoDEEX.ServiceModel.WebApi;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoxServiceFunction;

// Azure Function 구현 클래스
public static class BizDataFunc
{
    #region Fox Biz Service Function

    // FoxBizService 디스패치를 위한 옵션
    private static readonly FoxBizServiceDispatchOptions BizOptions = new()
    {
        ReturnServerInfo = true,
        UseTypeInfo = false,
    };

    // FoxBizService 를 제공하는 Function 구현
    [FunctionName("BizService")]
    public static async Task<IActionResult> BizService(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bizservice/{action:alpha}")] HttpRequest req, string action)
    {
        IActionResult result = await req.Dispatch(action, BizOptions);
        return result.ProcessJsonResult();
    }

    #endregion

    #region Fox Data Service Function

    // FoxDataService 디스패치를 위한 옵션
    private static readonly FoxDataServiceDispatchOptions DataOptions = new()
    {
        ReturnServerInfo = true,
        UseTypeInfo = false,
    };

    // FoxDataService 를 제공하는 Function 구현
    [FunctionName("DataService")]
    public static async Task<IActionResult> DataService(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "dataservice/{action:alpha}")] HttpRequest req, string action)
    {
        IActionResult result =  await req.Dispatch(action, DataOptions);
        return result.ProcessJsonResult();
    }

    #endregion

    #region Azure Function을 위한 헬퍼 메서드

    // Azure Functions 에서 JSON 직렬화에 Json.NET 을 사용하는 문제를 해결하기 위한 임시 확장 메서드
    private static IActionResult ProcessJsonResult(this IActionResult result)
    {
        if (result is JsonResult jsonResult)
        {
            object resultValue = jsonResult.Value;
            JsonSerializerOptions options = jsonResult.SerializerSettings as JsonSerializerOptions;
            ContentResult contentResult = new()
            {
                StatusCode = jsonResult.StatusCode,
                ContentType = "application/json",
                Content = JsonSerializer.Serialize(resultValue, options)
            };
            result = contentResult;
        }
        return result;
    }

    #endregion
}
