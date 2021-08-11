<template>
<v-dialogue>
    <v-card>
        <v-card-title>
            <h2>Add a New Post</h2>
        </v-card-title>
        <v-card-text>
            <v-form class="px-3">
                <v-text-field label="Title" v-model="post.postTitle" prepend-icon="tag" required></v-text-field>
                <v-textarea label="Content" v-model="post.content" prepend-icon="edit" required></v-textarea>
                <v-text-field label="ImageURL" v-model="post.imageURL" prepend-icon="camera-plus"></v-text-field>
                <v-btn flat class="success mx-0 mt-3" @click="addPost">Add post</v-btn>
            </v-form>
        </v-card-text>
    </v-card>
  </v-dialogue>
</template>

<script>
import postService from "..services/PostService"

export default {

    data() {
    return {
      post: {
        forumId: this.$route.params.forumId,
        postTitle: "",
        username: this.$store.state.user.username,
        content: "",
        imageURL: ""
      }
    };
  },

  methods: {
    addPost() {
      postService
        .addPost(this.post)
        .then(response => {
          if (response.status == 201) {
            this.$store.commit('ADD_POST', this.post);
            this.$router.push(`/forum/${this.$route.params.forumId}`);
          }
        })
        .catch(error => {
            alert('Error: The post could not be added to the forum.');
            }
        );
    }
  }

}

</script>
