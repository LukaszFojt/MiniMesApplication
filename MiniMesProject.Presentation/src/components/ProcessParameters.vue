<template>
  <div class="main-container">
    <h2>Parametry procesów</h2>
    <div class="container">
      <h2>Dodaj parametry procesów</h2>
      <form @submit.prevent="addProcessParameters">
        <input v-model="newProcessParametersValue" placeholder="Wartość" />
        <input v-model="newProcessParametersProcessId" placeholder="ID procesu" />
        <input v-model="newProcessParametersParameterId" placeholder="ID parametru" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('value')">Sortuj po wartości</button>
      <button @click="sortBy('processId')">Sortuj po ID procesu</button>
      <button @click="sortBy('parameterId')">Sortuj po ID parametru</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według wartości lub ID" />
      <button @click="searchProcessParameters">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="processParameter in sortedProcesses" :key="processParameter.id">
        <div>ID parametrów procesu: {{ processParameter.id }}</div>
        <label for="value">Wartość:</label>
        <input v-model="processParameter.value" :placeholder="processParameter.value" />
        <label for="processId">ID procesu:</label>
        <input v-model="processParameter.processId" :placeholder="processParameter.processId" />
        <label for="parameterId">ID parametru:</label>
        <input v-model="processParameter.parameterId" :placeholder="processParameter.parameterId" />
        <button @click="updateProcessParameters(processParameter)">Aktualizuj</button>
        <button @click="deleteProcessParameters(processParameter.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';

const processParameters = ref([]);
const newProcessParametersValue = ref();
const newProcessParametersProcessId = ref();
const newProcessParametersParameterId = ref();
const ProcessParametersId = ref();
const sortByField = ref('id');
const searchTerm = ref('');

onMounted(async () => {
  await fetchData();
});

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/processParameters');
    processParameters.value = response.data;
  } catch (error) {
    console.error('Error fetching processParameters:', error);
  }
};

const addProcessParameters = async () => {
  try {
    const processParameter = { 
      Value: newProcessParametersValue.value, 
      ProcessId: newProcessParametersProcessId.value,
      ParameterId: newProcessParametersParameterId.value,
    };
    await axios.post('http://localhost:5146/api/processParameters/Add', processParameter);
    console.log('Proces dodany!');
    await fetchData(); // Refresh data after adding
  } catch (error) {
    console.error('Błąd podczas dodawania zlecenia:', error);
  }
};

const updateProcessParameters = async (processParameter) => {
  try {
    const updatedProcessParametersData = { 
      Value: processParameter.value, 
      ProcessId: processParameter.processId,
      ParameterId: processParameter.parameterId,
    };
    await axios.put(`http://localhost:5146/api/processParameters/update/${processParameter.id}`, updatedProcessParametersData);
    console.log('ProcessParameters updated successfully!');
  } catch (error) {
    console.error('Error updating processParameters:', error);
  }
};

const deleteProcessParameters = async (processParametersId) => {
  try {
    await axios.delete(`http://localhost:5146/api/processParameters/delete/${processParametersId}`);
    processParameters.value = processParameters.value.filter(processParameters => processParameters.id !== processParametersId);
    console.log('processParameters deleted successfully!');
  } catch (error) {
    console.error('Error deleting processParameters:', error);
  }
};

const sortedProcesses = computed(() => {
  const sorted = processParameters.value
    .filter(processParameter => (typeof processParameter.value === 'string' && processParameter.value.toLowerCase().includes(searchTerm.value.toLowerCase())) || processParameter.id.toString().includes(searchTerm.value))
    .sort((a, b) => {
      if (sortByField.value === 'value') {
        return a.value.localeCompare(b.value);
      } else if (sortByField.value === 'processId') {
        return a.processId - b.processId;
      } else if (sortByField.value === 'parameterId') {
        return a.parameterId - b.parameterId;
      } else {
        // Default sorting by id
        return a.id - b.id;
      }
    });
  return sorted;
});

const sortBy = (field) => {
  sortByField.value = field;
};

const searchProcessParameters = async () => {
  await fetchData(); // Refresh data after searching
};

</script>
