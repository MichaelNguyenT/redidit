import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    currentForum: '',
    upvoteCounter: 0,
    downvoteCounter: 0,
    currentPosts: [],
    currentReplies: []
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    SET_CURRENT_FORUM(state, forumName) {
      state.currentForum = forumName;
    },
    SET_CURRENT_POSTS(state, posts) {
      state.currentPosts = posts;
    },
    ADD_POST(state, newPost) {
      state.currentPosts.push(newPost);
    },
    SET_CURRENT_REPLIES(state, replies){
      state.currentReplies = replies;
    },
    ADD_REPLY(state, newReply) {
      state.currentReplies.find(element => element.postId == newReply.postId)
    },
    SET_VOTE_COUNTERS(state, updateObject){
      let postToChange = state.currentPosts.find(element => element.postId == updateObject.postId)
      if (updateObject.response == 'plusminus') { //first is upvote second is downvote, no means no change
        postToChange.upvoteCounter = postToChange.upvoteCounter + 1
        postToChange.downvoteCounter = postToChange.downvoteCounter - 1
      }
      else if (updateObject.response == 'minusno') {
        postToChange.upvoteCounter = postToChange.upvoteCounter - 1
      }
      else if (updateObject.response == 'plusno') {
        postToChange.upvoteCounter = postToChange.upvoteCounter + 1
      }
      else if (updateObject.response == 'nominus') {
        postToChange.downvoteCounter = postToChange.downvoteCounter - 1
      }
      else if (updateObject.response == 'minusplus') {
        postToChange.upvoteCounter = postToChange.upvoteCounter - 1
        postToChange.downvoteCounter = postToChange.downvoteCounter + 1
      }
      else if (updateObject.response == 'noplus') {
        postToChange.downvoteCounter = postToChange.downvoteCounter + 1
      }
    }
  }
})
