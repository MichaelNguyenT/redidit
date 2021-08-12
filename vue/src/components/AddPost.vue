<template>
<v-dialogue>
    <v-card>
        <v-card-title>
            <h2>Add a New Post</h2>
        </v-card-title>
        <v-card-text>
            <v-form class="px-3">
                <v-text-field label="Title" v-model="post.postTitle" prepend-icon="mdi-tag" required></v-text-field>
                <v-textarea label="Content" v-model="post.content" prepend-icon="mdi-border-color" required></v-textarea>
                <v-text-field label="Photo Link (Supply your own photo or leave our link to use our default):" v-model="post.imageURL" prepend-icon="mdi-camera-plus"></v-text-field>
                <v-btn flat class="success mx-0 mt-3" @click.native="addPost">Add post</v-btn>
            </v-form>
        </v-card-text>
    </v-card>
  </v-dialogue>
</template>

<script>
import postService from "@/services/PostService"

export default {

    data() {
    return {
      post: {
        forumId: this.$route.params.forumId,
        postTitle: "",
        username: this.$store.state.user.username,
        content: "",
        imageURL: "http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w"
      }
    };
  },

  methods: {
    addPost() {
      postService
        .addPost(this.post)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit('ADD_POST', this.post);
            this.post = {
              forumId: this.$route.params.forumId,
              postTitle: "",
              username: this.$store.state.user.username,
              content: "",
              imageURL: "http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w"
            };
            this.$router.push(`/forum/${this.$route.params.forumId}`);
          }
        })
        .catch(error => {
            console.log(error.response.status);
            alert('Error: The post could not be added to the forum.');
            }
        );
    }
  }

}

</script>
