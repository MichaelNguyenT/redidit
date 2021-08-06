<template>
    <v-carousel>
        <v-carousel-item v-for="forum in forums" v-bind:key="forum.forumId">
            <v-row class="fill-height" align="center" justify="center">
                  <v-img src="https://cdn.mos.cms.futurecdn.net/jG3Csc3oK8zvPgzPn5CPMJ-1024-80.jpg.webp" max-height="200" max-width="200"></v-img>
                <v-subtitle>
                    <router-link  v-bind:to="{name: 'post-details' }" class="secondary--text">{{ forum.forumTitle }}</router-link>
                </v-subtitle>
            </v-row>
        </v-carousel-item>    
    </v-carousel>
</template>

<script>
import postService from '../services/PostService.js'

export default {
    name: 'forum-carousel',
     data(){
         return {
            forums: []
         }
     },
     created() {
        postService.getForum().then(
            (resp) => {
                this.forums = resp.data;
            })
    },
}
</script>

<style>

</style>