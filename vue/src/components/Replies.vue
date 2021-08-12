<template>
    <div>
        <v-timeline align-top dense class="mx-15">
        <v-timeline-item>
            <template v-slot:icon>
                <v-avatar>
                    <img src="https://images-ext-2.discordapp.net/external/AdJWzJfIdpBJppSLXDGvxWy5Pgs9r4K5IczkHsiLU1g/https/i.ytimg.com/vi/GNc_ZKCmjJ8/maxresdefault.jpg?width=786&height=442">
                </v-avatar>
            </template>
            <v-card elevation="3" outlined shaped class="pa-ma-4">
                <v-card-title class="py-0">{{ reply.username }}</v-card-title>
                <v-card-text class="py-0">{{ reply.postedDate | moment }}</v-card-text>
                <v-divider class="mt-0 mx-4"></v-divider>
                <v-card-text>{{ reply.content }}</v-card-text>
                <v-chip v-if="$store.state.user.role == 'admin'"  class="pa-1 ma-1" v-on:click="deleteReply(reply.replyId)">Delete Reply</v-chip>
            </v-card>
        </v-timeline-item>
        </v-timeline>
    </div>
</template>

<script>
import moment from 'moment'
import postService from '../services/PostService'

export default {
    props: ['reply'],
     data(){
         return {
            replies: []
         }
     },
    methods: {
        deleteReply(replyId) {
            if (
        confirm("Are you sure you want to delete this card? This action cannot be undone.")
        ){
            postService.deleteReply(replyId)
            .then((response) => {
                if (response.status === 204) {
              alert("Reply has been deleted");
              window.location.reload();
            }
            })
        }
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