<script>
import Vue from 'vue'
const API_URL = 'http://localhost:54895'
const LOGIN_URL = API_URL + '/Account/Create/'
const SIGNUP_URL = API_URL + '/Account/Register'
const GETINFOR_URL = API_URL + '/Account/GetInfor'
export default {
  name: 'Auth',
    user: {
      authenticated: false
    },
    login(context, creds, redirect) {
      localStorage.setItem('id_token','xxxxxxxxxxxxxxxxx')
      this.user.authenticated = true
      // context.$http.post(LOGIN_URL, creds, (data) => {
      //   localStorage.setItem('id_token', data.id_token)

      //   this.user.authenticated = true

      //   if(redirect) {
      //     router.go(redirect)        
      //   }

      // }).error((err) => {
      //   context.error = err
      // })
    },
    signup(context, creds, redirect) {
         Vue.http.post(SIGNUP_URL, JSON.stringify(creds)).then(
              (response) => {
               debugger;
                  localStorage.setItem('id_token', response.data.token)
                  Vue.http.headers.common['Authorization'] = this.getAuthHeader();
                  succes(response.data)
              },
              (response) => {
                  error(response)
              }
          )
    //   Vue.http.post(SIGNUP_URL, JSON.stringify(creds),{
    //   headers: {
    //         "Access-Control-Allow-Origin": "*",
    //         "Access-Control-Allow-Credentials": true
    //      }
    //     }).then(
    //     (response) => {
    //         localStorage.setItem('id_token', response.data.token)
    //         Vue.http.headers.common['Authorization'] = this.getAuthHeader();
    //         succes(response.data)
    //     },
    //     (response) => {
    //         error(response)
    //     }
    // )
    },

    logout() {
      localStorage.removeItem('id_token')
      this.user.authenticated = false
    },

    checkAuth() {
      var jwt = localStorage.getItem('id_token')
      if(jwt) {
        this.user.authenticated = true
      }
      else {
        this.user.authenticated = false      
      }
    },


    getAuthHeader() {
      return {
        'Authorization': 'Bearer ' + localStorage.getItem('id_token')
      }
    }

}
</script>
