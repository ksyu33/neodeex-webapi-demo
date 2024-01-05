<template>
  <div class="card mt-1" style="min-height:100px; max-height:360px">
    <div class="card-header">Result</div>
    <div class="card-body">
      <div class="alert alert-success mb-1" role="alert" v-if="success">{{ success }}</div>
      <div class="alert alert-danger mb-1" style="height: 300px;" role="alert" v-if="errorInfo">
        <ul class="list-group" style="height:100%;">
          <li class="list-group-item list-group-item-warning" style="height: 15%; overflow: auto;">{{ errorInfo.message }}</li>
          <li class="list-group-item list-group-item-danger" style="height: 20%; overflow: auto;">{{ errorInfo.messageDetail }}</li>
          <li class="list-group-item list-group-item-danger" style="height: 65%; overflow: auto;"><pre style="overflow: visible;">{{ errorInfo.stackTrace }}</pre></li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  props: [ "success", "errorInfo" ]
}

</script>
