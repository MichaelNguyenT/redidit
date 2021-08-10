<template>
    <div>
        <v-container grid-list-xl>
            <v-row>
             <v-card elevation="3" outlined shaped class="my-6" fluid style="width: 100%">
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
                        lazy-src="https://picsum.photos/id/11/10/6"
                        max-height="200"
                        max-width="200"
                        src="https://picsum.photos/id/11/500/300"
                    ></v-img>
                    <v-card-text>{{ post.content }}</v-card-text>
                </v-col>
                <v-divider class="mt-0 mx-4"></v-divider>
                <v-col class="d-flex justify-end mb-1">
                    <v-chip class="ma-1" @click.native="addCounter(post.postId)">
                         <v-icon medium>
                              mdi-duck 
                        </v-icon>
                            Yes {{ post.upvoteCounter }}
                        </v-chip>
                    <v-chip class="ma-1" @click.native="subtractCounter(post.postId)">
                         <v-icon medium>
                            mdi-beehive-off-outline 
                        </v-icon>
                            Eww {{ post.downvoteCounter }}
                    </v-chip>
                     <v-chip class="ma-1" @click.native="displayReplies(post.postId)">See Replies</v-chip>
                    <v-chip class="ma-1" @click.native="hideReplies">Hide Replies</v-chip>
            <v-dialog>
                <template v-slot:activator="{ on, attrs }">
                    <v-chip class="ma-1" v-bind="attrs" v-on="on">Add Your Thoughts</v-chip>
                </template>
                <v-card elevation="3" outlined shaped class="my-4">
                    <v-card-title>Hello!</v-card-title>
                    <v-textarea outlined label="What you think bud?">{{ reply.content }}</v-textarea>
                    <v-card-actions>
                    <v-btn @click.native="addReply">Save</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
            <v-chip class="ma-1">Delete Post</v-chip>
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
                postId: this.postId,
                content: '',
                username: this.username
            }
        }
    },
    methods: {
        addReply() {
             postService.addReply(this.reply).then(
                (resp) =>{
                    if(resp.status === 201){
                         this.reply = { postId: '', username: '', content: ''}
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
            // .then((response) => {
            //     if (response.status == 200) {
                    
            //     }
            // })
        },
        subtractCounter(postId) {
            postService.updateDownvote(postId)
            // .then((response) => {
            //     if (response.status == 200) {
                    
            //     }
            // })
        }
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