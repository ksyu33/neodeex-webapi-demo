class FoxServiceError extends Error {
    constructor(errorInfo) {
        super(errorInfo.Message);
        this.errorInfo = errorInfo;
    }
}

class FoxServiceClient {
    constructor(url) {
        this.url = url;
        this.authString = null;
    }

    async invoke(action, bizRequest) {
        let requestInit = {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(bizRequest),
        };
        if (this.authString) {
            requestInit.headers["FoxRest-Authenticate"] = this.authString;
        }
        let targetUrl = this.url + '/' + action;
        let response = await fetch(targetUrl, requestInit);
        // Content-Type 이 JSON 이면 정상 응답이거나 오류 정보를 포함하고 있으므로
        // 역직렬화를 수행한다.
        let contentType = response.headers.get("Content-Type");
        let result;
        if (contentType && contentType.indexOf("application/json") >= 0) {
            result = await response.json();
        }
        if (response.ok) {
            return result;
        } else {
            if (result) {
                result.message = `HTTP ${response.status} : ` + result.message;
                throw new FoxServiceError(result);
            } else {
                throw new Error(`HTTP ${response.status} - ${response.statusText}`);
            }
        }
    }
}

class FoxBizServiceClient extends FoxServiceClient {
    async execute(bizRequest) {
        return await this.invoke("execute", bizRequest);
    }
}

class FoxDataServiceClient extends FoxServiceClient {
    async executeDataSet(dataRequest) {
        return await this.invoke("executeDataSet", dataRequest);
    }
    async executeNonQuery(dataRequest) {
        return await this.invoke("executeNonQuery", dataRequest);
    }
    async executeScalar(dataRequest) {
        return await this.invoke("executeScalar", dataRequest);
    }
}

const FoxBizRequestDiagnostics = {
    None: 0,
    ServiceLog: 1,
    LogId: 0x2,
    PerformanceLog: 0x4,
    SupressPerfLogWrite: 0x400,
    All: 0x407    
}

export { FoxBizServiceClient, FoxServiceError, FoxBizRequestDiagnostics, FoxDataServiceClient };