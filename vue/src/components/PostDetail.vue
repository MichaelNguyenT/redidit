<template>
    <div>
        <v-container>
        <v-row no-gutters align="center">
        <v-card elevation="3" outlined shaped>
            <v-card-title class="pa-md-1">A title to rule all titles....</v-card-title>
            <v-card-subtitle class="pa-md-1">UserNameOfUserNames</v-card-subtitle>
            <v-card-subtitle class="pa-md-1">8/3/2021 12:57pm</v-card-subtitle>
            <v-divider class="mt-0 mx-4"></v-divider>
            <v-col class="d-flex justify-space-between align-center">
                <v-img
                    lazy-src="https://picsum.photos/id/11/10/6"
                     max-height="200"
                    max-width="200"
                    src="https://picsum.photos/id/11/500/300"
                ></v-img>
            <v-card-text>Maecenas eu diam facilisis, tincidunt elit at, laoreet dolor. Proin ultrices, ligula quis pellentesque pharetra, lorem est bibendum erat, in fermentum velit purus vel leo. Ut commodo ex vitae scelerisque laoreet. Curabitur nec viverra orci. Sed tempor lorem nec nisi dapibus, id tincidunt urna efficitur. Cras sagittis eros mollis mi faucibus, a accumsan elit rutrum. Donec vitae nibh posuere, condimentum turpis accumsan, posuere eros. Pellentesque faucibus, augue a tincidunt aliquet, tellus tellus interdum purus, ac rhoncus lectus diam eu purus. Praesent aliquam diam eu est varius, a hendrerit mauris laoreet. Aliquam pharetra eros nisl, sed pretium lorem sollicitudin id.Maecenas eu diam facilisis, tincidunt elit at, laoreet dolor. Proin ultrices, ligula quis pellentesque pharetra, lorem est bibendum erat, in fermentum velit purus vel leo. Ut commodo ex vitae scelerisque laoreet. Curabitur nec viverra orci. Sed tempor lorem nec nisi dapibus, id tincidunt urna efficitur. Cras sagittis eros mollis mi faucibus, a accumsan elit rutrum. Donec vitae nibh posuere, condimentum turpis accumsan, posuere eros. Pellentesque faucibus, augue a tincidunt aliquet, tellus tellus interdum purus, ac rhoncus lectus diam eu purus. Praesent aliquam diam eu est varius, a hendrerit mauris laoreet. Aliquam pharetra eros nisl, sed pretium lorem sollicitudin id.</v-card-text>
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
            </v-col>
        </v-card>
        <v-subheader >Replies to this Post...</v-subheader>
        <v-spacer></v-spacer>
        <v-card elevation="3" outlined shaped>
            <v-col class="d-flex justify-start mb-1">
            <v-card-subtitle class="pa-md-1">UserNameOfReplies</v-card-subtitle>
            <v-card-subtitle class="pa-md-1">8/4/2021 1:14am</v-card-subtitle>
            </v-col>
            <v-divider class="mt-0 mx-4"></v-divider>
            <v-card-text>Maecenas eu diam facilisis, tincidunt elit at, laoreet dolor. Proin ultrices, ligula quis pellentesque pharetra, lorem est bibendum erat, in fermentum velit purus vel leo. Ut commodo ex vitae scelerisque laoreet. Curabitur nec viverra orci. Sed tempor lorem nec nisi dapibus, id tincidunt urna efficitur. Cras sagittis eros mollis mi faucibus, a accumsan elit rutrum. Donec vitae nibh posuere, condimentum turpis accumsan, posuere eros. Pellentesque faucibus, augue a tincidunt aliquet, tellus tellus interdum purus, ac rhoncus lectus diam eu purus. Praesent aliquam diam eu est varius, a hendrerit mauris laoreet. Aliquam pharetra eros nisl, sed pretium lorem sollicitudin id.Maecenas eu diam facilisis, tincidunt elit at, laoreet dolor. Proin ultrices, ligula quis pellentesque pharetra, lorem est bibendum erat, in fermentum velit purus vel leo. Ut commodo ex vitae scelerisque laoreet. Curabitur nec viverra orci. Sed tempor lorem nec nisi dapibus, id tincidunt urna efficitur. Cras sagittis eros mollis mi faucibus, a accumsan elit rutrum. Donec vitae nibh posuere, condimentum turpis accumsan, posuere eros. Pellentesque faucibus, augue a tincidunt aliquet, tellus tellus interdum purus, ac rhoncus lectus diam eu purus. Praesent aliquam diam eu est varius, a hendrerit mauris laoreet. Aliquam pharetra eros nisl, sed pretium lorem sollicitudin id.</v-card-text>
        </v-card>
        </v-row>
       </v-container>
    </div>
</template>

<script>

import postService from '../services/PostService.js'

export default {
    data() {
        return {
            posts: []
        }
    },
    created() {
        postService.getPost().then(
            (resp) => {
                this.post = resp.data;
            }
        )
    }

}
</script>

<style>

</style>