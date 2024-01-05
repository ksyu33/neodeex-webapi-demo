<template>
  <div class="container-fluid">
    <div class="row h-100">
      <div class="col-md-6 mt-2">
        <div class="d-flex justify-content-between">
          <div class="form-check-inline form-switch ps-8">
            <input class="form-check-input" type="checkbox" role="switch" id="throwException" v-model="throwException" />
            <label class="form-check-label text-nowrap ms-1" for="throwException">Throw exception on Error</label>
          </div>
          <button type="button" class="btn btn-primary" @click="onExecuteClick">Execute Biz Logic</button>
        </div>
        <div class="input-group mt-1">
          <span class="input-group-text">Class ID</span>
          <input class="form-control" v-model="classId"/>
          <span class="input-group-text">Method ID</span>
          <input type="text" class="form-control" v-model="methodId"/>
        </div>
        <!-- 매개변수-->
        <Parameters v-model="parameters" />
        <!-- 진단 속성 -->
        <Diagnostics v-model="diagnostics" idKey="biz" title="Biz Request Diagnostic Options"/>
        <!-- 호출 결과 -->
        <Result :success="successInfo" :errorInfo="errorInfo" />
      </div>
      <div class="col-md-6">
        <!-- Reqeust/Response JSON -->
        <RequestResponseJson :request="bizRequest" :response="bizResponse" />
      </div>
    </div>
  </div>
</template>

<script>
import Parameters from './Parameters.vue';
import Diagnostics from './Diagnostics.vue';
import RequestResponseJson from './RequestResponseJson.vue';
import Result from './Result.vue';
import { FoxBizServiceClient, FoxServiceError } from './FoxServiceClient'

export default {
  components: {
    Parameters,
    Diagnostics,
    RequestResponseJson,
    Result
  },
  data() {
    return {
      classId: "",
      methodId: "",
      throwException: true,
      parameters: [ {name: 'key1', value: '"value"'}],
      diagnostics: [],
      bizResponse: null,
      successInfo: null,
      errorInfo: null,
    }
  },
  mounted() {
  },
  methods: {
    async onExecuteClick() {
      this.bizResponse = null;
      let request = this.bizRequest;
      let url = "https://simpleisbest-hello-func.azurewebsites.net/api/bizservice";
      
      let client = new FoxBizServiceClient(url);
      try {
        let response = await client.execute(request);
        this.bizResponse = response;
        this.errorInfo = null;
        this.successInfo = "HTTP 200 - OK";
      } catch (e) {
        if (e instanceof FoxServiceError) {
          this.bizResponse = e.errorInfo;
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
    bizRequest() {
      let params = {};
      this.parameters.forEach(p => {
        try {
          params[p.name] = JSON.parse(p.value);
        } catch(e) {
          params[p.name] = p.value;
        }
      });
      let request = { classId: this.classId, methodId: this.methodId, parameters: params };
      if (!this.throwException) {
        request.throwException = this.throwException;
      }
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
