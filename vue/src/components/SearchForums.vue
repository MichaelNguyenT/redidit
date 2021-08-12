<template>
<div>
    <v-form  class="mx-10 mb-15">
      <v-container >
          <v-text-field label="Search Forums Here..." outlined class="grey pa-4 mb-8" v-model="search"></v-text-field>
      </v-container>
    </v-form>
    <v-card v-show="search != ''" class="ma-1 pa-1" v-for="forum in filteredForums" v-bind:key="forum.forumId">
        <h2>
        <router-link  v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text" @click.native="setTitle(forum.forumTitle)">{{ forum.forumTitle }}</router-link>
        </h2>
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
            empty: ''
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