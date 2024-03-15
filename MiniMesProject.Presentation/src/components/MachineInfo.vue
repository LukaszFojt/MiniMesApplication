<template>
  <div class="main-container">
    <h2>Maszyny</h2>
    <div class="container">
      <h2>Dodaj nową maszynę</h2>
      <form @submit.prevent="addMachine">
        <input v-model="newMachineName" placeholder="Nazwa maszyny" @input="validateInput" />
        <input v-model="newMachineDesc" placeholder="Opis maszyny" @input="validateInput" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('name')">Sortuj po nazwie</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według nazwy lub ID" />
      <button @click="searchMachine">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="machine in sortedMachines" :key="machine.id">
        <div>ID maszyny: {{ machine.id }}</div>
        <label for="name">Nazwa:</label>
        <input v-model="machine.name" :placeholder="machine.name" @input="validateInput"/>
        <label for="description">Opis:</label>
        <input v-model="machine.description" :placeholder="machine.description" @input="validateInput"/>
        <button @click="confirmUpdateMachine(machine.id)">Aktualizuj</button>
        <button @click="confirmDeleteMachine(machine.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { machines, newMachineName, newMachineDesc, sortByField, searchTerm, deletingMachineId, updatingMachineId, fetchData, addMachine, updateMachine, confirmUpdateMachine, deleteMachine, confirmDeleteMachine, sortedMachines, sortBy, searchMachine, validateInput } from '../scripts/machines';

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>

</style>
