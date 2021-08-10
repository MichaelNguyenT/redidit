<template>
<div>
  <h1 align="center" justify="center">Welcome, Username!</h1>
  <h2 align="center" justify="center" class="py-5 my-5">Your Top Forums To Re-Do</h2>
  <forum-carousel />
</div>
</template>

<script>
import ForumCarousel from '../components/ForumCarousel.vue'
export default {
  components: { ForumCarousel },

}
</script>

<style>

</style>