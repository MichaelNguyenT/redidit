<template>
    <div class="secondary">
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