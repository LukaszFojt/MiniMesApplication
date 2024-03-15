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
        <button @click="updateParameter(parameter)">Aktualizuj</button>
        <button @click="deleteParameter(parameter.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';

const parameters = ref([]);
const newParameterName = ref('');
const newParameterUnit = ref('');
const ParametersId = ref('');
const searchTerm = ref('');
const sortByField = ref('id');

onMounted(async () => {
  await fetchData();
});

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/parameters');
    parameters.value = response.data;
    console.log(parameters.value);
  } catch (error) {
    console.error('Error fetching parameters:', error);
  }
};

const addParameter = async () => {
  try {
    const parameter = { 
      Name: newParameterName.value, 
      Unit: newParameterUnit.value,
    };
    await axios.post('http://localhost:5146/api/parameters/Add', parameter);
    console.log('Parametr dodany!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania parametru:', error);
  }
};

const updateParameter = async (parameter) => {
  try {
    const updatedParameterData = { 
      Name: parameter.name, 
      Unit: parameter.unit,
    };
    await axios.put(`http://localhost:5146/api/parameters/update/${parameter.id}`, updatedParameterData);
    console.log('Parameter updated successfully!');
  } catch (error) {
    console.error('Error updating parameter:', error);
  }
};

const deleteParameter = async (parameterId) => {
  try {
    await axios.delete(`http://localhost:5146/api/parameters/delete/${parameterId}`);
    parameters.value = parameters.value.filter(parameter => parameter.id !== parameterId);
    console.log('Parameter deleted successfully!');
  } catch (error) {
    console.error('Error deleting parameter:', error);
  }
};

const sortedParameters = computed(() => {
  const sorted = parameters.value
    .filter(parameter => parameter.name.toLowerCase().includes(searchTerm.value.toLowerCase()) || parameter.id.toString().includes(searchTerm.value))
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

const searchParameter = async () => {
  await fetchData();
};

const validateInput = (event) => { 
  event.target.value = event.target.value.replace(/[^\w\s]/gi, ''); // Usuwa znaki specjalne
};
</script>
