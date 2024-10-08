import { reactive } from 'vue';
import CartItem from '@/models/CartItem';

const state = reactive({
  cartItems: []
});

export default {
  addToCart(product) {
    let item = state.cartItems.find(c => c.product.productId === product.productId);
    if (item) {
      item.quantity += 1;
    } else {
      let cartItem = new CartItem(product, 1);
      state.cartItems.push(cartItem);
    }
  },

  removeFromCart(product) {
    let item = state.cartItems.find(c => c.product.productId === product.productId);
    if (item) {
      state.cartItems.splice(state.cartItems.indexOf(item), 1);
    }
  },

  list() {
    return state.cartItems;
  }
};
