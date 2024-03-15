<template>
  <div class="main-container">
    <h2>Procesy</h2>
    <div class="container">
      <h2>Dodaj Proces</h2>
      <form @submit.prevent="addProcesses">
        <input v-model="newProcessesSerial" placeholder="Numer seryjny" type="number" @input="validateInput({ serial: newProcessesSerial })" />
        <input v-model="newProcessesStatus" placeholder="Status zlecenia" @input="validateInput({ status: newProcessesStatus })" />
        <input v-model="newProcessesDate" type="datetime-local" @input="validateInput({ date: newProcessesDate })" />
        <input v-model="newProcessesOrderId" placeholder="Numer zlecenia" type="number" @input="validateInput({ orderId: newProcessesOrderId })" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('serial')">Sortuj po numerze seryjnym</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według numeru seryjnego lub ID" />
      <button @click="searchProcesses">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="process in sortedProcesses" :key="process.id">
        <div>ID procesu: {{ process.id }}</div>
        <label for="serial">Numer seryjny:</label>
        <input v-model="process.serial" :placeholder="process.serial" type="number" @input="validateInput(process, 'serial')" />
        <label for="status">Status:</label>
        <input v-model="process.status" :placeholder="process.status" @input="validateInput(process, 'status')" />
        <label for="date">Data:</label>
        <input v-model="process.date" :placeholder="process.date" type="datetime-local" @input="validateInput(process, 'date')" />
        <label for="orderId">ID zlecenia:</label>
        <input v-model="process.orderId" :placeholder="process.orderId" type="number" @input="validateInput(process, 'orderId')" />
        <button @click="updateProcesses(process)">Aktualizuj</button>
        <button @click="confirmDeleteProcesses(process.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';

const processes = ref([]);
const newProcessesSerial = ref('');
const newProcessesStatus = ref('');
const newProcessesDate = ref('');
const newProcessesOrderId = ref('');
const sortByField = ref('id');
const searchTerm = ref('');
const deletingProcessesId = ref(null);
const updatingProcessesId = ref(null);

onMounted(async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/processess');
    processes.value = response.data;
  } catch (error) {
    console.error('Error fetching processes:', error);
  }
});

const addProcesses = async () => {
  try {
    const process = { 
      SerialNumber: newProcessesSerial.value, 
      Status: newProcessesStatus.value,
      DateTime: newProcessesDate.value,
      OrderId: newProcessesOrderId.value,
    };
    await axios.post('http://localhost:5146/api/processess/Add', process);
    console.log('Proces dodany!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania procesu:', error);
  }
};

const updateProcesses = async (process) => {
  try {
    const updatedProcessData = { 
      SerialNumber: process.serial, 
      Status: process.status,
      DateTime: process.date,
      OrderId: process.orderId,
    };
    await axios.put(`http://localhost:5146/api/processess/update/${process.id}`, updatedProcessData);
    console.log('Process updated successfully!');
  } catch (error) {
    console.error('Error updating process:', error);
  }
};

const deleteProcesses = async (processId) => {
  try {
    await axios.delete(`http://localhost:5146/api/processess/delete/${processId}`);
    processes.value = processes.value.filter(process => process.id !== processId);
    console.log('process deleted successfully!');
  } catch (error) {
    console.error('Error deleting process:', error);
  }
};

const sortedProcesses = computed(() => {
  const sorted = processes.value
    .filter(process => {
      return (process.serial && process.serial.toLowerCase().includes(searchTerm.value.toLowerCase())) ||
             process.id.toString().includes(searchTerm.value);
    })
    .sort((a, b) => {
      if (sortByField.value === 'serial') {
        return a.serial.localeCompare(b.serial);
      } else {
        return a.id - b.id;
      }
    });
  return sorted;
});


const sortBy = (field) => {
  sortByField.value = field;
};

const searchProcesses = async () => {
  await fetchData();
};

const validateInput = (process, field) => { 
  const target = process[field];
  if (field === 'date') {
    // Upewnij się, że data jest w odpowiednim formacie
    const dateTimeRegex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/;
    if (!dateTimeRegex.test(target.value)) {
      // Jeśli format jest niepoprawny, wyzeruj wartość
      target.value = '';
    }
  } else {
    // Usuń znaki specjalne
    target.value = target.value.replace(/[^\w\s]/gi, '');
  }
};

const confirmDeleteProcesses = (processId) => {
  deletingProcessesId.value = processId;
  if (confirm('Czy na pewno chcesz usunąć ten proces?')) {
    deleteProcesses(processId);
    deletingProcessesId.value = null;
  }
};
</script>
