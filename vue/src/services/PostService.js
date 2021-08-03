import axios from 'axios';

export default {

    getPost() {
        return axios.get('/postdetails')
      }
}
