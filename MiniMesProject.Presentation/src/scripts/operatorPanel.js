import axios from 'axios';
import { ref, computed, watchEffect} from 'vue';
import jsPDF from 'jspdf';
import 'jspdf-autotable'; // Dodatkowa biblioteka do renderowania tabel

const newOperatorSerialNumber = ref('');
const newOperatorStatus = ref('');
const newOperatorOrderId = ref();
const newOperatorProductId = ref();
const newOperatorDateTime = ref('');

const orders = ref([]);
const selectedOrderCode = ref(null);
const selectedOrder = ref(null);

const products = ref([]);
const selectedProductName = ref(null);
const selectedProduct = ref(null);

const processes = ref([]);
const sortByField = ref('id');
const searchTerm = ref('');

const fetchOrders = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/orders');
    orders.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania zleceń:', error);
  }
};

const sortedOrders = computed(() => {
  const sorted = orders.value
    .sort((a, b) => {
      return a.id - b.id;     
    });
  return sorted;
});


  const fetchProducts = async () => {
    try {
      const response = await axios.get('http://localhost:5146/api/products');
      products.value = response.data;
    } catch (error) {
      console.error('Błąd podczas pobierania produktów:', error);
    }
  };

  const sortedProducts = computed(() => {
    const sorted = products.value
      .sort((a, b) => {
        return a.id - b.id;     
      });
    return sorted;
  });

  const fetchProcesses = async () => {
    try {
      const response = await axios.get('http://localhost:5146/api/processess');
      processes.value = response.data;
    } catch (error) {
      console.error('Błąd podczas pobierania procesów:', error);
    }
  };

  const sortedProcesses = computed(() => {
    const sorted = processes.value
      .filter(process => 
        process.serialNumber.toLowerCase().includes(searchTerm.value.toLowerCase()) || 
        process.id.toString().includes(searchTerm.value)) 
      .sort((a, b) => {
        if (sortByField.value === 'serialNumber') {
          return a.serialNumber.localeCompare(b.serialNumber);
        } 
        else if (sortByField.value === 'dateTime') {
          return a.dateTime.localeCompare(b.dateTime);
        }        
        else {
          return a.id - b.id;
        }
      });
    return sorted;
  });

  const addProcess = async () => {
    try {
      const process = { 
        SerialNumber: newOperatorSerialNumber.value, 
        OrderId: selectedOrderCode.value,
        Status: newOperatorStatus.value,
        DateTime: newOperatorDateTime.value,
      };
      await axios.post('http://localhost:5146/api/processess/Add', process);
      console.log('Proces dodany!');
      alert('Proces został dodany!');
      await fetchProcesses();
    } catch (error) {
      console.error('Błąd podczas dodawania procesu:', error);
    }
  };
  
  const sortBy = (field) => {
    sortByField.value = field;
  };
  
  const searchProcess = async () => {
    await fetchProcesses();
  };

const validateInput = (event) => { 
  event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
};

watchEffect(() => {
    if (selectedOrderCode.value) {
      selectedOrder.value = sortedOrders.value.find(order => order.id === selectedOrderCode.value);     
    } else {
      selectedOrder.value = null;
    }

    if (selectedProductName.value) {
        selectedProduct.value = sortedProducts.value.find(product => product.id === selectedProductName.value);
      } else {
        selectedProduct.value = null;
      }
  });

  const exportToPDF = async () => {
    const doc = new jsPDF();
    doc.autoTable({ html: '#processData' });
    doc.save('process_data.pdf');
  };

export { 
  newOperatorSerialNumber, 
  newOperatorOrderId, 
  newOperatorProductId, 
  newOperatorStatus, 
  newOperatorDateTime, 

  fetchOrders, 
  sortedOrders, 
  selectedOrder,
  selectedOrderCode,

  fetchProducts,
  sortedProducts,
  selectedProduct,
  selectedProductName,

  fetchProcesses,
  addProcess,

  sortedProcesses,
  sortBy,
  searchProcess,
  sortByField,
  searchTerm,

  validateInput,
  watchEffect,
  exportToPDF,
};
