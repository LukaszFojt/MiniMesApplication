import axios from 'axios';
import { ref, computed } from 'vue';

const machines = ref([]);
const newMachineName = ref('');
const newMachineDesc = ref('');
const sortByField = ref('id');
const searchTerm = ref('');
const deletingMachineId = ref(null);
const updatingMachineId = ref(null);

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
    alert("Maszyna " + newMachineName.value + " została dodana");
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
  event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
};

export { machines, newMachineName, newMachineDesc, sortByField, searchTerm, deletingMachineId, updatingMachineId, fetchData, addMachine, updateMachine, confirmUpdateMachine, deleteMachine, confirmDeleteMachine, sortedMachines, sortBy, searchMachine, validateInput };
