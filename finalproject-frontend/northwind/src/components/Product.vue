<template>
  <div>
    <div v-if="!dataLoaded" class="spinner-border" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>

    <div class="mb-3">
      <label for="filterText" class="form-label">Product search</label>
      <input type="text" class="form-control" id="filterText" placeholder="enter search expression"
        v-model="filterText" />
    </div>

    <div v-if="filterText.length > 2" class="alert alert-success">
      {{ filterText }} aradınız.
    </div>

    <table v-if="dataLoaded" class="table table-striped table-bordered">
      <thead>
        <tr>
          <th>ID</th>
          <th>Product Name</th>
          <th>Category</th>
          <th>Unit Price</th>
          <th>Price with VAT</th>
          <th>Units In Stock</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in filteredProducts" :key="product.productId"
          v-if="Array.isArray(filteredProducts) && filteredProducts.length > 0">
          <td>{{ product.productId }}</td>
          <td>{{ product.productName }}</td>
          <td>{{ product.categoryId }}</td>
          <td>{{ formatCurrency(product.unitPrice) }}</td>
          <td>{{ formatCurrency(getPriceWithVAT(product.unitPrice)) }}</td>
          <td>{{ product.unitsInStock }}</td>
          <td><button v-on:click="addToCart(product)" type="button" class="btn btn-success">Add to cart</button></td>
        </tr>
      </tbody>
    </table>

    <div v-if="products.length === 0 && dataLoaded" class="alert alert-warning">
      No products found.
    </div>

    <nav aria-label="Page navigation">
      <ul class="pagination">
        <li class="page-item" :class="{ disabled: currentPage === 1 }">
          <a class="page-link" @click="changePage(currentPage - 1)" aria-label="Previous">
            <span aria-hidden="true">&laquo;</span>
          </a>
        </li>
        <li v-for="page in totalPages" :key="page" class="page-item" :class="{ active: page === currentPage }">
          <a class="page-link" @click="changePage(page)">{{ page }}</a>
        </li>
        <li class="page-item" :class="{ disabled: currentPage === totalPages }">
          <a class="page-link" @click="changePage(currentPage + 1)" aria-label="Next">
            <span aria-hidden="true">&raquo;</span>
          </a>
        </li>
      </ul>
    </nav>


  </div>
</template>

<script>
import { fetchProducts, fetchProductsByCategory } from '../services/productService.js';
import { useToast } from 'vue-toastification';
import cartService from '@/services/CartService';


export default {
  data() {
    return {
      products: [],
      dataLoaded: false,
      error: null,
      filterText: '',
      currentPage: 1,
      pageSize: 10,
      totalProducts: 0,
      totalPages: 0
    };
  },
  methods: {
    async loadProducts() {
      try {
        let result;
        if (this.$route.params.categoryId) {
          result = await fetchProductsByCategory(this.$route.params.categoryId, this.currentPage, this.pageSize);
        } else {
          result = await fetchProducts(this.currentPage, this.pageSize);
        }

        if (result.success) {
          this.products = result.data.products || [];
          this.totalProducts = result.data.totalCount || 0;
          this.totalPages = Math.ceil(this.totalProducts / this.pageSize);
          this.dataLoaded = true;
        } else {
          this.error = result.message;
        }
      } catch (error) {
        console.error('Error fetching products:', error);
        this.error = 'Error fetching products';
      }
    },

    async changePage(page) {
      if (page < 1 || page > this.totalPages) return;
      this.currentPage = page;
      await this.loadProducts();
    },

    async changeCategory(categoryId) {
      this.$router.push({ name: 'product-list', params: { categoryId } });
      this.currentPage = 1;
      this.loadProducts();
    },


    getPriceWithVAT(price) {
      const vatRate = 0.18; // 18% VAT
      return price * (1 + vatRate);
    },

    formatCurrency(value) {
      return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
    },

    addToCart(product) {
      this.$toast.success(`${product.productName} eklendi`, {
        position: 'top-right',
        timeout: 3000,
      });
      cartService.addToCart(product);
    },

    filterProducts(products, filterText) {
      filterText = filterText ? filterText.toLowerCase() : "";
      return filterText
        ? products.filter(product =>
          product.productName.toLowerCase().includes(filterText)
        )
        : products;
    }
  },

  computed: {
    filteredProducts() {
      return this.filterProducts(this.products, this.filterText);
    }
  },
  async created() {
    await this.loadProducts();
  },
  watch: {
    '$route.params.categoryId': {
      handler() {
      this.currentPage = 1;  
      this.loadProducts();   
    },
      immediate: true
    },
  },
  mounted() {
    this.$toast = useToast();
  }
};
</script>

<style scoped></style>
