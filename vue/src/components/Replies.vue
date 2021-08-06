<template>
    <div>
        <v-timeline align-top dense>
        <v-timeline-item>
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
    </div>
</template>

<script>
import moment from 'moment'

export default {
    props: ['reply'],
     data(){
         return {
            replies: []
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