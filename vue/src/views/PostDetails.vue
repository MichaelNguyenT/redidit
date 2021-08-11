<template>
    <div class="secondary">
        <h1 align="center" class="ma-5 pa-5">{{ $store.state.currentForum }}</h1>
        <v-row align="center" justify="center" no gutters>
            
            <v-btn class="pa-10 ma-10">Most Recent</v-btn>
            <v-btn class="pa-10 ma-10">Most Popular</v-btn>
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10">Love Forum</v-btn>
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10" @click.native="showPostForm = !showPostForm">Add Post</v-btn>
                <div v-show="showPostForm">
                    <add-post />
                </div>
            <v-btn v-if="$store.state.user.role == 'admin'" class="pa-10 ma-10" @click.native="deleteForum()">Delete Forum</v-btn>
        </v-row>
        <post-detail v-bind:posts="posts"/>
        <replies />
       
    </div>
</template>

<script>
import Replies from "../components/Replies.vue"
import PostDetail from "../components/SingleForum.vue"
import postService from "../services/PostService.js"
import AddPost from "@/components/AddPost.vue"

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
            showPostForm: false
        }
        
    },
    created() {
        postService.getPost(this.$route.params.forumId).then(
            (resp) => {
                this.posts = resp.data;

                this.$store.commit('SET_CURRENT_POSTS', this.posts);
            })
    },
    methods: {
        deleteForum() {

        }
    }
}
</script>

<style>

</style>