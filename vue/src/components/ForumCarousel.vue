<template>
<v-card class="d-flex mx-15 mb-15">
            <v-carousel>
                <v-carousel-item v-for="forum in forums" v-bind:key="forum.forumId">
                 <v-row class="fill-height">
                    <v-col align="center" justify="center">
                        <v-img src="https://cdn.mos.cms.futurecdn.net/jG3Csc3oK8zvPgzPn5CPMJ-1024-80.jpg.webp"      max-height="500" max-width="500" class="ma-4">
                        </v-img>
                        <h1 class="ma-2">
                        <router-link  v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text" @click.native="setCurrentForum(forum)">{{ forum.forumTitle }}</router-link>
                        </h1>
                     <v-chip v-if="$store.state.token != ''" @click.native="setCurrentForum(forum), loveForum()" class="align-end ma-4 pa-6">Love</v-chip>
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
            forums: [],
            forumId: this.forumId,
            userId: this.$store.state.user.userId
         }
     },
     created() {
        postService.getForum().then(
            (resp) => {
                this.forums = resp.data;
            })
    },
    methods: {
        setTitle(forum) {
            this.$store.commit('SET_CURRENT_FORUM', forum);
        },
        // loveForum() {
        //     postService.loveForum(this.userId, this.$store.state.currentForum.forumId)
        //     .then(resp => {
        //             if (resp.status == 200) {
        //             this.$store.commit('LOVE_FORUM', this.$store.state.currentForum.forumId);
        //         }
        //     }
        //     )},

        loveForum() {
                    this.$store.commit('LOVE_FORUM', this.$store.state.currentForum);
                },

        setCurrentForum(forum) {
            this.$store.commit('SET_CURRENT_FORUM', forum);
        }
}
}
</script>

<style>

</style>