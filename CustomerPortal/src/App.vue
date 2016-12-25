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
                <li><router-link :to="{ name: 'about' }">about</router-link></li>
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
  //import Vue from 'vue'
  // import VueRouter from 'vue-router'
  // import Hello from './components/Hello'
  // import Home from './components/Home'
  // import About from './components/About'
  // Vue.use(VueRouter)
  // const router = new VueRouter({
  //   mode: 'history',
  //   base: __dirname,
  //   routes: [
  //     { path: '/', name: 'home', component: Home },
  //     { path: '/about', name: 'about', component: About }
  //   ]
  // })
  import Home from './components/Home'
  import About from './components/About'
  export default {
    name: 'App',
    components: { Home , About}
  }
</script>


