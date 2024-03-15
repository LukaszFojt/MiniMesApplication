import axios from 'axios';
import { ref, computed } from 'vue';

const processParameters = ref([]);
const newProcessParameter_Value = ref();
const newProcessParameter_ProcessId = ref();
const newProcessParameter_ParameterId = ref();
const searchTerm = ref('');
const sortByField = ref('id');
const deletingProcessParameterId = ref(null);
const updatingProcessParameterId = ref(null);

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/processParameters');
    processParameters.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania produktów:', error);
  }
};

const addProcessParameter = async () => {
  try {
    if (!newProcessParameter_Value.value || newProcessParameter_Value.value.trim() === '') {
      console.error('Value is empty. Please provide a valid number.');
      return;
    }
    const value = parseFloat(newProcessParameter_Value.value);
    if (isNaN(value)) {
      console.error('Invalid value. Please provide a valid number.');
      return;
    }
    const processParameter = { 
      Value: value,
      ProcessId: newProcessParameter_ProcessId.value,
      ParameterId: newProcessParameter_ParameterId.value,
      processProcessParameters: [],
    };
    await axios.post('http://localhost:5146/api/processParameters/Add', processParameter);
    console.log('Parametr Procesu dodany!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania produktu:', error);
  }
};



const updateProcessParameter = async (processParameterId) => {
  try {
    const processParameter = processParameters.value.find(m => m.id === processParameterId);
    const updatedProcessParameterData = { 
      Name: processParameter.name, 
      Description: processParameter.description, 
    };
    if (confirm('Czy chcesz zaktualizować ten produkt?')) {
        await axios.put(`http://localhost:5146/api/processParameters/update/${processParameterId}`, updatedProcessParameterData);
        console.log('Parametr Procesu zaktualizowany!');
        alert('Parametr Procesu został zaktualizowany!');
      }
    } catch (error) {
      console.error('Błąd podczas aktualizowania produktu:', error);
    }
};

const confirmUpdateProcessParameter = (processParameterId) => {
    updatingProcessParameterId.value = processParameterId;
    updateProcessParameter(processParameterId);
    updatingProcessParameterId.value = null;
  };

const deleteProcessParameter = async (processParameterId) => {
  try {
    await axios.delete(`http://localhost:5146/api/processParameters/delete/${processParameterId}`);
    processParameters.value = processParameters.value.filter(processParameter => processParameter.id !== processParameterId);
    console.log('Parametr Procesu usunięty!');
  } catch (error) {
    console.error('Błąd podczas usuwania produktu:', error);
  }
};

const confirmDeleteProcessParameter = (processParameterId) => {
    deletingProcessParameterId.value = processParameterId;
    if (confirm('Czy na pewno chcesz usunąć ten parametr procesu?')) {
      deleteProcessParameter(processParameterId);
      deletingProcessParameterId.value = null;
    }
  };

const sortedProcessParameters = computed(() => {
  const sorted = processParameters.value
    .filter(processParameter => processParameter.id.toString().includes(searchTerm.value))
    .sort((a, b) => {
        return a.id - b.id;
    });
  return sorted;
});

const sortBy = (field) => {
  sortByField.value = field;
};

const searchProcessParameter = async () => {
  await fetchData();
};

const validateInput = (event) => { 
    event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
  };

export { processParameters, newProcessParameter_Value, newProcessParameter_ProcessId,newProcessParameter_ParameterId, sortByField, searchTerm, deletingProcessParameterId, updatingProcessParameterId, fetchData, addProcessParameter, updateProcessParameter, confirmUpdateProcessParameter, deleteProcessParameter, confirmDeleteProcessParameter, sortedProcessParameters, sortBy, searchProcessParameter, validateInput };