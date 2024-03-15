<template>
  <div class="main-container">
    <h2>Procesy</h2>
    <div class="container">
      <h2>Dodaj Proces</h2>
      <form @submit.prevent="addProcess">
        <input v-model="newProcessSerialNumber" placeholder="Numer seryjny"/>
        <input v-model="newProcessStatus" placeholder="Status procesu" />
        <input v-model="newProcessOrderId" placeholder="ID zlecenia" type="number"/>
        <input v-model="newProcessDateTime" type="datetime-local"/>       
        <button type="submit">Dodaj</button>
      </form>
    </div>

    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('serialNumber')">Sortuj po numerze seryjnym</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według numeru seryjnego lub ID" />
      <button @click="searchProcess">Szukaj</button>
    </div>

    <ul class="container">
      <li v-for="process in sortedProcesses" :key="process.id">
        <div>ID procesu: {{ process.id }}</div>
        <label for="serialNumber">Numer seryjny:</label>
        <input v-model="process.serialNumber" placeholder="Numer seryjny"/>
        <label for="status">Status:</label>
        <input v-model="process.status" placeholder="Status procesu" />
        <label for="dateTime">Data:</label>
        <input v-model="process.dateTime" type="datetime-local" />
        <label for="orderId">ID zlecenia:</label>
        <input v-model="process.orderId" placeholder="ID zlecenia" type="number" />
        <button @click="updateProcess(process.id)">Aktualizuj</button>
        <button @click="confirmDeleteProcess(process.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { 
  processes, 
  newProcessSerialNumber, 
  newProcessOrderId, 
  newProcessStatus, 
  newProcessDateTime, 

  deletingProcessId, 
  updatingProcessId, 

  fetchData, 
  addProcess, 
  updateProcess, 
  confirmUpdateProcess, 
  deleteProcess, 
  confirmDeleteProcess,

  sortByField, 
  searchTerm, 
  sortedProcesses, 
  sortBy, 
  searchProcess, 

  validateInput 
} from '../scripts/processes';

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>

</style>
