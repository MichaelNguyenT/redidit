<template>
<v-card class="mx-auto">
    <v-toobar>
        <v-toolbar-title class="font-weight-bold secondary h1 ma-8 pa-3">Top Forums Now</v-toolbar-title>
        <v-spacer></v-spacer>
    </v-toobar>
            <v-carousel>
                <v-carousel-item v-for="forum in forums" v-bind:key="forum.forumId">
                 <v-row class="fill-height" align="center" justify="center">
                    <v-img src="https://cdn.mos.cms.futurecdn.net/jG3Csc3oK8zvPgzPn5CPMJ-1024-80.jpg.webp" max-height="500" max-width="500"></v-img>
                     <h1 class="ma-8">
                        <router-link  v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text">{{ forum.forumTitle }}</router-link>
                    </h1>
                </v-row>
            </v-carousel-item>    
        </v-carousel>
</v-card>
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