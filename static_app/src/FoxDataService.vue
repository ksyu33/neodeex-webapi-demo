<template>
    <div class="container-fluid">
      <div class="row h-100">
        <div class="col-md-6 mt-2">
          <div class="d-flex justify-content-between">
            <div class="form-check-inline form-switch ps-8">
              <input class="form-check-input" type="checkbox" role="switch" id="throwException" v-model="throwException" />
              <label class="form-check-label text-nowrap ms-1" for="throwException">Throw exception on Error</label>
            </div>
            <button type="button" class="btn btn-primary" @click="onExecuteClick">Execute Fox Query</button>
          </div>
          <div class="input-group mt-1">
            <span class="input-group-text">Execution Type</span>
            <select class="form-select" v-model="action">
              <option value="executeDataSet">ExecuteDataSet</option>
              <option value="executeNonQuery">ExecuteNonQuery</option>
              <option value="executeScalar">ExecuteScalar</option>
            </select>
          </div>
          <div class="input-group mt-1">
            <span class="input-group-text">Query ID</span>
            <input class="form-control" v-model="queryId"/>
            <span class="input-group-text">Database</span>
            <input type="text" class="form-control" v-model="database"/>
          </div>
          <!-- 매개변수-->
          <Parameters v-model="parameters" />
          <!-- DataService 기타 옵션 -->
          <div class="card mt-1">
            <div class="card-header">Data Service Options</div>
            <div class="card-body">
              <div class="input-group">
                <span class="input-group-text">Command Timeout (sec)</span>
                <input type="text" class="form-control" v-model.number="commandTimeout">
              </div>
              <div class="input-group">
                <span class="input-group-text">Transaction Options</span>
                <select class="form-select" v-model="transaction">
                  <option selected value="None">None</option>
                  <option value="Local">Local Transaction</option>
                  <option value="Distributed" disabled>Distributed Transaction</option>
                </select>
              </div>
              <div class="input-group">
                <span class="input-group-text">Transaction Isolation Level</span>
                <select class="form-select" v-model="isolationLevel">
                  <option selected value="ReadCommitted">Read Committed</option>
                  <option value="ReadUncommitted" class="danger">Read Uncommitted</option>
                  <option value="RepeatableRead">Repeatable Read</option>
                  <option value="Serializable">Serializable</option>
                  <option value="Snapshot">Snapshot</option>
                  <option value="Unspecified">Unspecified</option>
                </select>
              </div>
            </div>
          </div>
          <!-- 진단 속성 -->
          <Diagnostics v-model="diagnostics" idKey="data" title="Data Request Diagnostic Options" isDataRequest="true"/>
          <!-- 호출 결과 -->
          <Result :success="successInfo" :errorInfo="errorInfo" />
        </div>
        <div class="col-md-6">
          <!-- Reqeust/Response JSON -->
          <RequestResponseJson :request="dataRequest" :response="dataResponse" />
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import Parameters from './Parameters.vue';
  import Diagnostics from './Diagnostics.vue';
  import RequestResponseJson from './RequestResponseJson.vue';
  import Result from './Result.vue';
  import { FoxDataServiceClient, FoxServiceError } from './FoxServiceClient'
  
  export default {
    components: {
      Parameters,
      Diagnostics,
      RequestResponseJson,
      Result
    },
    data() {
      return {
        queryId: "",
        database: "",
        throwException: true,
        action: "executeDataSet",
        parameters: [ {name: 'key1', value: '"value"'}],
        diagnostics: [],
        commandTimeout: null,
        transaction: null,
        isolationLevel: null,
        dataResponse: null,
        successInfo: null,
        errorInfo: null,
      }
    },
    mounted() {
    },
    methods: {
      async onExecuteClick() {
        this.dataResponse = null;
        this.errorInfo = null;
        let request = this.dataRequest;
        let url = "https://neodeex-webapi-demo-func.azurewebsites.net/api/dataservice";
        
        let client = new FoxDataServiceClient(url);
        try {
          let response = await client.invoke(this.action, request);
          this.dataResponse = response;
          this.errorInfo = null;
          this.successInfo = "HTTP 200 - OK";
        } catch (e) {
          if (e instanceof FoxServiceError) {
            this.dataResponse = e.errorInfo;
            this.errorInfo = e.errorInfo;
            this.successInfo = null;
          } else {
            this.errorInfo = e;
            this.successInfo = null;
          }
        }
      }
    },
    computed: {
      dataRequest() {
        let params = {};
        this.parameters.forEach(p => {
          try {
            params[p.name] = JSON.parse(p.value);
          } catch(e) {
            params[p.name] = p.value;
          }
        });
        let request = { queryId: this.queryId, parameters: params };
        // database 속성 설정
        if (this.database && this.database.length > 0) {
            request.databaseName = this.database;
        }
        // throwException 속성 설정
        if (!this.throwException) {
          request.throwException = this.throwException;
        }
        // commandTimeout 속성 설정
        if (this.commandTimeout && this.commandTimeout.length > 0) {
          try {
            request.commandTimeout = JSON.parse(this.commandTimeout);
          } catch {}
        }
        // transaction 속성 설정
        if (this.transaction && this.transaction != "None") {
          request.transaction = this.transaction;
          if (this.isolationLevel && this.isolationLevel != "ReadCommitted") {
            request.transactionIsolation = this.isolationLevel;
          }
        }
        // diagnostics 속성 설정
        const diagnostics = this.diagnostics;
        if (diagnostics && diagnostics.length > 0) {
          request.diagnostics = diagnostics.join(", ");
        }
        return request;
      },
    }
  }
  
  </script>
  
  <style scoped>
  .row {
    font-size: small;
    line-height: 100%;
  }
  </style>
  