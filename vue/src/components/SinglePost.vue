<template>
    <div>
        <v-container grid-list-xl>
            <v-row>
             <v-card elevation="3" outlined class="my-6" fluid style="width: 100%">
                <v-card-title class="pa-ma-2">{{post.postTitle}}</v-card-title>
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
                        
                        max-height="200"
                        max-width="200"
                        :src = post.imageURL
                    ></v-img>
                    <v-card-text>{{ post.content }}</v-card-text>
                </v-col>
                <v-divider class="mt-0 mx-4"></v-divider>
                <v-col class="d-flex justify-end mb-1">
                    <v-chip v-if="$store.state.token != ''" class="ma-1" @click.native="addCounter(post.postId)">
                         <v-icon medium>
                              mdi-heart-plus 
                        </v-icon>
                            Yes {{ post.upvoteCounter }}
                        </v-chip>
                    <v-chip v-if="$store.state.token != ''" class="ma-1" @click.native="subtractCounter(post.postId)">
                         <v-icon medium>
                            mdi-home-remove 
                        </v-icon>
                            Eww {{ post.downvoteCounter }}
                    </v-chip>
                     <v-chip class="ma-1" @click.native="displayReplies(post.postId)">See Replies</v-chip>
                    <v-chip class="ma-1" @click.native="hideReplies">Hide Replies</v-chip>
                    <template>
                    <v-dialog>
                        <template v-slot:activator="{ on, attrs }">
                            <v-chip v-if="$store.state.token != ''" class="ma-1" v-bind="attrs" v-on="on">Add Your Thoughts</v-chip>
                        </template>
                            <v-card elevation="3" outlined shaped class="my-4">
                                <v-card-title>Hello!</v-card-title>
                                <v-textarea outlined label="What you think bud?" v-model="reply.content" required></v-textarea>
                                <v-card-actions>
                                <v-btn @click.native="addReply()">Save</v-btn>
                                </v-card-actions>
                            </v-card>
                    </v-dialog>
                </template>
                <!-- v-if="$store.state.user.role == 'admin' @click.native" -->
            <v-chip  class="ma-1" v-if="$store.state.user.role == 'admin'" v-on:click="deletePost(post.postId)">Delete Post</v-chip>
            </v-col>
        </v-card>
   </v-row>   
    </v-container>
    <replies v-for="reply in replies" v-bind:key="reply.replyId" v-bind:reply="reply"></replies>
    </div>
</template>

<script>

import moment from 'moment'
import postService from '../services/PostService.js'
import Replies from '../components/Replies.vue'
//import addReply from '@/components/AddReply.vue'

export default {
    components: { Replies },
    name: 'single-post',
    props: ['post'],
    data() {
        return {
            posts: [],
            counterUp: 0,
            counterDown: 0,
            replies: [],
            reply: {
                postId: this.post.postId,
                content: '',
                username: this.$store.state.user.username
            }
        }
    },
    methods: {
        addReply() {
             postService.addReply(this.reply).then(
                (resp) =>{
                    if(resp.status === 200){
                        this.reply = {
                    postId: this.post.postId,
                    content: '',
                    username: this.$store.state.user.username
                }
                    }
                 })
        },
         displayReplies(postId) {
            postService.getReplies(postId).then(
            (resp) => {
                this.replies = resp.data;
            })
        },
        hideReplies() {
            this.replies = [];
        },
        addCounter(postId) {
            postService.updateUpvote(postId)
                .then((response) => {
                    let updateObject = {'postId': postId, 'response': response.data}
                    this.$store.commit("SET_VOTE_COUNTERS", updateObject)
                })
        },
        subtractCounter(postId) {
            postService.updateDownvote(postId)
                .then((response) => {
                    let updateObject = {'postId': postId, 'response': response.data}
                    this.$store.commit("SET_VOTE_COUNTERS", updateObject)
                })
        },
        deletePost(postId) {
            if (
                confirm("Are you sure you want to delete this card? This action cannot be undone.")
            ){
            postService.deletePost(postId)
            .then((response) => {
                if (response.status === 204) {
              alert("Post has been deleted");
              window.location.reload();
            }
            })
            }
        },
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