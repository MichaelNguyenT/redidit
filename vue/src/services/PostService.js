import axios from 'axios';
const http = axios.create({
  baseURL: "https://localhost:44315"
});


export default {

    getPost(forumId) {
        return http.get(`/forum/${forumId}`)
      },

    getReplies(postId) {
      return http.get(`/post/${postId}`)
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
