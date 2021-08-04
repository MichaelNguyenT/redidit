<template>
    <div>
       <v-container grid-list-xl class="d-flex">
        <v-row>
        <div v-for="post in posts" v-bind:key="post.postId">
        <v-card elevation="3" outlined shaped class="my-4">
            <v-card-title class="pa-md-2">{{post.postTitle}}</v-card-title>
            <v-col class="d-flex justify-start mb-1 py-0">
             <v-avatar>
                    <img src="https://images-ext-2.discordapp.net/external/AdJWzJfIdpBJppSLXDGvxWy5Pgs9r4K5IczkHsiLU1g/https/i.ytimg.com/vi/GNc_ZKCmjJ8/maxresdefault.jpg?width=786&height=442">
                </v-avatar>
            <v-card-subtitle>{{post.username}}</v-card-subtitle>
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
            <v-chip class="ma-1" @click.native="displayReplies">See replies to this Post...</v-chip>
            <v-dialog persistent v-model="dialog">
                <template v-slot:activator="{ on, attrs }">
                    <v-chip v-bind="attrs" v-on="on">Add Your Thoughts</v-chip>
                </template>
                <v-card elevation="3" outlined shaped class="my-4">
                    <v-card-title>Hello!</v-card-title>
                    <v-textarea outlined label="What you think bud?" value="Your thoughts here...">{{ reply.content }}</v-textarea>
                    <v-card-actions>
                    <v-btn @click.native="addReply">Save</v-btn>
                    <v-btn @click="dialog=false">Close</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
            </v-col>
        </v-card>
        </div>
        <v-timeline align-top dense>
        <v-timeline-item v-for="reply in replies" v-bind:key="reply.replyId">
            <template v-slot:icon>
                <v-avatar>
                    <img src="https://images-ext-2.discordapp.net/external/AdJWzJfIdpBJppSLXDGvxWy5Pgs9r4K5IczkHsiLU1g/https/i.ytimg.com/vi/GNc_ZKCmjJ8/maxresdefault.jpg?width=786&height=442">
                </v-avatar>
            </template>
            <v-card elevation="3" outlined shaped class="pa-md-2">

                <v-card-title class="py-0">{{ reply.username }}</v-card-title>
                <v-card-text class="py-0">{{ reply.postedDate | moment }}</v-card-text>
                <v-divider class="mt-0 mx-4"></v-divider>
                <v-card-text>{{ reply.content }}</v-card-text>
            </v-card>
        </v-timeline-item>
        </v-timeline>
        </v-row>
       </v-container>
    </div>
</template>

<script>
import moment from 'moment'
import postService from '../services/PostService.js'


export default {
    data() {
        return {
            posts: [],
            replies: [],
            reply: {
                postId: this.postID,
                content: '',
                username: this.username
            }
        }
    },
    created() {
        postService.getPost().then(
            (resp) => {
                this.posts = resp.data;
            })
    },
    methods: {
        displayReplies() {
            postService.getReplies().then(
            (resp) => {
                this.replies = resp.data;
            })
        },
        addReply() {
             postService.addReply(this.reply).then(
                (resp) =>{
                    if(resp.status === 201){
                         this.reply = { postId: '', username: '', content: ''}
                    }
                 })
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