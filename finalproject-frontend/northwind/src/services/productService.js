import axios from 'axios';
import Product from '../models/Product.js';

const API_URL = 'https://localhost:44313/api/';

function getApiUrl(path) {
  return `${API_URL}${path}`;
}

export async function fetchProducts(page, pageSize) {
  try {
    const response = await axios.get(getApiUrl('products/getpaged'), {
      params: { page, pageSize }
    });

    
    const { products, totalCount } = response.data.data;

    const productObjects = products.map(item => new Product(
      item.productId,
      item.categoryId,
      item.productName,
      item.unitsInStock,
      item.unitPrice
    ));

    return {
      data: {
        products: productObjects,
        totalCount
      },
      success: true,
      message: ''
    };
  } catch (error) {
    console.error('Ürünler getirilirken hata oluştu:', error);
    return {
      data: {
        products: [],
        totalCount: 0
      },
      success: false,
      message: 'Ürünler getirilirken hata oluştu'
    };
  }
}



export async function fetchProductsByCategory(categoryId, page, pageSize) {
  try {
    const response = await axios.get(getApiUrl('products/getbycategoryid'), {
      params: { categoryId, page, pageSize }
    });

    const { data } = response.data;

    const productObjects = data.products.map(item => new Product(
      item.productId,
      item.categoryId,
      item.productName,
      item.unitsInStock,
      item.unitPrice
    ));

    return {
      data: {
        products: productObjects,
        totalCount: data.totalCount
      },
      success: true,
      message: ''
    };
  } catch (error) {
    console.error('Kategoriye göre ürünler getirilirken hata oluştu:', error);
    return {
      data: {
        products: [],
        totalCount: 0
      },
      success: false,
      message: 'Kategoriye göre ürünler getirilirken hata oluştu'
    };
  }
}


export async function add(product) {
  try {
    const response = await axios.post(getApiUrl('products/add'), product);
    return {
      data: response.data,
      success: response.data.success,
      message: response.data.message
    };
  } catch (error) {
    console.error('Ürün eklenirken hata oluştu:', error);

    let errorMessage = 'Bir hata oluştu';
    if (error.response && error.response.data) {
      errorMessage = error.response.data.message || 'Bir hata oluştu';
    }

    return {
      data: null,
      success: false,
      message: errorMessage
    };
  }
}

