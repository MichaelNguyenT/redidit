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

    updateUpvote(postId, upvoteCounter) {
      return http.put(`/post${postId}/upvotes${upvoteCounter}`) //takes current postId and new number of total upvotes
    },

    updateDownvote(postId, downvoteCounter) {
      return http.put(`/post${postId}/downvotes${downvoteCounter}`)
    },

    getForum() {
      return http.get('/')
    }
}
