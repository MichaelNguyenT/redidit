import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'


Vue.use(Vuetify)

Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

const opts = {
  theme: {
    dark: false,
    themes: {
      light: {
        primary: '#d300c7',
        secondary: '#18FFFF',
        accent: '#e70e5b',
        error: '#e32828',
      },
      dark: {
        primary: '#18FFFF',
        secondary: '#d300c7',
        accent: '#e70e5b',
        error: '#e32828',
      },
    },
  }
}
Vue.config.productionTip = false
Vue.use(Vuetify)
new Vue({
  router,
  store,
  vuetify : new Vuetify(opts),
  render: h => h(App),
}).$mount('#app')

