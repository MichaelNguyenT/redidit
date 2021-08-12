import axios from 'axios';

export default {

    getPost(forumId) {
        return axios.get(`/forum/${forumId}`)
      },

    getSinglePost(postId){
      return axios.get(`/posts/${postId}`)
    },

    addPost(post) {
      return axios.post('/forum', post)
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
    },

    deletePost(postId) {
      return axios.delete(`/forum/${postId}`)
    },

    deleteForum(forumId) {
      return axios.delete(`/forum${forumId}/delete`)
    },

    deleteReply(replyId) {
      return axios.delete(`/post/${replyId}`)
    },

    createForum(forum) {
      return axios.post(`/`, forum)
    },

    getPopularPost() {
      return axios.get('/popular');
    },

    loveForum(userId, forumId) {
      return axios.post(`/favorites/user${userId}/forum${forumId}`);
    },

    getFavoriteForums(userId) {
      return axios.get(`/favorites/${userId}`);
    }
}
