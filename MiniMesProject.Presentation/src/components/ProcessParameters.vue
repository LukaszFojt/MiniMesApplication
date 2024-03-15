<template>
  <div class="main-container">
    <h2>Parametry procesów</h2>
    <div class="container">
      <h2>Dodaj parametry procesów</h2>
      <form @submit.prevent="addProcessParameter">
        <input v-model="newProcessParameter_Value" placeholder="Wartość" />
        <input v-model="newProcessParameter_ProcessId" placeholder="ID procesu" />
        <input v-model="newProcessParameter_ParameterId" placeholder="ID parametru" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według ID" />
      <button @click="searchProcessParameters">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="processParameter in sortedProcessParameters" :key="processParameter.id">
        <div>ID parametrów procesu: {{ processParameter.id }}</div>
        <label for="value">Wartość:</label>
        <input v-model="processParameter.value" :placeholder="processParameter.value" />
        <label for="processId">ID procesu:</label>
        <input v-model="processParameter.processId" :placeholder="processParameter.processId" />
        <label for="parameterId">ID parametru:</label>
        <input v-model="processParameter.parameterId" :placeholder="processParameter.parameterId" />
        <button @click="confirmUpdateProcessParameter(processParameter.id)">Aktualizuj</button>
        <button @click="confirmDeleteProcessParameter(processParameter.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { processParameters, newProcessParameter_Value, newProcessParameter_ProcessId, newProcessParameter_ParameterId, sortByField, searchTerm, deletingProcessParameterId, updatingProcessParameterId, fetchData, addProcessParameter, updateProcessParameter, confirmUpdateProcessParameter, deleteProcessParameter, confirmDeleteProcessParameter, sortedProcessParameters, sortBy, searchProcessParameter, validateInput } from '../scripts/processParameters';

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>

</style>