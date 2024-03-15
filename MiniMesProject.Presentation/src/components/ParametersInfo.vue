<template>
  <div class="main-container">
    <h2>Parametry</h2>
    <div class="container">
      <h2>Dodaj Parametr</h2>
      <form @submit.prevent="addParameter">
        <input v-model="newParameterName" placeholder="Nazwa parametru" />
        <input v-model="newParameterUnit" placeholder="Jednostka parametru" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('name')">Sortuj po nazwie</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według nazwy lub ID" />
      <button @click="searchParameter">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="parameter in sortedParameters" :key="parameter.id">
        <div>ID parametru: {{ parameter.id }}</div>
        <label for="name">Nazwa:</label>
        <input v-model="parameter.name" :placeholder="parameter.name" />
        <label for="unit">Jednostka:</label>
        <input v-model="parameter.unit" :placeholder="parameter.unit" />   
        <button @click="confirmUpdateParameter(parameter.id)">Aktualizuj</button>
        <button @click="confirmDeleteParameter(parameter.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import { parameters, newParameterName, newParameterUnit, sortByField, searchTerm, deletingParameterId, updatingParameterId, fetchData, addParameter, updateParameter, confirmUpdateParameter, deleteParameter, confirmDeleteParameter, sortedParameters, sortBy, searchParameter, validateInput } from '../scripts/parameters';

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>
.error {
  color: red;
}
</style>
