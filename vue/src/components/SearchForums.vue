<template>
<div>
    <v-form>
      <v-container>
          <v-text-field label="Search Forums Here..." outlined class="grey pa-5 mb-8" v-model="search"></v-text-field>
          
      </v-container>
    </v-form>
    <div v-for="forum in filteredForums" v-bind:key="forum.forumId">
        <h1>{{ forum.forumTitle }}</h1>
    </div>
</div>
</template>

<script>
import postService from '../services/PostService.js'

export default {
    name: 'search-forums',
    data() {
        return {
            forums: [],
            search: '',
        }
    },
     created() {
        postService.getForum().then(
            (resp) => {
                this.forums = resp.data;
            })
    },
    computed: {
        filteredForums(){ 
            return this.forums.filter(forum => {
                return forum.forumtTitle.toLowerCase().includes(this.search.toLowerCase());
            })
        }
    }
}
</script>

<style>

</style>