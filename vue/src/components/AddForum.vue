<template>
    <v-dialogue>
    <v-card>
        <v-card-title>
            <h2>Create a New Forum</h2>
        </v-card-title>
        <v-card-text>
            <v-form class="px-3">
                <v-text-field label="Forum Name" v-model="forum.forumTitle" prepend-icon="" required></v-text-field>
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
        forumTitle: ""
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
                        forumTitle: ""
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
