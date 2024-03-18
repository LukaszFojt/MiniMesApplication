import axios from 'axios';
import { ref, computed } from 'vue';

const newProcessSerialNumber = ref('');
const newProcessOrderId = ref();
const newProcessStatus = ref('');
const newProcessDateTime = ref('');

const processes = ref([]);
const deletingProcessId = ref(null);
const updatingProcessId = ref(null);

const sortByField = ref('id');
const searchTerm = ref('');

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/processess');
    processes.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania maszyn:', error);
  }
};

const addProcess = async () => {
  try {
    const process = { 
      SerialNumber: newProcessSerialNumber.value, 
      OrderId: newProcessOrderId.value,
      Status: newProcessStatus.value,
      DateTime: newProcessDateTime.value,
    };
    await axios.post('http://localhost:5146/api/processess/Add', process);
    console.log('Proces dodany!');
    alert('Proces został dodany!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania procesu:', error);
  }
};


const updateProcess = async (processId) => {
  try {
    const process = processes.value.find(m => m.id === processId);
    if (!process) {
      console.error(`Process with ID ${processId} not found.`);
      return;
    }
    
    const updatedProcessData = { 
      SerialNumber: process.serialNumber, 
      OrderId: process.orderId,
      Status: process.status,
      DateTime: process.dateTime,
    };
    if (confirm('Czy chcesz zaktualizować ten proces?')) {
      await axios.put(`http://localhost:5146/api/processess/update/${processId}`, updatedProcessData);
      console.log('Proces zaktualizowany!');
      alert('Proces został zaktualizowany!');
    }
  } catch (error) {
    console.error('Błąd podczas aktualizowania procesu:', error);
  }
};

const confirmUpdateProcess = (processId) => {
  updatingProcessId.value = processId;
  updateProcess(processId);
  updatingProcessId.value = null;
};

const deleteProcess = async (processId) => {
  try {
    await axios.delete(`http://localhost:5146/api/processess/delete/${processId}`);
    processes.value = processes.value.filter(process => process.id !== processId);
    console.log('Prorces usunięty!');
    alert('Proces został usunięty!');
  } catch (error) {
    console.error('Błąd podczas usuwania procesu:', error);
  }
};

const confirmDeleteProcess = (processId) => {
  deletingProcessId.value = processId;
  if (confirm('Czy na pewno chcesz usunąć ten proces?')) {
    deleteProcess(processId);
    deletingProcessId.value = null;
  }
};

const sortedProcesses = computed(() => {
  const sorted = processes.value
    .filter(process => process.serialNumber.toLowerCase().includes(searchTerm.value.toLowerCase()) || process.id.toString().includes(searchTerm.value))
    .sort((a, b) => {
      if (sortByField.value === 'serialNumber') {
        return a.serialNumber.localeCompare(b.serialNumber);
      } else {
        return a.id - b.id;
      }
    });
  return sorted;
});

const sortBy = (field) => {
  sortByField.value = field;
};

const searchProcess = async () => {
  await fetchData();
};

const validateInput = (event) => { 
  event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
};

export { 
  processes, 
  newProcessSerialNumber, 
  newProcessOrderId, 
  newProcessStatus, 
  newProcessDateTime,

  deletingProcessId, 
  updatingProcessId,

  fetchData, 
  addProcess, 
  updateProcess, 
  confirmUpdateProcess, 
  deleteProcess, 
  confirmDeleteProcess,

  sortedProcesses, 
  sortBy, 
  searchProcess,
  sortByField, 
  searchTerm, 

  validateInput };
