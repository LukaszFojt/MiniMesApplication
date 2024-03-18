import axios from 'axios';
import { ref, computed } from 'vue';

const products = ref([]);
const newProductName = ref('');
const newProductDesc = ref('');
const searchTerm = ref('');
const sortByField = ref('id');
const deletingProductId = ref(null);
const updatingProductId = ref(null);

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/products');
    products.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania produktów:', error);
  }
};

const addProduct = async () => {
  try {
    const product = { 
      Name: newProductName.value, 
      Description: newProductDesc.value,
    };
    await axios.post('http://localhost:5146/api/products/Add', product);
    console.log('Produkt dodany!');
    alert('Produkt ' + newProductName.value + ' został dodany');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania produktu:', error);
  }
};

const updateProduct = async (productId) => {
  try {
    const product = products.value.find(m => m.id === productId);
    const updatedProductData = { 
      Name: product.name, 
      Description: product.description, 
    };
    if (confirm('Czy chcesz zaktualizować ten produkt?')) {
        await axios.put(`http://localhost:5146/api/products/update/${productId}`, updatedProductData);
        console.log('Produkt zaktualizowany!');
        alert('Produkt został zaktualizowany!');
      }
    } catch (error) {
      console.error('Błąd podczas aktualizowania produktu:', error);
    }
};

const confirmUpdateProduct = (productId) => {
    updatingProductId.value = productId;
    updateProduct(productId);
    updatingProductId.value = null;
  };

const deleteProduct = async (productId) => {
  try {
    await axios.delete(`http://localhost:5146/api/products/delete/${productId}`);
    products.value = products.value.filter(product => product.id !== productId);
    console.log('Produkt usunięty!');
  } catch (error) {
    console.error('Błąd podczas usuwania produktu:', error);
  }
};

const confirmDeleteProduct = (productId) => {
    deletingProductId.value = productId;
    if (confirm('Czy na pewno chcesz usunąć ten produkt?')) {
      deleteProduct(productId);
      deletingProductId.value = null;
    }
  };

const sortedProducts = computed(() => {
  const sorted = products.value
    .filter(product => product.name.toLowerCase().includes(searchTerm.value.toLowerCase()) || product.id.toString().includes(searchTerm.value))
    .sort((a, b) => {
      if (sortByField.value === 'name') {
        return a.name.localeCompare(b.name);
      } else {
        return a.id - b.id;
      }
    });
  return sorted;
});

const sortBy = (field) => {
  sortByField.value = field;
};

const searchProduct = async () => {
  await fetchData();
};

const validateInput = (event) => { 
    event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
  };

export { products, newProductName, newProductDesc, sortByField, searchTerm, deletingProductId, updatingProductId, fetchData, addProduct, updateProduct, confirmUpdateProduct, deleteProduct, confirmDeleteProduct, sortedProducts, sortBy, searchProduct, validateInput };