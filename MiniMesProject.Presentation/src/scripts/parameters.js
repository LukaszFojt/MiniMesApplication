import axios from 'axios';
import { ref, computed } from 'vue';

const parameters = ref([]);
const newParameterName = ref('');
const newParameterUnit = ref('');
const searchTerm = ref('');
const sortByField = ref('id');
const deletingParameterId = ref(null);
const updatingParameterId = ref(null);

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/parameters');
    parameters.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania parametrów:', error);
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
    alert('Parametr został dodany!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania parametru:', error);
  }
};

const updateParameter = async (parameterId) => {
  try {
    const parameter = parameters.value.find(m => m.id === parameterId);
    const updatedParameterData = { 
      Name: parameter.name, 
      Unit: parameter.unit,
    };
    if (confirm('Czy chcesz zaktualizować ten parametr?')) {
        await axios.put(`http://localhost:5146/api/parameters/update/${parameterId}`, updatedParameterData);
        console.log('Parametr zaktualizowany!');
        alert('Parametr został zaktualizowany!');
      }
    } catch (error) {
      console.error('Błąd podczas aktualizowania parametru:', error);
    }
};

const confirmUpdateParameter = (parameterId) => {
    updatingParameterId.value = parameterId;
    updateParameter(parameterId);
    updatingParameterId.value = null;
  };


const deleteParameter = async (parameterId) => {
  try {
    await axios.delete(`http://localhost:5146/api/parameters/delete/${parameterId}`);
    parameters.value = parameters.value.filter(parameter => parameter.id !== parameterId);
    console.log('Parametr usunięty!');
  } catch (error) {
    console.error('Błąd podczas usuwania parametru:', error);
  }
};

const confirmDeleteParameter = (parameterId) => {
    deletingParameterId.value = parameterId;
    if (confirm('Czy na pewno chcesz usunąć ten parametr?')) {
      deleteParameter(parameterId);
      deletingParameterId.value = null;
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
  event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
};

export { parameters, newParameterName, newParameterUnit, sortByField, searchTerm, deletingParameterId, updatingParameterId, fetchData, addParameter, updateParameter, confirmUpdateParameter, deleteParameter, confirmDeleteParameter, sortedParameters, sortBy, searchParameter, validateInput };