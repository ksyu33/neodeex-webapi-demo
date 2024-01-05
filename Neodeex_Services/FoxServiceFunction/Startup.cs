//
// Startup.cs
//  : Function App 에 맞도록 초기화를 수행한다.
//
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using NeoDEEX;
using NeoDEEX.Configuration;
using NeoDEEX.ServiceModel.Services.Biz;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(FoxServiceFunction.Startup))]

namespace FoxServiceFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // Azure Function 의 AppBaseDirectory 는 바이너리 디렉터리가 아니다.
        // 따라서 Context 로부터 Function 앱의 루트 디렉터리를 재설정 해야 한다.
        string basePath = builder.GetContext().ApplicationRootPath;
        FoxUtils.AppBaseDirectory = basePath;
        // 개발 환경에서는 neodeex.config.dev.json 을 사용하도록 설정한다.
        string funcEnv = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
        string configFile = "neodeex.config.json";
        if (funcEnv != null && funcEnv == "Development")
        {
            string devConfigFile = "neodeex.config.dev.json";
            string tempPath = Path.Combine(basePath, devConfigFile);
            // neodeex.config.dev.json 이 존재한느 경우에만 사용한다.
            if (File.Exists(tempPath))
            {
                configFile = devConfigFile;
            }
        }
        // 구성 설정 파일 위치 지정. 이 코드가 없는 경우 구성 파일을 찾지 못한다.
        FoxConfigurationManager.ConfigurationFileName = Path.Combine(basePath, configFile);
        // 비즈 모듈을 로드 한다.
        FoxBizServiceConfig.Configure();
    }
}