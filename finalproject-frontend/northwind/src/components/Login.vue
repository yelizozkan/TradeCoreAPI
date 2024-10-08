<template>
  <div class="d-flex align-items-center py-4 bg-body-tertiary">
    <main class="form-signin w-100 m-auto">
      <form @submit.prevent="login">
        <h1 class="h3 mb-3 fw-normal">Giriş yap</h1>

        <div class="form-floating">
          <input v-model="loginForm.email" type="email" class="form-control" id="floatingInput"
            placeholder="name@example.com" :class="{ 'is-invalid': emailError }" />
          <label for="floatingInput">E-posta</label>
          <div v-if="emailError" class="invalid-feedback">
            E-posta gereklidir.
          </div>
        </div>

        <div class="form-floating">
          <input v-model="loginForm.password" type="password" class="form-control" id="floatingPassword"
            placeholder="Password" :class="{ 'is-invalid': passwordError }" />
          <label for="floatingPassword">Parola</label>
          <div v-if="passwordError" class="invalid-feedback">
            Parola gereklidir.
          </div>
        </div>

        <div class="checkbox mb-3">
          <label>
            <input type="checkbox" v-model="rememberMe" /> Beni Hatırla
          </label>
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">
          Giriş yap
        </button>
        <p class="mt-5 mb-3 text-muted">&copy; 2024–2028</p>
      </form>
    </main>
  </div>
</template>

<script>
import { useToast } from 'vue-toastification'; 
import { authService } from '../services/authService';

export default {
  name: 'Login',
  data() {
    return {
      loginForm: {
        email: '',
        password: '',
      },
      rememberMe: false,
      emailError: false,
      passwordError: false,
      isLoading: false,
    };
  },
  methods: {
    validateForm() {
      this.emailError = !this.loginForm.email;
      this.passwordError = !this.loginForm.password;
      return !this.emailError && !this.passwordError;
    },
    async login() {
      if (this.validateForm()) {
        this.isLoading = true;
        const toast = useToast(); 

        try {
        const response = await authService.login(this.loginForm);

        if (response && response.success) {
          const token = response.data.token;
          localStorage.setItem('token', token);

          console.log('Token:', token); 

          toast.success(response.message); 
        } else {
          toast.error(response.message || 'Bir hata oluştu');
        }
      } catch (error) {
        console.error('Full error object:', error);

        let errorMessage = 'Giriş başarısız';
        if (error.response) {
          errorMessage = error.response.data?.message || 'Sunucu yanıtı mevcut değil';
        } else if (error.request) {
          errorMessage = 'Sunucuya ulaşılamıyor';
        } else {
          errorMessage = error.message;
        }

        toast.error(errorMessage); 
        } finally {
          this.isLoading = false;
        }
      }
    }
  }
};
</script>



<style scoped>
html,
body {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.form-signin {
  max-width: 330px;
  padding: 1rem;
  text-align: center;
}

.form-signin .form-floating:focus-within {
  z-index: 2;
}

.form-signin input[type="email"] {
  margin-bottom: -1px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}

.form-signin input[type="password"] {
  margin-bottom: 10px;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
}

.form-check {
  text-align: center;
  
}

.text-body-secondary {
  text-align: center;
  
}

.b-example-divider {
  width: 100%;
  height: 3rem;
  background-color: rgba(0, 0, 0, .1);
  border: solid rgba(0, 0, 0, .15);
  border-width: 1px 0;
  box-shadow: inset 0 .5em 1.5em rgba(0, 0, 0, .1), inset 0 .125em .5em rgba(0, 0, 0, .15);
}

.b-example-vr {
  flex-shrink: 0;
  width: 1.5rem;
  height: 100vh;
}

.bi {
  vertical-align: -.125em;
  fill: currentColor;
}

.nav-scroller {
  position: relative;
  z-index: 2;
  height: 2.75rem;
  overflow-y: hidden;
}

.nav-scroller .nav {
  display: flex;
  flex-wrap: nowrap;
  padding-bottom: 1rem;
  margin-top: -1px;
  overflow-x: auto;
  text-align: center;
  white-space: nowrap;
  -webkit-overflow-scrolling: touch;
}

.btn-bd-primary {
  --bd-violet-bg: #712cf9;
  --bd-violet-rgb: 112.520718, 44.062154, 249.437846;

  --bs-btn-font-weight: 600;
  --bs-btn-color: var(--bs-white);
  --bs-btn-bg: var(--bd-violet-bg);
  --bs-btn-border-color: var(--bd-violet-bg);
  --bs-btn-hover-color: var(--bs-white);
  --bs-btn-hover-bg: #6528e0;
  --bs-btn-hover-border-color: #6528e0;
  --bs-btn-focus-shadow-rgb: var(--bd-violet-rgb);
  --bs-btn-active-color: var(--bs-btn-hover-color);
  --bs-btn-active-bg: #5a23c8;
  --bs-btn-active-border-color: #5a23c8;
}

.bd-mode-toggle {
  z-index: 1500;
}

.bd-mode-toggle .dropdown-menu .active .bi {
  display: block !important;
}
</style>