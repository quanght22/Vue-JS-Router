<template>
  <div id="app">
    <nav class="navbar navbar-default navbar-static-top">
        <div class="container">
          <div class="navbar-header">
              <a class="navbar-brand color">Customer Portal</a>
          </div>
          <div id="navbar" class="navbar-collapse collapse">
              <ul class="nav navbar-nav">
                <li><router-link v-if="user.authenticated" :to="{ name: 'home' }">Home</router-link></li>
                <li><router-link v-if="user.authenticated" :to="{ name: 'about' }">About</router-link></li>
                <li><router-link :to="{ name: 'signup' }">Sign Up</router-link></li>
                <li><router-link v-if="!user.authenticated" :to="{ name: 'login' }">Login</router-link></li>
                <li><router-link v-if="user.authenticated" v-on:click.native="Logout" to="/">Logout</router-link></li>
              </ul>
          </div>
        </div>
    </nav>
    <div class="container">      
        <router-view></router-view>
    </div>
    <modal :show.sync="showModal"></modal>
</template>
  </div>
  

<script>
  import css from './assets/css/Site.scss';
  import Home from './components/Home'
  import About from './components/About'
  import SignUp from './components/SignUp'
  import Login from './components/Login'
  import Logout from './components/Logout'
  import modal from './components/modal'
  import Auth from './services/Auth'
  export default {
    name: 'App',
    data() {
      return {
        user: Auth.user,
        showModal: false
      }
    },
     methods: {
      Logout() {
        this.showModal = true;
        //Logout.logout()
      }
    },
    components: { Home , About,SignUp,Login,Logout,modal}
  }
</script>


