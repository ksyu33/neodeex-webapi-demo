<template>
  <nav class="navbar navbar-expand-md navbar-dark bg-dark">
    <div class="container-fluid">
      <a class="navbar-brand" href="#" @click.prevent="()=> onPage(this.pageIndex)">
        <img src="./assets/neodeex_logo.png" alt="NeoDEEX logo" />
        {{title}}
      </a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link" :class="{ active: pageIndex == 0 }" href="#" @click.prevent="onPage(0)">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" :class="{ active: pageIndex == 1 }" href="#" @click.prevent="onPage(1)">Biz Service</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" :class="{ active: pageIndex == 2 }" href="#" @click.prevent="onPage(2)">Data Service</a>
          </li>
        </ul>
      </div>    
    </div>
  </nav>
</template>

<script>
export default {
  props: [ "initialPage", "modelValue" ],
  emits: [ "onPageChange", "update:modelValue"],
  data() {
    return {
      pages: [
        { title:"Fox Biz/Data Service WebApi Tester", page: 0 },
        { title:"Fox Biz Service WebApi Tester", page: 1 },
        { title:"Fox Data Service WebApi Tester", page: 2 },
      ],
      title: "",
      pageIndex: 0,
    }
  },
  created() {
    this.title = this.pages[this.initialPage].title;
    this.pageIndex = this.initialPage;
  },
  computed: {
  },
  methods: {
    onPage(index) {
      this.title = this.pages[index].title;
      this.pageIndex = index;
      this.$emit('onPageChange', index);
    }
  }
}
</script>