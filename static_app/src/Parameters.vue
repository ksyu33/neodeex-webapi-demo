<template>
  <div class="card mt-1">
    <div class="card-header">Parameters</div>
    <div class="card-body text-end">
      <a href="#" class="btn btn-success mb-1" @click="addParameters">Add Parameter</a>
      <template v-for="(param, index) in parameters">
        <div class="input-group">
          <button type="button" class="btn" @click="removeParameter(index)" style="padding-left: 2px; padding-right: 2px;">
            <img class="trashcan" src="./assets/trashcan.svg" width="16" height="16"/>
          </button>
          <span class="input-group-text">Name</span>
          <input type="text" class="form-control" v-model="param.name"/>
          <span class="input-group-text">Value</span>
          <input type="text" class="form-control" v-model="param.value"/>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
export default {
  props: [ "modelValue"],
  emits: [ "update:modelValue" ],
  computed: {
    parameters: {
      get() {
        return this.modelValue
      },
      set(value) {
        this.$emit('update:modelValue', value)
      }
    }
  },
  methods: {
    addParameters() {
      let param = { name: 'key' + (this.parameters.length + 1), value: '' }
      this.parameters.push(param);
    },
    removeParameter(index) {
      this.parameters.splice(index, 1);
    },
  }
}
</script>

<style scoped>
.trashcan:hover {
    filter: invert(16%) sepia(89%) saturate(6054%) hue-rotate(358deg) brightness(97%) contrast(113%);
}
</style>
