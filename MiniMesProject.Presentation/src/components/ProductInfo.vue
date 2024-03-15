<template>
  <div class="main-container">
    <h2>Produkty</h2>
    <div class="container">
      <h2>Dodaj nowy produkt</h2>
      <form @submit.prevent="addProduct">
        <input v-model="newProductName" placeholder="Nazwa produktu" @input="validateInput({ name: newProductName })" />
        <input v-model="newProductDesc" placeholder="Opis produktu" @input="validateInput({ desc: newProductDesc })" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('name')">Sortuj po nazwie</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według nazwy lub ID" />
      <button @click="searchProduct">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="product in sortedProducts" :key="product.id">
        <div>ID produktu: {{ product.id }}</div>
        <label for="name">Nazwa:</label>
        <input v-model="product.name" :placeholder="product.name" @input="validateInput(product)" />
        <label for="description">Opis:</label>
        <input v-model="product.description" :placeholder="product.description" @input="validateInput(product)" />
        <button @click="updateProduct(product)">Aktualizuj</button>
        <button @click="confirmDeleteProduct(product.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';

const products = ref([]);
const newProductName = ref('');
const newProductDesc = ref('');
const sortByField = ref('id');
const searchTerm = ref('');

onMounted(async () => {
  await fetchData();
});

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
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania produktu:', error);
  }
};

const updateProduct = async (product) => {
  try {
    const updatedProductData = { 
      Name: product.name, 
      Description: product.description, 
    };
    await axios.put(`http://localhost:5146/api/products/update/${product.id}`, updatedProductData);
    console.log('Produkt zaktualizowany!');
  } catch (error) {
    console.error('Błąd podczas aktualizowania produktu:', error);
  }
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

const validateInput = (product) => { 
  const target = product.name ? product.name : product.description;
  target.value = target.value.replace(/[^\w\s]/gi, ''); // Usuwa znaki specjalne
};
</script>

<style scoped>
.error {
  color: red;
}
</style>
