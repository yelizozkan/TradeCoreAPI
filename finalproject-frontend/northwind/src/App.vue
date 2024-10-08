<template>
  <div class="container">
    <div id="app">
      <Navbar />
    <div class="row mt-4">
      <div class="col-md-3">
        <Category/>
      </div>
      <div class="col-md-9">
        <router-view />
      </div>
  </div>
</div>
  
  </div>
</template>

<script>
import { Modal } from 'bootstrap';
import Navbar from './components/Navbar.vue'
import Category from './components/Category.vue'
import Product from './components/Product.vue'

export default {
  components: {
    Navbar,
    Category,
  },
}
</script>

<style>
</style>
