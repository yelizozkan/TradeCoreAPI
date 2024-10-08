import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; 
import axios from 'axios';
import Toast, { POSITION } from 'vue-toastification';
import 'vue-toastification/dist/index.css'; 

import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import 'bootstrap/dist/css/bootstrap.min.css';


const app = createApp(App);

export function useToast() {
  return app.config.globalProperties.$toast;
}

axios.interceptors.response.use(
  response => response,
  error => {
    console.error('Global error interceptor:', error);

    const errorMessage = error?.response?.data?.message || 'An error occurred';
    useToast().error(errorMessage); 

    return Promise.reject(error);
  }
);

app.use(Toast, {
  position: POSITION.TOP_RIGHT, 
  timeout: 5000, 
  closeOnClick: true, 
  pauseOnHover: true, 
  draggable: true, 
  draggablePercent: 0.6, 
  showCloseButtonOnHover: true, 
  hideProgressBar: false, 
});

app.use(router); 
app.mount('#app');
