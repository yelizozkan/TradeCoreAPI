<template>
  <li v-if="cartItems?.length > 0" class="nav-item dropdown">
    <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-bs-toggle="dropdown"
      aria-expanded="false">
      <i class="bi bi-cart"></i> <!-- Sepet ikonu -->
      Sepet <span class="badge bg-primary">{{ cartItems.length }}</span>
    </a>
    <ul class="dropdown-menu dropdown-menu-end p-3" aria-labelledby="navbarDropdown">
      <li v-for="cartItem in cartItems" :key="cartItem.product.productId" class="d-flex justify-content-between align-items-center mb-2">
        <a class="dropdown-item">
          <span class="badge bg-secondary me-2">{{ cartItem.quantity }}</span>
          {{ cartItem.product.productName }}
        </a>
        <!-- "Sil" butonu çöp kutusu ikonu ile -->
        <button @click="handleRemoveFromCart(cartItem.product)" class="btn btn-danger btn-sm">
          <i class="bi bi-trash"></i> Sil
        </button>
      </li>
      <li>
        <hr class="dropdown-divider">
      </li>
      <li>
        <a class="dropdown-item text-center" href="/cart">
          <i class="bi bi-arrow-right-circle"></i> Sepete git
        </a>
      </li>
    </ul>
  </li>
</template>

<script>
import cartService from '@/services/CartService';
import { useToast } from 'vue-toastification';

export default {
  data() {
    return {
      cartItems: []
    };
  },
  setup() {
    const toast = useToast(); 
    return { toast };
  },
  created() {
    this.loadCartItems();
  },
  methods: {
    loadCartItems() {
      const items = cartService.list();
      if (items && Array.isArray(items)) {
        this.cartItems = items;
      } else {
        this.toast.error('Sepet yüklenemedi.', { 
          position: 'top-right',
          timeout: 3000,
        });
      }
    },
    handleRemoveFromCart(product) {
      if (!product) {
        this.toast.error('Hata: Ürün bulunamadı.', { 
          position: 'top-right',
          timeout: 3000,
        });
        return;
      }

      cartService.removeFromCart(product);
      this.loadCartItems();

      this.toast.error(`${product.productName} sepetten silindi.`, { 
        position: 'top-right',
        timeout: 3000,
      });
    }
  }
};
</script>

<style scoped>
.dropdown-menu {
  min-width: 300px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
}

.dropdown-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.dropdown-item:hover {
  background-color: #f8f9fa;
}

.bi-trash {
  font-size: 1.2rem;
}

.nav-link {
  display: flex;
  align-items: center;
}

.bi-cart {
  font-size: 1.5rem;
  margin-right: 5px;
}

.badge {
  font-size: 0.8rem;
  margin-left: 10px;
}



</style>
