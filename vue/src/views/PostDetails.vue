<template>
    <div class="secondary">
        <h1 align="center" class="ma-5 pa-5">{{ $store.state.currentForum }}</h1>
        <v-row align="center" justify="center" no gutters>
            
            <!-- <v-btn class="pa-10 ma-10" @click.native="sortMostRecent">Most Recent</v-btn>
            <v-btn class="pa-10 ma-10">Most Popular</v-btn> -->
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10">Love Forum</v-btn>
            <v-btn v-if="$store.state.token != ''" class="pa-10 ma-10" @click.native="showPostForm = !showPostForm">Add Post</v-btn>
                <!-- <div v-show="showPostForm">
                    <add-post />
                </div> -->
            <v-btn v-if="$store.state.user.role == 'admin'" class="pa-10 ma-10" v-on:click="deleteForum(currentForum)">Delete Forum</v-btn>
        </v-row>
        <div class="pa-10 ma-10" v-show="showPostForm">
                    <add-post />
                </div>
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
        deleteForum(forumId) {
            postService.deleteForum(forumId)
            .then((response) => {
                if (response.status === 204) {
              alert("Forum has been deleted");
              window.location.reload();
            }
            })
        },
        sortMostRecent() {
            this.post.sort((a, b) => {
                return new Date(a.date) - new Date(b.date)
            })
            return this.posts;
        }
    }
}

</script>

<style>

</style>