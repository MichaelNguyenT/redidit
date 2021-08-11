<template>
<v-card class="mx-15 mb-15">
            <v-carousel>
                <v-carousel-item>
                    <single-post v-for="post in posts" v-bind:key="post.postId" v-bind:post="post"></single-post> 
                </v-carousel-item>    
        </v-carousel>
</v-card>
</template>


<script>
import moment from 'moment'
import postService from '../services/PostService.js'
import SinglePost from '../components/SinglePost.vue'

//greg imported all features below from PostDetails, but then moved into making SinglePost component. May need the below, may not; this component in only for top post on homepage as of now.

export default {
    name: "top-post",
    components: { SinglePost },
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
    filters: {
        moment: function(postedDate) {
            return moment(postedDate).format('MMMM Do YYYY, h:mm:ss a');
        }
    }
}
</script>

<style>

</style>