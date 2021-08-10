<template>
<v-card class="d-flex mx-15 mb-15">
            <v-carousel>
                <v-carousel-item v-for="forum in forums" v-bind:key="forum.forumId">
                 <v-row class="fill-height">
                    <v-col align="center" justify="center">
                    <v-img src="https://cdn.mos.cms.futurecdn.net/jG3Csc3oK8zvPgzPn5CPMJ-1024-80.jpg.webp" max-height="500" max-width="500" class="ma-4"></v-img>
                     <h1 class="ma-2">
                        <router-link  v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text" @click.native="setTitle(forum.forumTitle)">{{ forum.forumTitle }}</router-link>
                    </h1>
                     <v-chip class="align-end ma-4 pa-6">Love</v-chip>
                    </v-col>
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
    methods: {
        setTitle(title) {
            this.$store.commit('SET_CURRENT_FORUM', title);
        }
    }
}
</script>

<style>

</style>