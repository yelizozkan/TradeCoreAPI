<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container-fluid">
      <router-link class="navbar-brand" to="/">Anasayfa</router-link>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link class="nav-link" :class="{ active: $route.name === 'login' }" to="/login">Giriş Yap</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" :class="{ active: $route.name === 'productAdd' }" to="/products/add">Ürün Ekle</router-link>
          </li>
          
          <li class="nav-item">
            <CartSummary></CartSummary>
          </li>
          
          <li class="nav-item">
            <a class="nav-link disabled" aria-disabled="true">Disabled</a>
          </li>
        </ul>
        <form class="d-flex" role="search">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form>
      </div>
    </div>
  </nav>
</template>

<script>
import CartSummary from './CartSummary.vue';

export default {
  name: 'Navbar',
  components: {
    CartSummary 
  }
};
</script>

<style scoped>
</style>
