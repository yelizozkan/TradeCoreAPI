import { createRouter, createWebHistory } from 'vue-router';
import Product from '../components/Product.vue';
import ProductAdd from '@/components/ProductAdd.vue';
import Login from '@/components/Login.vue';
import AboutView from '../views/AboutView.vue'; 

const routes = [
  {
    path: '/',
    name: 'home',
    component: Product  
  },
  {
    path: '/products',
    name: 'products',
    component: Product
  },
  {
    path: '/products/category/:categoryId',
    name: 'category',
    component: Product, 
  },
  {
    path: '/products/add',
    name: 'productAdd',
    component: ProductAdd
  },
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/about',
    name: 'about',
    component: AboutView 
  },
  {
    path: '/:pathMatch(.*)*', // Herhangi bir başka yol için
    redirect: '/' // Kök yola yönlendirme
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
