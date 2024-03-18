import axios from 'axios';
import { ref, onMounted, computed } from 'vue';

const orders = ref([]);
const newOrderCode = ref('');
const newOrderQuantity = ref('');
const newOrderMachineId = ref('');
const newOrderProductId = ref('');
const sortByField = ref('id');
const searchTerm = ref('');
const deletingOrderId = ref(null);
const updatingOrderId = ref(null);

const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/orders');
    orders.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania zleceń:', error);
  }
};

const addOrder = async () => {
  try {
    const order = { 
      Code: newOrderCode.value, 
      Quantity: newOrderQuantity.value,
      MachineId: newOrderMachineId.value,
      ProductId: newOrderProductId.value,
    };
    await axios.post('http://localhost:5146/api/orders/Add', order);
    console.log('Zlecenie dodane!');
    alert('Zlecenie zostało dodane!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania zlecenia:', error);
  }
};

const updateOrder = async (orderId) => {
  try {
    const order = orders.value.find(m => m.id === orderId);
    const updatedOrderData = { 
      Code: order.code, 
      Quantity: order.quantity,
      MachineId: order.machineId,
      ProductId: order.productId,
    };
    if (confirm('Czy chcesz zaktualizować to zlecenie?')) {
        await axios.put(`http://localhost:5146/api/orders/update/${orderId}`, updatedOrderData);
        console.log('Zlecenie zaktualizowane!');
        alert('Zlecenie zostało zaktualizowane!');
      }
    } catch (error) {
      console.error('Błąd podczas aktualizowania zlecenia:', error);
    }
  };

  const confirmUpdateOrder = (OrderId) => {
    updatingOrderId.value = OrderId;
    updateOrder(orderId);
    updatingOrderId.value = null;
  };

const deleteOrder = async (orderId) => {
  try {
    await axios.delete(`http://localhost:5146/api/orders/delete/${orderId}`);
    orders.value = orders.value.filter(order => order.id !== orderId);
    console.log('Zlecenie usunięte!');
    alert('Zlecenie zostało usunięte!');
  } catch (error) {
    console.error('Błąd podczas usuwania zlecenia:', error);
  }
};

const confirmDeleteOrder = (orderId) => {
    deletingOrderId.value = orderId;
    if (confirm('Czy na pewno chcesz usunąć to zlecenie?')) {
      deleteOrder(orderId);
      deletingOrderId.value = null;
    }
  };

const sortedOrders = computed(() => {
  const sorted = orders.value
    .filter(order => order.code.toLowerCase().includes(searchTerm.value.toLowerCase()) || order.id.toString().includes(searchTerm.value))
    .sort((a, b) => {
      if (sortByField.value === 'code') {
        return a.code.localeCompare(b.code);
      } else {
        return a.id - b.id;
      }
    });
  return sorted;
});

const sortBy = (field) => {
  sortByField.value = field;
};

const searchOrder = async () => {
  await fetchData();
};

const validateInput = (event) => { 
  event.target.value = event.target.value.replace(/[^\w\s]/gi, '');
};

export { orders, newOrderCode, newOrderQuantity, newOrderMachineId, newOrderProductId, sortByField, searchTerm, deletingOrderId, updatingOrderId, fetchData, addOrder, updateOrder, confirmUpdateOrder, deleteOrder, confirmDeleteOrder, sortedOrders, sortBy, searchOrder, validateInput };