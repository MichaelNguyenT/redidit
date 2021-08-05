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
      return http.put(`/upvote-counter/${1}`, { postId : postId, upvoteCounter : upvoteCounter }) //takes postId at end of route
    },

    updateDownvote(postId, downvoteCounter) {
      return http.put(`/downvote-counter/${1}`, { postId : postId, downvoteCounter : downvoteCounter })
    }
}
