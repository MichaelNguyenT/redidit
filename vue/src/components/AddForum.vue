<template>
    <v-dialogue>
    <v-card>
        <v-card-title>
            <h2>Create a New Forum</h2>
        </v-card-title>
        <v-card-text>
            <v-form class="px-3">
                <v-text-field label="Forum Name" v-model="forum.forumTitle" prepend-icon="" required></v-text-field>
                <v-text-field label="Photo Link (Supply your own photo or leave our link to use our default):" v-model="forum.forumPicture" prepend-icon="camera-plus"></v-text-field>
                <v-btn flat class="success mx-0 mt-3" @click.native="createForum()">Create</v-btn>
            </v-form>
        </v-card-text>
    </v-card>
  </v-dialogue>
</template>

<script>

import PostService from '@/services/PostService.js'

export default {

    data() {
    return {
      forum: {
        forumTitle: "",
        forumPicture: "http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w"
      }
    };
  },

  methods: {
      createForum() {
        PostService
            .createForum(this.forum)
            .then(response => {
                if (response.status == 200) {
                    this.forum = {
                        forumTitle: "",
                        forumPicture: "http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w"
                    }
                    alert('Forum succesfully created!');
                    this.$router.push('/');
                }
            })
            .catch(error => {
                console.log(error.response.status);
                alert('Error: The forum could not be created.')
            });

      }
  }
}

</script>
