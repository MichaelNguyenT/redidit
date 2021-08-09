<template>
<v-card class="mx-auto">
    <v-toobar>
        <v-toolbar-title class="font-weight-bold secondary h1 ma-8 pa-3">Top Post Now</v-toolbar-title>
        <v-spacer></v-spacer>
    </v-toobar>
            <v-carousel>
                <v-carousel-item v-for="post in posts" v-bind:key="post.postId">
                  <v-row class="fill-height" align="center" justify="center">
                  </v-row>
            </v-carousel-item>    
        </v-carousel>
</v-card>
</template>


<script>
export default {
    name: "top-post",
}
</script>

<style>

</style>