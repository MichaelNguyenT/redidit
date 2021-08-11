<template>
<div>
  <h1 align="center" justify="center" class="py-5 my-5 primary">Welcome, Username!</h1>
  <v-btn class="pa-10 ma-10" @click.native="showForumForm = !showForumForm">Create your own Forum!</v-btn>
    <div v-show="showForumForm">
      <add-forum />
    </div>
  <h2 align="center" justify="center" class="py-5 my-5 secondary">Your Top Forums To Re-Do</h2>
  <forum-carousel />
</div>
</template>

<script>

import ForumCarousel from '../components/ForumCarousel.vue'
import AddForum from "@/components/AddForum.vue"

export default {
  components: { ForumCarousel, AddForum },
  
  data() {
        return {
            showForumForm: false
        }
  }
}
</script>

<style>

</style>