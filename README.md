# NeoDEEX 5.x Web API Demo

NeoDEEX 의 Fox Biz/Data Service Web API (과거 4.x 에서는 RESTful API 라고 불렀었음.)는 다양한 환경에서 Web API 를 통해 Biz/Data 서비스를 호출할 수 있도록 해줍니다. 이 예제는 [Vue.js](https://vuejs.org/) 를 사용하여 클라이언트 App 을 작성하고 [Azure Static Web Apps](https://docs.microsoft.com/azure/static-web-apps/overview) 으로 배포합니다. Web API 로는 Fox Biz/Data Service Web API 를 [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/) 에 적용하였습니다.

작동하는 Web App 의 Url 은 다음과 같습니다.

* [https://neodeex-webapi-demo-app.simpleisbest.net/](https://neodeex-webapi-demo-app.simpleisbest.net/)

## Prerequisites

* NeoDEEX 5.x

* Visual Studio 2022

  * Azure Development Workload

* Visual Studio Code

  * C# extension for Visual Studio Code

  * Azure Functions extension for Visual Studio Code.

## Directories structure

static_app 폴더에는 Vite + Vue 프로젝트가 포함되어 있으며 이 폴더에 새로운 내용이 push 되거나 pull request 가 처리되면 Github Workflow 에 의해 빌드되어 자동으로 Azure Static Web App 으로 배포 됩니다.

NeoDEEX_Services 폴더에는 Visual Studio 혹은 Visual Studio Code 를 사용하여 Azure Functions 프로젝트가 포함되어 있습니다. 이 Azure Functions 는 Fox Biz/Data Service Web API 를 사용하여 Http Trigger 를 구현하였으며 Static Web App 이 호출하게 됩니다.

## Build

이 예제를 빌드하기 위해서 다음 과정을 따릅니다.

### Static web app

다음과 같이 의존성을 설치하십시요.

```bash
npm install
```

Vite 가 사용되었기 때문에 개발 환경에서 테스트를 위해는 다음과 같이 수행해야 합니다.

```bash
npm run dev
```

빌드 및 배포는 GitHub Workflow 를 사용하기 때문에 별도로 빌드할 필요는 없습니다.

### Azure Functions

> :warning: 사용 가능한 NeoDEEX 버전에 따라 프로젝트 파일에서 패키지 참조를 수정해야 할 수도 있습니다.

Visual Studio 를 사용한 경우, 빌드 및 테스트는 일반적인 웹 프로젝트와 동일하게 수행하면 됩니다. 로컬 환경에서 테스트를 위해서는 `test.http` 파일을 사용하면 됩니다. 이 때, 테스트 대상이 되는 `host` 변수가 `localhost` 를 가리키도록 설정하십시요.

```txt
#@host=https://neodeex-webapi-demo-func.azurewebsites.net
@host=http://localhost:7162
```

Visual Studio Code 를 사용하는 경우에도 빌드 및 테스트는 일반적인 웹 프로젝트와 동일하게 수행하면 됩니다.
