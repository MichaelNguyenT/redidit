import axios from 'axios';

const http = axios.create({
  baseURL: "https://localhost:44315"
});

export default {

  login(user) {
    return http.post('/login', user)
  },

  register(user) {
    return http.post('/register', user)
  },

}
