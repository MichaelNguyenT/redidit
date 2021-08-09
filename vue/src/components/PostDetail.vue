<template>
    <div>
        <single-post v-for="post in posts" v-bind:key="post.postId" v-bind:post="post" fluid style="width: 100%"></single-post> 
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
//   created() {
//         postService.getPost(this.$route.params.forumId).then(
//             (resp) => {
//                 this.posts = resp.data;
//             })
//     },
}
</script>

<style>

</style>