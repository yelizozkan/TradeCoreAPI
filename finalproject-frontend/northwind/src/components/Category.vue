<template>
  <div>
    
    <div v-if="!dataLoaded" class="spinner-border" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>

    
    <ul v-if="dataLoaded" class="list-group">
      
      <router-link 
        :to="{ name: 'products' }" 
        :class="!currentCategory ? 'list-group-item active' : 'list-group-item'"
        @click="setCurrentCategory(null)">
        Tüm Ürünler
      </router-link>

      
      <router-link 
        v-for="category in categories"
        :key="category.categoryId"
        :to="{ name: 'category', params: { categoryId: category.categoryId } }"
        :class="getCurrentCategoryClass(category)"
        @click="setCurrentCategory(category)">
        {{ category.categoryName }}
      </router-link>
    </ul>

    
    <h4 v-if="currentCategory">
      {{ currentCategory.categoryName }} seçtiniz
    </h4>
  </div>
</template>

<script>
import { fetchCategories } from '../services/categoryService.js';

export default {
  data() {
    return {
      categories: [],
      currentCategory: null,
      dataLoaded: false, 
      error: null
    };
  },
  async created() {
    this.loadCategories();
  },
  watch: {
    
    '$route.params.categoryId'(newCategoryId) {
      this.setCurrentCategoryById(newCategoryId);
    },
  },
  methods: {
    async loadCategories() {
      this.dataLoaded = false; 
      try {
        const result = await fetchCategories();
        if (result.success) {
          this.categories = result.data;
          
          if (this.$route.params.categoryId) {
            this.setCurrentCategoryById(this.$route.params.categoryId);
          }
        } else {
          this.error = result.message;
        }
      } catch (error) {
        console.error('Error fetching categories:', error);
        this.error = 'Error fetching categories';
      } finally {
        this.dataLoaded = true; 
      }
    },
    setCurrentCategory(category) {
      this.currentCategory = category;
    },
    setCurrentCategoryById(categoryId) {
      const selectedCategory = this.categories.find(cat => cat.categoryId === parseInt(categoryId));
      this.currentCategory = selectedCategory || null;
    },
    getCurrentCategoryClass(category) {
      return category === this.currentCategory ? "list-group-item active" : "list-group-item";
    },
  }
};
</script>

<style scoped>
</style>
