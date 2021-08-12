<template>
    <v-card class="d-flex mx-15 mb-15">
            <v-carousel>
                <v-carousel-item v-for="forum in this.$store.state.favoriteForums" v-bind:key="forum.forumId">
                 <v-row class="fill-height">
                    <v-col align="center" justify="center">
                    <v-img :src = forum.forumPicture max-height="300" max-width="300" class="ma-4"></v-img>
                     <h1 class="ma-2">
                        <router-link @click="setCurrentForum(forum)" v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text" >{{ forum.forumTitle }}</router-link>
                    </h1>
                     <v-chip v-if="$store.state.token != ''" class="align-end ma-4 pa-6">Love</v-chip>
                    </v-col>
                </v-row>
            </v-carousel-item>    
        </v-carousel>
</v-card>
</template>

<script>
//import postService from '../services/PostService.js'

export default {
    name: 'forum-carousel',
     data(){
         return {
            forums: []
         }
     },
    methods: {
        setCurrentForum(forum) {
            this.$store.commit('SET_CURRENT_FORUM', forum);
        }
    }
}
</script>

<style>

</style>