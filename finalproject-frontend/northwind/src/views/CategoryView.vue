<!-- <template>
  <div>
    <h1>Category {{ categoryId }}</h1>
    <ul v-if="products.length">
      <li v-for="product in products" :key="product.productId">
        {{ product.productName }} - {{ product.unitPrice }}
      </li>
    </ul>
    <p v-else>No products found for this category.</p>
  </div>
</template>

<script>
import { fetchProductsByCategory } from '../services/productService.js';

export default {
  props: ['categoryId'],  // 'id' yerine 'categoryId' kullanıyoruz
  data() {
    return {
      products: [],
      error: null
    };
  },
  async created() {
    try {
      const result = await fetchProductsByCategory(this.categoryId);
      if (result.success) {
        this.products = result.data;
      } else {
        this.error = result.message;
      }
    } catch (error) {
      console.error('Error fetching products by category:', error);
      this.error = 'Error fetching products by category';
    }
  }
};
</script>

<style>
</style> -->
