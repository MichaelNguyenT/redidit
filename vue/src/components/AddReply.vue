<template>
   <v-dialog>
        <template v-slot:activator="{ on, attrs }">
            <v-chip v-if="$store.state.token != ''" class="ma-1" v-bind="attrs" v-on="on">Add Your Thoughts</v-chip>
        </template>
            <v-card elevation="3" outlined shaped class="my-4">
                <v-card-title>Hello!</v-card-title>
                <v-textarea outlined label="What you think bud?" v-model="reply.content" required></v-textarea>
                <v-card-actions>
                <v-btn @click.native="addReply">Save</v-btn>
                </v-card-actions>
            </v-card>
    </v-dialog>
</template>

<script>
import postService from "@/services/PostService"
export default {
    data() {
        return {
            reply: {
                postId: 1,
                username: this.$store.state.user.username,
                content: ""
            }
        };
    },

    methods: {
        addReply() {
            postService.addReply(this.reply).then(response => {
                if(response.status == 200){
                    this.$store.commit('ADD_REPLY', this.reply);
                    this.$router.push(`/forum/${this.$route.params.forumId}`);
                }
            })
            .catch(error => {
                console.log(error.response.status);
                alert(`Error: The reply could not be added to the post.`);
            })
        }
    }
}
</script>

<style>

</style>