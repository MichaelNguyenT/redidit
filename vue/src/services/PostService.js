import axios from 'axios';
const http = axios.create({
  baseURL: "https://localhost:44315"
});


export default {

    getPost(forumId) {
        return http.get(`/forum/${forumId}`)
      },

    addPost(post) {
      return http.post('/forum', post)
    },

    getReplies(postId) {
      return http.get(`/post/${postId}`)
    },

    addReply(reply) {
      return http.post('/post', reply)
    },

    updateUpvote(postId) {
      return http.put(`/upvotes${postId}`) //takes postId, all other logic handled in back end
    },

    updateDownvote(postId) {
      return http.put(`/downvotes${postId}`)
    },

    getForum() {
      return http.get('/')
    }
}
