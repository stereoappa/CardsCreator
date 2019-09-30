import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
//import Models from './models'


Vue.config.productionTip = false
//Vue.prototype.Models = Models

Vue.use(VueAxios, axios)

new Vue({
  render: h => h(App),
}).$mount('#app')
