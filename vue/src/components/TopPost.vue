<template>
<v-card class="mx-auto">
    <v-toolbar>
        <v-toolbar-title class="font-weight-bold secondary h1 ma-8 pa-3">Top Post Now</v-toolbar-title>
    </v-toolbar>
            <v-carousel>
                <v-carousel-item v-for="post in posts" v-bind:key="post.postId">
                  <v-row class="fill-height" align="center" justify="center">
                  </v-row>
            </v-carousel-item>    
        </v-carousel>
</v-card>
</template>


<script>
import moment from 'moment'
import postService from '../services/PostService.js'

//greg imported all features below from PostDetails, but then moved into making SinglePost component. May need the below, may not; this component in only for top post on homepage as of now.

export default {
    name: "top-post",
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