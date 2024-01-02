<template>
  <TopNavBar :initialPage="page" @onPageChange="(index) => this.page = index"/>
  <div class="container">
    <Home v-if="page == 0" />
    <FoxBizService v-show="page == 1"/>
    <FoxDataService v-show="page == 2"/>
  </div>
</template>

<script>
import TopNavBar from './TopNavBar.vue';
import Home from './Home.vue';
import FoxBizService from './FoxBizService.vue';
import FoxDataService from './FoxDataService.vue';

export default {
  components: {
    TopNavBar,
    Home,
    FoxBizService,
    FoxDataService,
  },
  data() {
    return {
      page: 0
    }
  }
}
</script>

<style scoped>
</style>
