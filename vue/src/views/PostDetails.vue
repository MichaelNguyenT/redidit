<template>
    <div class="secondary">
        <h1 align="center">Forum Title</h1>
        <v-row align="center" justify="center" no gutters>
            
            <v-btn class="pa-10 ma-10">Most Recent</v-btn>
            <v-btn class="pa-10 ma-10">Most Popular</v-btn>
        </v-row>
        <post-detail v-bind:posts="posts"/>
        <replies />
       
    </div>
</template>

<script>
import Replies from "../components/Replies.vue"
import PostDetail from "../components/PostDetail.vue"
import postService from '../services/PostService.js'

export default {
    name: "post-details",
    components: {
        PostDetail,
        Replies
    },
    data() {
        return {
            posts: [], 
        }
        
    },
    created() {
        postService.getPost(this.$route.params.forumId).then(
            (resp) => {
                this.posts = resp.data;
            })
    },

}
</script>

<style>

</style>