<template>
<div class="primary">
  <h1 align="center" justify="center" class="py-5 my-5 secondary">Welcome, {{ this.$store.state.user.username}}!</h1>
  <v-btn class="pa-10 ma-10" @click.native="showForumForm = !showForumForm">Create your own Forum!</v-btn>
    <div v-show="showForumForm">
      <add-forum />
    </div>
  <h2 align="center" justify="center" class="py-5 my-5 secondary">Your Top Forums To Re-Do</h2>
    <loved-forums />
</div>
</template>

<script>

import LovedForums from '../components/LovedForums.vue'
import AddForum from "@/components/AddForum.vue"

export default {
  components: { LovedForums, AddForum },
  
  data() {
        return {
            showForumForm: false
        }
  }
}
</script>

<style>

</style>