<template>
    <div>
        <single-post v-for="post in posts" v-bind:key="post.postId" v-bind:post="post"></single-post> 
    </div>
    
</template>

<script>
import SinglePost from '../components/SinglePost.vue'


//what needs to stay in postdetail, vs what needs to be moved over to TopPost? I copied everything over
//and PostDetail's only function should end up being "gets all posts in a given forum", correct?

export default {
  components: { SinglePost },
    name: 'post-details',
    props: ['posts'],
}
</script>

<style>

</style>