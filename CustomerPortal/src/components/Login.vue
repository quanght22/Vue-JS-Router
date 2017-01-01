<template>
    <div>
        <h1 class="loginheader">Login</h1>

        <form v-on:submit.prevent="login()">
            <table><tr>
                <td >Username:<span class="usericon"></span></td>
                <td><input v-model="data.body.username" /></td>
            </tr><tr>
                <td>Password:</td>
                <td><input v-model="data.body.password" type="password" /></td>
            </tr><tr>
                <td></td>
                <td><label><input v-model="data.rememberMe" type="checkbox" /> Remember Me</label></td>
            </tr><tr>
                <td></td>
                <td><button type="submit">Login</button></td>
            </tr></table>

            <hr/>
        </form>
    </div>
</template>

<script>
import Auth from '../services/Auth'
export default {
  name: 'Login',
   data() {
            return {
                context: 'login context',

                data: {
                    body: {
                        username: 'quang777@gmail.com',
                        password: 'Qu@nght198412'
                    },
                    rememberMe: false
                },

                error: null
            };
        },

    methods: {
            login() {
                Auth.login(this,{
                    body: this.data.body,
                    rememberMe: this.data.rememberMe,
                    redirect: {name: 'account'},
                    success() {
                        console.log('success ' + this.context);
                    },
                    error(res) {
                        console.log('error ' + this.context);

                        this.error = res.data;
                    }
                });
            }
        }
}
    
</script>