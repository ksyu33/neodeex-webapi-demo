//
// BizDataFunc.cs
//  : Fox Biz/Data Service �� ���� Function ����.
//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using NeoDEEX.ServiceModel.WebApi;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoxServiceFunction;

// Azure Function ���� Ŭ����
public static class BizDataFunc
{
    #region Fox Biz Service Function

    // FoxBizService ����ġ�� ���� �ɼ�
    private static readonly FoxBizServiceDispatchOptions BizOptions = new()
    {
        ReturnServerInfo = true,
        UseTypeInfo = false,
    };

    // FoxBizService �� �����ϴ� Function ����
    [FunctionName("BizService")]
    public static async Task<IActionResult> BizService(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bizservice/{action:alpha}")] HttpRequest req, string action)
    {
        IActionResult result = await req.Dispatch(action, BizOptions);
        return result.ProcessJsonResult();
    }

    #endregion

    #region Fox Data Service Function

    // FoxDataService ����ġ�� ���� �ɼ�
    private static readonly FoxDataServiceDispatchOptions DataOptions = new()
    {
        ReturnServerInfo = true,
        UseTypeInfo = false,
    };

    // FoxDataService �� �����ϴ� Function ����
    [FunctionName("DataService")]
    public static async Task<IActionResult> DataService(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "dataservice/{action:alpha}")] HttpRequest req, string action)
    {
        IActionResult result =  await req.Dispatch(action, DataOptions);
        return result.ProcessJsonResult();
    }

    #endregion

    #region Azure Function�� ���� ���� �޼���

    // Azure Functions ���� JSON ����ȭ�� Json.NET �� ����ϴ� ������ �ذ��ϱ� ���� �ӽ� Ȯ�� �޼���
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
