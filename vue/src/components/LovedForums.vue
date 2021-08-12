<template>
<v-card class="d-flex mx-15 mb-15">
            <v-carousel>
                <v-carousel-item v-for="forum in this.$store.state.favoriteForums" v-bind:key="forum.forumId">
                 <v-row class="fill-height">
                    <v-col align="center" justify="center">
                        <v-img :src = forum.forumPicture      max-height="300" max-width="300" class="ma-4">
                        </v-img>
                        <h1 class="ma-2">
                        <router-link  v-bind:to="{name: 'post-details', params: { forumId: forum.forumId }}" class="secondary--text" @click.native="setTitle(forum.forumTitle)">{{ forum.forumTitle }}</router-link>
                        </h1>
                    </v-col>
                </v-row>
            </v-carousel-item>    
        </v-carousel>
</v-card>
</template>

<script>

export default {
    name: 'loved-forums',
    data() {
        return {
            forums: [],
        }
    },
    // created() {
    //     postService.getFavoriteForums(this.$store.state.user.userId).then(
    //         (resp) => {
    //             this.forums = resp.data;
    //         })
    // }
}