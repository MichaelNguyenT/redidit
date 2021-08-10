<template>
  <div class="home">
    <h2 align="center" class="ma-5 pa-5 secondary">Top Forums Now</h2>
    <forum-carousel />
    <h3 align="center" class="ma-5 pa-5 secondary">Find Your New Favorite Forum</h3>
    <search-forums />
    <h2 align="center" class="ma-5 pa-5 secondary">Top Posts Now</h2>
    <top-post />
   
  </div>
</template>

<script>
import TopPost from "../components/TopPost.vue"
import ForumCarousel from '../components/ForumCarousel.vue';
import SearchForums from '../components/SearchForums.vue'

export default {
  name: "home",
  components: {
    ForumCarousel,
    TopPost,
    SearchForums,
    },
};
</script>
