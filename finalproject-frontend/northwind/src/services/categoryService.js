import axios from 'axios';
import Category from '../models/Category.js';

const API_URL = 'https://localhost:44313/api/categories/getall'; 

export async function fetchCategories() {
    try {
        const response = await axios.get(API_URL);
        const data = response.data;

        
        console.log('API Response:', data);

        
        const categories = data.data; 

        return {
            data: categories.map(item => new Category(
                item.categoryId,
                item.categoryName
            )),
            success: data.success,
            message: data.message
        };
    } catch (error) {
        console.error('Error fetching categories:', error);
        return {
            data: [],
            success: false,
            message: 'Error fetching categories'
        };
    }
}
