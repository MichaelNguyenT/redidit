<template>
<v-card class="d-flex mx-16 mb-15">
            <v-carousel>
                <v-carousel-item v-for="post in posts" v-bind:key="post.postId">
                    <v-row class="fill-height">
                       <v-col align="center" justify="center" class="ma-15">
                            <v-avatar>
                                <img src="https://images-ext-2.discordapp.net/external/AdJWzJfIdpBJppSLXDGvxWy5Pgs9r4K5IczkHsiLU1g/https/i.ytimg.com/vi/GNc_ZKCmjJ8/maxresdefault.jpg?width=786&height=442">
                            </v-avatar>
                            <h1>{{ post.username }}</h1>
                            <h1>{{ post.postTitle }}</h1>
                            <h2>{{ post.postedDate | moment }}</h2>
                            <h2>{{ post.content }}</h2>
                       </v-col>
                    </v-row>
                </v-carousel-item>    
        </v-carousel>
</v-card>
</template>


<script>
import postService from '../services/PostService.js'
import moment from 'moment'

//greg imported all features below from PostDetails, but then moved into making SinglePost component. May need the below, may not; this component in only for top post on homepage as of now.

export default {
    name: "top-post",
    data() {
        return {
            posts: [],
        }
    },
    created() {
        postService.getPopularPost().then(
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