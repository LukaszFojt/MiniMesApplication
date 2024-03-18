import axios from 'axios';
import { ref, computed, watchEffect } from 'vue';

const machineParameters = ref([]);
const newMachineParameter_DateTime = ref();
const newMachineParameter_MachineId = ref();
const newMachineParameter_ParameterId = ref();

const deletingMachineParameterId = ref(null);
const updatingMachineParameterId = ref(null);

const searchTerm = ref('');
const sortByField = ref('id');

const machines = ref([]);
const machineId = ref(1);
const selectedMachine = ref(null);

const fetchData = async () => {
    try {
      const response = await axios.get(`http://localhost:5146/api/machineParameter?machineId=${machineId.value}`);
      machineParameters.value = response.data;
    } catch (error) {
      console.error('Błąd podczas pobierania parametrów maszyn:', error);
      alert("Ta maszyna nie posiada parametrów maszyn, wybierz inną.");
    }
  };
  
  const fetchMachines = async () => {
    try {
      const response = await axios.get('http://localhost:5146/api/machines');
      machines.value = response.data;
    } catch (error) {
      console.error('Błąd podczas pobierania maszyn:', error);
    }
  };

  const sortedMachines = computed(() => {
    const sorted = machines.value
      .sort((a, b) => {
        return a.id - b.id;     
      });
    return sorted;
  });

const addMachineParameter = async () => {
  try {
    const machineParameter = { 
      DateTime: newMachineParameter_DateTime.value,
      MachineId: newMachineParameter_MachineId.value,
      ParameterId: newMachineParameter_ParameterId.value,
      machineMachineParameters: [],
    };
    await axios.post('http://localhost:5146/api/MachineParameter/Add', machineParameter);
    console.log('Parametr maszyny dodany!');
    alert('Parametr maszyny został dodany!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania parametru maszyny:', error);
  }
};

const updateMachineParameter = async (machineParameterId) => {
  try {
    const machineParameter = machineParameters.value.find(m => m.id === machineParameterId);
    const updatedMachineParameterData = { 
      DateTime: machineParameter.dateTime,
      MachineId: machineParameter.machineId, 
      ParameterId: machineParameter.parameterId, 
    };
    if (confirm('Czy chcesz zaktualizować ten parametr maszyny?')) {
        await axios.put(`http://localhost:5146/api/MachineParameter/update/${machineParameterId}`, updatedMachineParameterData);
        console.log('Parametr maszyny zaktualizowany!');
        alert('Parametr maszyny został zaktualizowany!');
      }
    } catch (error) {
      console.error('Błąd podczas aktualizowania produktu:', error);
    }
};

const confirmUpdateMachineParameter = (machineParameterId) => {
    updatingMachineParameterId.value = machineParameterId;
    updateMachineParameter(machineParameterId);
    updatingMachineParameterId.value = null;
  };

const deleteMachineParameter = async (machineParameterId) => {
  try {
    await axios.delete(`http://localhost:5146/api/MachineParameter/delete/${machineParameterId}`);
    machineParameters.value = machineParameters.value.filter(machineParameter => machineParameter.id !== machineParameterId);
    console.log('Parametr Procesu usunięty!');
  } catch (error) {
    console.error('Błąd podczas usuwania produktu:', error);
  }
};

const confirmDeleteMachineParameter = (machineParameterId) => {
    deletingMachineParameterId.value = machineParameterId;
    if (confirm('Czy na pewno chcesz usunąć ten parametr procesu?')) {
      deleteMachineParameter(machineParameterId);
      deletingMachineParameterId.value = null;
    }
  };

const sortedMachineParameters = computed(() => {
  const sorted = machineParameters.value
    .filter(machineParameter => machineParameter.id.toString().includes(searchTerm.value))
    .sort((a, b) => {
        return a.id - b.id;
    });
  return sorted;
});

const sortBy = (field) => {
  sortByField.value = field;
};

const searchMachineParameter = async () => {
  await fetchData();
};

const validateInput = (event) => { 
    event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
  };

  watchEffect(() => {
    if (machineId.value) {
      selectedMachine.value = sortedMachines.value.find(machine => machine.id === machineId.value);  
      fetchData();   
    } else {
      selectedMachine.value = null;
    }
  });

export { 
    machineParameters, 
    newMachineParameter_DateTime, 
    newMachineParameter_MachineId,
    newMachineParameter_ParameterId, 
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
    sortByField,
    searchTerm,  
    searchMachineParameter, 
    validateInput,

    machines,
    machineId, 
    sortedMachines,
    fetchMachines,
    selectedMachine,
};