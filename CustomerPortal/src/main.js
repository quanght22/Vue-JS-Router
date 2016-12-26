// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'
import VueResource from 'vue-resource'

import Home from './components/Home'
import About from './components/About'
import App from './App'
import Auth from './services/Auth'
import SignUp from './components/SignUp'
import Login from './components/Login'
Vue.use(VueResource)
Vue.use(VueRouter)
Auth.checkAuth()
const router = new VueRouter({
  mode: 'history',
  base: __dirname,
  routes: [
    { path: '/', name: 'home', component: Home },
    { path: '/about', name: 'about', component: About },
    { path: '/signup', name: 'signup', component: SignUp },
     { path: '/login', name: 'login', component: Login }
  ]
})
/* eslint-disable no-new */
new Vue({
  router,
  el: '#app',
  template: '<App/>',
  components: { App }
}).$mount('#app')
