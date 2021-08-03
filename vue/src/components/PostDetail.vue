<template>
    <div>
        
        <v-container>
        <v-row no-gutters align="center">
        <div v-for="post in posts" v-bind:key="post.postId">
        <v-card elevation="3" outlined shaped>
            <v-card-title class="pa-md-2">{{post.postTitle}}</v-card-title>
            <v-card-subtitle>{{post.username}}</v-card-subtitle>
            <v-card-subtitle class="pa-md-1">{{post.postedDate}}</v-card-subtitle>
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
            <v-divider class="mt-0 mx-4"></v-divider>
            <v-col class="d-flex justify-end mb-1">
            <v-chip class="ma-1">Yes
                 <v-icon medium>
                     mdi-duck
                </v-icon>
            </v-chip>
            <v-chip class="ma-1">Eww
                 <v-icon medium>
                     mdi-beehive-off-outline
                </v-icon>
            </v-chip>
            <v-subheader>Replies to this Post...</v-subheader>
            </v-col>
        </v-card>
        </div>
        <v-spacer></v-spacer>
        <div v-for="reply in replies" v-bind:key="reply.replyId">
        <v-card elevation="3" outlined shaped>
            <v-col class="d-flex justify-start mb-1">
            <v-card-subtitle class="pa-md-1">{{ reply.username }}</v-card-subtitle>
            <v-card-subtitle class="pa-md-1">{{ reply.postedDate }}</v-card-subtitle>
            </v-col>
            <v-divider class="mt-0 mx-4"></v-divider>
            <v-card-text>{{ reply.content }}</v-card-text>
            </v-card>
        </div>
        </v-row>
       </v-container>
    </div>
</template>

<script>

import postService from '../services/PostService.js'

export default {
    data() {
        return {
            posts: [],
            replies: []
        }
    },
    created() {
        postService.getPost().then(
            (resp) => {
                this.posts = resp.data;
            }
        ),
        postService.getReplies().then(
            (resp) => {
                this.replies = resp.data;
            }
        )
    }

}
</script>

<style>

</style>