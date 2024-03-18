<template>
    <div class="main-container">
      <h2>Parametry maszyn</h2>
      <div class="container">
        <h2>Dodaj parametry maszyny</h2>
        <form @submit.prevent="addMachineParameter">
          <input v-model="newMachineParameter_DateTime" type="datetime-local"/>
          <input v-model="newMachineParameter_MachineId" placeholder="ID maszyny" />
          <input v-model="newMachineParameter_ParameterId" placeholder="ID parametru" />
          <button type="submit">Dodaj</button>
        </form>
      </div>
      <div>
        <p>Wybierz maszynę</p>
        <select class="select-button" v-model="machineId">
            <option disabled selected="selected">Wybierz maszynę</option>
            <option v-for="machine in sortedMachines" :key="machine.id" :value="machine.id">{{ machine.id }}</option>             
        </select>
      </div>
      <div class="sorting-menu">
        <button @click="sortBy('id')">Sortuj po ID</button>
        <input v-model="searchTerm" class="search" placeholder="Wyszukaj według ID" />
        <button @click="searchMachineParameters">Szukaj</button>
      </div>
      <ul class="container">
        <li v-for="machineParameter in sortedMachineParameters" :key="machineParameter.id">
          <div>ID parametru maszyny: {{ machineParameter.id }}</div>
          <label for="dateTime">Data dodania:</label>
          <input v-model="machineParameter.dateTime" type="datetime-local" />
          <label for="machineId">ID maszyny:</label>
          <input v-model="machineParameter.machineId" :placeholder="machineParameter.machineId" />
          <label for="parameterId">ID parametru:</label>
          <input v-model="machineParameter.parameterId" :placeholder="machineParameter.parameterId" />
          <button @click="confirmUpdateMachineParameter(machineParameter.id)">Aktualizuj</button>
          <button @click="confirmDeleteMachineParameter(machineParameter.id)">Usuń</button>
        </li>
      </ul>
    </div>
  </template>
  
  <script setup>
  import { onMounted } from 'vue';
  import { machineParameters, 
    newMachineParameter_DateTime, 
    newMachineParameter_MachineId, 
    newMachineParameter_ParameterId, 
    sortByField, 
    searchTerm, 
    deletingMachineParameterId, 
    updatingMachineParameterId, 
    fetchData, 
    addMachineParameter, 
    updateMachineParameter, 
    confirmUpdateMachineParameter, 
    deleteMachineParameter, 
    confirmDeleteMachineParameter, 
    sortedMachineParameters, 
    sortBy, 
    searchMachineParameter, 
    validateInput,

    fetchMachines,
    machineId,
    machines,
    sortedMachines,
    selectedMachine,
} from '../scripts/machineParameters';
  
  onMounted(async () => {
    await fetchData(machineId);
    await fetchMachines();
  });
  </script>
  
  <style scoped>
  
  </style>