<script>
import Vue from 'vue'
import {router} from '../main'
const API_URL = 'http://localhost:54895'
const SIGNUP_URL = API_URL + '/Account/Register'
const LOGIN_URL = API_URL + '/connect/token'
export default {
  name: 'Auth',
    user: {
      authenticated: false
    },
    login(context, creds, redirect) {
       var data = JSON.stringify(creds.body);
        data = JSON.parse(data);
        this.authentication(context, data, redirect);
    },
    authentication(context, data, redirect) {
      Vue.http.options.emulateJSON = true
      Vue.http.options.headers = {
        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
      }
      Vue.http.options.beforeSend = function(request) {
          request.headers['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
      };
      Vue.http.post(LOGIN_URL,{
        grant_type: 'password',
        username: data.username,
        password: data.password,
        scope: 'openid+email+name+profile+roles',
      }
      ).then(
            (response) => {
                localStorage.setItem('auth-tokens-expiration',this.getExpirationDate(response.data.expires_in));
                localStorage.setItem('auth-tokens',JSON.stringify(response.data))
                Vue.http.headers.common['Authorization'] = this.getAuthHeader();
                // router.push('/');
                //  router.go('/Home') 
                  router.push('/');
            },
            (response) => {
                error(response)
            }
        )
    },
    getExpirationDate(expires_in){
      var now = new Date();
      return new Date(now.getTime() + expires_in * 1000).getTime();
    },
    signup(context, creds, redirect) {
        var data = JSON.stringify(creds);
         Vue.http.post(SIGNUP_URL, data).then(
              (response) => {
                  this.authentication(context, JSON.parse(data), redirect);
              },
              (response) => {
                  error(response)
              }
          )
    },

    logout() {
      localStorage.removeItem('auth-tokens')
      localStorage.removeItem('auth-tokens-expiration')
      this.user.authenticated = false
      router.go('/Login') 
    },

    checkAuth() {
      var tokens_expiration = JSON.parse(localStorage.getItem('auth-tokens-expiration'));
      if (tokens_expiration > new Date().getTime()) {
        localStorage.setItem('auth-tokens-expiration',this.getExpirationDate(JSON.parse(localStorage.getItem('auth-tokens')).expires_in));
        this.user.authenticated = true;
        return true;
      }
      this.user.authenticated = false
      return false;
    },
    getAuthHeader() {
      var authTokens = JSON.parse(localStorage.getItem('auth-tokens'));
      if(authTokens) {
        return {
          'Authorization': 'Bearer ' + authTokens.access_token
        }
      }
      return null;
      
    }

}
</script>
