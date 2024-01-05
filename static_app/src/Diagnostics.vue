<template>
  <div class="card mt-1">
    <div class="card-header">
        {{title}}
    </div>
    <div class="card-body">
      <div class="form-check">
        <input class="form-check-input" type="checkbox" :id="gen_id('diagServiceLog')" value="ServiceLog" v-model="diagnostics" />
        <label class="form-check-label" :for="gen_id('diagServiceLog')">Service Log</label>
      </div>
      <div class="form-check">
        <input class="form-check-input" type="checkbox" :id="gen_id('diagLogId')" value="LogId" v-model="diagnostics" />
        <label class="form-check-label" :for="gen_id('diagLogId')">LogId</label>
      </div>
      <div class="form-check">
        <input class="form-check-input" type="checkbox" :id="gen_id('diagPerformanceLog')" value="PerformanceLog" v-model="diagnostics" />
        <label class="form-check-label" :for="gen_id('diagPerformanceLog')">Performance Log</label>
      </div>
      <div class="form-check" v-if="isDataRequest">
        <input class="form-check-input" type="checkbox" :id="gen_id('diagDbProfileInfo')" value="DbProfileInfo" v-model="diagnostics" />
        <label class="form-check-label" :for="gen_id('diagDbProfileInfo')">DB Profile</label>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: [ "title", "idKey", "isDataRequest", "modelValue" ],
  emits: ['update:modelValue'],
  data() {
    return {
    }
  },
  methods: {
    gen_id(base_id) {
      if (this.idKey) {
        return base_id + '_' + this.idKey;
      }
      else {
        return base_id;
      }
    }
  },
  computed: {
    diagnostics: {
      get() {
        return this.modelValue
      },
      set(value) {
        this.$emit('update:modelValue', value)
      }
    },
  }
}
</script>