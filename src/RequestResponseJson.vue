<template>
  <div class="card mt-1" style="min-height:120px">
    <div class="card-header"><strong>Request JSON</strong></div>
    <div class="card-body json-scroll">
      <pre class="json-pre" v-html="requestJson"></pre>
    </div>
  </div>
  <div class="card" style="min-height:240px; max-height: 480px;">
    <div class="card-header"><strong>Response JSON</strong></div>
    <div class="card-body json-scroll">
      <pre class="json-pre" v-html="responseJson" v-if="response"></pre>
    </div>
  </div>
</template>

<script>
import 'highlight.js/styles/stackoverflow-light.min.css';
import hljs from 'highlight.js/lib/core';
import json from 'highlight.js/lib/languages/json';

hljs.registerLanguage('json', json);

export default {
  props: [ "request", "response" ],
  data() {
    return {
    }
  },
  computed: {
    requestJson() {
      let request = this.request;
      let jsonString = JSON.stringify(request, null, "  ");
      return hljs.highlight(jsonString, { language: 'json' }).value;
    },
    responseJson() {
      let jsonString = JSON.stringify(this.response, null, "  ");
      return hljs.highlight(jsonString, { language: 'json'}).value;
    }
  }
}
</script>

<style scoped>
.json-scroll {
  max-height: 100%;
  max-width: 100%;
  overflow: auto;
}
.json-pre {
  overflow: visible;
}
</style>
