import axios from 'axios';

const apiUrl = 'https://localhost:44313/api/auth/';

export const authService = {
  async login(loginModel) {
    try {
      const response = await axios.post(`${apiUrl}login`, loginModel);
      return {
        success: true,
        message: response.data.message,
        data: {
          token: response.data.data.token,
          expiration: response.data.data.expiration
        }
      };
    } catch (error) {
      console.error('Login error: ', error.response);  // Hata detaylarını logluyoruz.
      
      const message = error.response?.data?.message || 'Login failed';
      return {
        success: false,
        message,
        data: null
      };
    }
  }
  
};
