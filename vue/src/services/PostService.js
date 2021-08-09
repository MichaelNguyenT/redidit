import axios from 'axios';
const http = axios.create({
  baseURL: "https://localhost:44315"
});


export default {

    getPost() {
        return http.get(`/forum/${1}`)
      },

    getReplies() {
      return http.get(`/post/${1}`)
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
