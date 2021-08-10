import axios from 'axios';

export default {

    getPost(forumId) {
        return axios.get(`/forum/${forumId}`)
      },

    addPost(post) {
      return http.post('/forum', post)
    },

    getReplies(postId) {
      return axios.get(`/post/${postId}`)
    },

    addReply(reply) {
      return axios.post('/post', reply)
    },

    updateUpvote(postId) {
      return axios.put(`/upvotes${postId}`) //takes postId, all other logic handled in back end
    },

    updateDownvote(postId) {
      return axios.put(`/downvotes${postId}`)
    },

    getForum() {
      return axios.get('/')
    }
}
