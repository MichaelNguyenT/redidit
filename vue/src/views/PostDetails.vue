<template>
    <div class="secondary">
        <h1 align="center">{{ $store.state.currentForum }}</h1>
        <v-row align="center" justify="center" no gutters>
            
            <v-btn class="pa-10 ma-10">Most Recent</v-btn>
            <v-btn class="pa-10 ma-10">Most Popular</v-btn>
            <v-btn class="pa-10 ma-10">Love Forum</v-btn>
            <v-btn class="pa-10 ma-10"><add-post>Add Post</add-post></v-btn>
            <v-btn class="pa-10 ma-10">Delete Forum</v-btn>
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10">Love Forum</v-btn>
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10">Add Post</v-btn>
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10">Delete Forum</v-btn>
        </v-row>
        <post-detail v-bind:posts="posts"/>
        <replies />
       
    </div>
</template>

<script>
import Replies from "../components/Replies.vue"
import PostDetail from "../components/SingleForum.vue"
import postService from '../services/PostService.js'
import AddPost from "../components/AddPost.vue"

export default {
    name: "post-details",
    components: {
        PostDetail,
        Replies,
        AddPost
    },
    data() {
        return {
            posts: [], 
            forums: [],
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