<template>
<v-card class="mx-15 mb-15">
            <v-carousel>
                <v-carousel-item v-for="post in posts" v-bind:key="post.postId">
                  <v-row class="fill-height" align="center" justify="center">
                        <v-card-title class="pa-md-2">{{post.postTitle}}</v-card-title>
                            <v-col class="d-flex justify-start mb-1 py-0">
                                <v-avatar>
                                    <img src="https://images-ext-2.discordapp.net/external/AdJWzJfIdpBJppSLXDGvxWy5Pgs9r4K5IczkHsiLU1g/https/i.ytimg.com/vi/GNc_ZKCmjJ8/maxresdefault.jpg?width=786&height=442">
                                </v-avatar>
                                <v-card-subtitle>{{ post.username }}</v-card-subtitle>
                            </v-col>
                        <v-card-subtitle class="pa-md-1">{{post.postedDate | moment}}</v-card-subtitle>
                        <v-divider class="mt-0 mx-4"></v-divider>
                        <v-col class="d-flex justify-space-between align-center">
                     <v-img
                    lazy-src="https://picsum.photos/id/11/10/6"
                     max-height="200"
                    max-width="200"
                    src="https://picsum.photos/id/11/500/300"
                ></v-img>
            <v-card-text>{{ post.content }}</v-card-text>
            </v-col>
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
    props: ['post'],
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