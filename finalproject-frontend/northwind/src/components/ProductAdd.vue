<template>
    <div class="content">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header">
                    <h5 class="title">Add product</h5>
                </div>
                <div class="card-body">
                    <form @submit.prevent="submitForm">
                        <div class="mb-3">
                            <label for="productName">Product Name</label>
                            <div class="form-group">
                                <input type="text" id="productName" v-model="productAddForm.productName"
                                    class="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="categoryId">CategoryId</label>
                            <div class="form-group">
                                <input type="number" id="categoryId" v-model="productAddForm.categoryId"
                                    class="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="unitsInStock">Units In Stock</label>
                            <div class="form-group">
                                <input type="number" id="unitsInStock" v-model="productAddForm.unitsInStock"
                                    class="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="unitPrice">Unit Price</label>
                            <div class="form-group">
                                <input type="number" id="unitPrice" v-model="productAddForm.unitPrice"
                                    class="form-control" />
                            </div>
                        </div>

                        <div class="card-footer">
                            <button type="submit" class="btn btn-fill btn-primary">Add</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { ref } from 'vue';
import { useToast } from 'vue-toastification';
import { add } from '../services/productService.js';

export default {
    name: 'ProductAdd',
    setup() {
        const productAddForm = ref({
            productName: '',
            categoryId: '',
            unitsInStock: '',
            unitPrice: ''
        });

        const toast = useToast();

        const submitForm = async () => {
            if (isValidForm()) {
                try {
                    const response = await add(productAddForm.value);
                    if (response.success) {
                        toast.success(response.message, 'Başarılı');
                    } else {
                        toast.error(response.message, 'Hata');
                    }
                } catch (error) {
                    
                    if (error.response && error.response.data && error.response.data.Errors) {
                        error.response.data.Errors.forEach(err => {
                            toast.error(err.ErrorMessage || 'Doğrulama Hatası', 'Doğrulama Hatası');
                        });
                    } else {
                        toast.error('Beklenmeyen bir hata oluştu', 'Hata');
                    }
                }
            } else {
                toast.error('Formunuz eksik', 'Uyarı');
            }
        };



        const isValidForm = () => {
            return productAddForm.value.productName && productAddForm.value.categoryId
                && productAddForm.value.unitsInStock && productAddForm.value.unitPrice;
        };

        return {
            productAddForm,
            submitForm
        };
    }
};
</script>

<style scoped></style>
