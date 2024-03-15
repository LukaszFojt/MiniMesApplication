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
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';

const machines = ref([]);
const newMachineName = ref('');
const newMachineDesc = ref('');
const sortByField = ref('id'); // Pole, według którego sortujemy
const searchTerm = ref('');
const deletingMachineId = ref(null);
const updatingMachineId = ref(null);

onMounted(async () => {
  await fetchData();
});

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/machines');
    machines.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania maszyn:', error);
  }
};

const addMachine = async () => {
  try {
    const machine = { 
      Name: newMachineName.value, 
      Description: newMachineDesc.value,
    };
    await axios.post('http://localhost:5146/api/machines/Add', machine);
    console.log('Maszyna dodana!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania maszyny:', error);
  }
};

const updateMachine = async (machineId) => {
  try {
    const machine = machines.value.find(m => m.id === machineId);
    const updatedMachineData = { 
      Name: machine.name, 
      Description: machine.description 
    };
    if (confirm('Czy chcesz zaktualizować tę maszynę?')) {
      await axios.put(`http://localhost:5146/api/machines/update/${machineId}`, updatedMachineData);
      console.log('Maszyna zaktualizowana!');
      alert('Maszyna została zaktualizowana!');
    }
  } catch (error) {
    console.error('Błąd podczas aktualizowania maszyny:', error);
  }
};

const confirmUpdateMachine = (machineId) => {
  updatingMachineId.value = machineId;
  updateMachine(machineId);
  updatingMachineId.value = null;
};

const deleteMachine = async (machineId) => {
  try {
    await axios.delete(`http://localhost:5146/api/machines/delete/${machineId}`);
    machines.value = machines.value.filter(machine => machine.id !== machineId);
    console.log('Maszyna usunięta!');
    alert('Maszyna została usunięta!');
  } catch (error) {
    console.error('Błąd podczas usuwania maszyny:', error);
  }
};

const confirmDeleteMachine = (machineId) => {
  deletingMachineId.value = machineId;
  if (confirm('Czy na pewno chcesz usunąć tę maszynę?')) {
    deleteMachine(machineId);
    deletingMachineId.value = null;
  }
};

const sortedMachines = computed(() => {
  const sorted = machines.value
    .filter(machine => machine.name.toLowerCase().includes(searchTerm.value.toLowerCase()) || machine.id.toString().includes(searchTerm.value))
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

const searchMachine = async () => {
  await fetchData();
};

const validateInput = (event) => { 
  event.target.value = event.target.value.replace(/[^\w\s]/gi, ''); // Usuwa znaki specjalne
};

</script>

<style scoped>
.error {
  color: red;
}
</style>
