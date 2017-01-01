<template id="home-template">
    <div v-if="items">
      <ul>
        <li v-for="item in items">
            {{ item }}
        </li>
    </ul> 
    </div>
</template>
<script>
import Vue from 'vue'
import VueResource from 'vue-resource'
import Auth from '../services/Auth'
Vue.use(VueResource);
const API_URL = 'http://localhost:54895'
const HOME_URL = API_URL + '/api/user'
export default {
    name: 'Home',
    template: '#home-template',
    data: function () {
        return {
            items: null
            }
    },
    created: function () {
        this.GetListUser();
    },
    methods: {
        GetListUser: function (){
            Vue.http.options.headers = {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin':'*'
            }
            Vue.http.get(HOME_URL,{
                headers: Auth.getAuthHeader()
            }).then(
              (response) => {
                this.items = response.data;
              },
              (response) => {
                   console.error(response);
              }
          )
        }
    }
  }
</script>