<template>
    <div class="secondary">
        <post-detail />
        <replies />
        </div>
</template>

<script>
import Replies from "../components/Replies.vue"
import PostDetail from "../components/PostDetail.vue"
export default {
    name: "post-details",
    components: {
        PostDetail,
        Replies
    }

}
</script>

<style>

</style>