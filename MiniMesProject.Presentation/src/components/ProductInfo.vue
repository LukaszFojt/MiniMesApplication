<template>
  <div class="main-container">
    <h2>Produkty</h2>
    <div class="container">
      <h2>Dodaj nowy produkt</h2>
      <form @submit.prevent="addProduct">
        <input v-model="newProductName" placeholder="Nazwa produktu" @input="validateInput" />
        <input v-model="newProductDesc" placeholder="Opis produktu" @input="validateInput" />
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
        <input v-model="product.name" :placeholder="product.name" @input="validateInput" />
        <label for="description">Opis:</label>
        <input v-model="product.description" :placeholder="product.description" @input="validateInput" />
        <button @click="confirmUpdateProduct(product.id)">Aktualizuj</button>
        <button @click="confirmDeleteProduct(product.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import { products, newProductName, newProductDesc, sortByField, searchTerm, deletingProductId, updatingProductId, fetchData, addProduct, updateProduct, confirmUpdateProduct, deleteProduct, confirmDeleteProduct, sortedProducts, sortBy, searchProduct, validateInput } from '../scripts/products';

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>
.error {
  color: red;
}
</style>
