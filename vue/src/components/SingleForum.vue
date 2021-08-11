<template>
    <div>
        <single-post v-for="post in this.$store.state.currentPosts" v-bind:key="post.postId" v-bind:post="post"></single-post> 
    </div>
    
</template>

<script>
import SinglePost from './SinglePost.vue'
import postService from '../services/PostService.js'


//what needs to stay in postdetail, vs what needs to be moved over to TopPost? I copied everything over
//and PostDetail's only function should end up being "gets all posts in a given forum", correct?

export default {
  components: { SinglePost },
    name: 'post-details',
    props: ['posts'],
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