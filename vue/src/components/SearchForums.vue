<template>
<div>
    <v-form>
      <v-container>
          <v-text-field label="Search Forums Here..." outlined class="grey pa-5 mb-8" v-model="search"></v-text-field>
          
      </v-container>
    </v-form>
    <v-card class="ma-1 pa-1" v-for="forum in filteredForums" v-bind:key="forum.forumId">
        <h1>
        <router-link  v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text" @click.native="setTitle(forum.forumTitle)">{{ forum.forumTitle }}</router-link>
        </h1>
    </v-card>
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
    methods: {
        setTitle(title) {
            this.$store.commit('SET_CURRENT_FORUM', title);
        }
    },    
    computed: {
        filteredForums(){ 
            return this.forums.filter(forum => {
                return forum.forumTitle.toLowerCase().includes(this.search.toLowerCase());
            })
        }
    }
}
</script>

<style>

</style>