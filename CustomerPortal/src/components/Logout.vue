<script>
import Auth from '../services/Auth'
export default {
  name: 'Logout',
  template: '',
  logout() {
        Auth.logout();
    }
}
    
</script>