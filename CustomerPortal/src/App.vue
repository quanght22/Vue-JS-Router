<template>
  <div id="app">
    <nav class="navbar navbar-default navbar-static-top">
        <div class="container">
          <div class="navbar-header">
              <a class="navbar-brand">Customer Portal</a>
          </div>
          <div id="navbar" class="navbar-collapse collapse">
              <ul class="nav navbar-nav">
                <li><router-link :to="{ name: 'home' }">Home</router-link></li>
                <li><router-link :to="{ name: 'signup' }">Sign Up</router-link></li>
                <li><router-link :to="{ name: 'login' }">Login</router-link></li>
                <li><router-link v-if="user.authenticated" :to="{ name: 'about' }">About</router-link></li>
              </ul>
          </div>
        </div>
    </nav>
    <div class="container">      
        <router-view></router-view>
    </div>
  </div>
</template>

<script>
  import Home from './components/Home'
  import About from './components/About'
  import SignUp from './components/SignUp'
  import Login from './components/Login'
  import Auth from './services/Auth'
  export default {
    name: 'App',
    data() {
      return {
        user: Auth.user
      }
    },
    components: { Home , About,SignUp,Login}
  }
</script>


