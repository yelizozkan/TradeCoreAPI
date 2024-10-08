// import axios from 'axios';

// /**
//  * @param {string} url 
//  * @param {Function} Model 
//  * @returns {Object}
//  */
// export async function fetchData(url, Model) {
//     try {
//       const response = await axios.get(url);
//       const data = response.data;
  
//       // Veriyi Model nesnelerine dönüştür
//       const items = data.data ? data.data.map(item => new Model(item)) : [];
  
//       return {
//         data: items,
//         success: data.success,
//         message: data.message 
//       };
//     } catch (error) {
//       console.error('Error fetching data:', error);
//       return {
//         data: [],
//         success: false,
//         message: 'Error fetching data'
//       };
//     }
//   }