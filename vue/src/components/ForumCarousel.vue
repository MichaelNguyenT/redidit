<template>
    <v-carousel>
        <v-carousel-item v-for="forum in forums" v-bind:key="forum.forumId">
            <v-row class="fill-height" align="center" justify="center">
                <v-subtitle>
                    {{ forum.forumTitle }}|
                </v-subtitle>
                <v-subtitle>
                    <router-link  v-bind:to="{name: 'post-details' }" class="secondary--text"> Demo Here</router-link>
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