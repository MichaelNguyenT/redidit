import axios from 'axios';
const http = axios.create({
  baseURL: "https://localhost:44315"
});


export default {

    getPost() {
        return http.get(`/forum/${1}`)
      },

    getReplies() {
      return http.get(`/post/${2}`)
    }
}
