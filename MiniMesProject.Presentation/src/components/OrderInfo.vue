<template>
  <div class="main-container">
    <h2>Zlecenia</h2>
    <div class="container">
      <h2>Dodaj nowe zlecenie</h2>
      <form @submit.prevent="addOrder">
        <input v-model="newOrderCode" placeholder="Kod zlecenia" @input="validateInput({ code: newOrderCode })" />
        <input v-model="newOrderQuantity" placeholder="Ilość sztuk zlecenia" type="number" @input="validateInput({ quantity: newOrderQuantity })" />
        <input v-model="newOrderMachineId" placeholder="ID maszyny" type="number" @input="validateInput({ machineId: newOrderMachineId })" />
        <input v-model="newOrderProductId" placeholder="ID produktu" type="number" @input="validateInput({ productId: newOrderProductId })" />
        <button type="submit">Dodaj</button>
      </form>
    </div>
    <div class="sorting-menu">
      <button @click="sortBy('id')">Sortuj po ID</button>
      <button @click="sortBy('code')">Sortuj po kodzie</button>
      <input v-model="searchTerm" class="search" placeholder="Wyszukaj według kodu lub ID" />
      <button @click="searchOrder">Szukaj</button>
    </div>
    <ul class="container">
      <li v-for="order in sortedOrders" :key="order.id">
        <div>ID zlecenia: {{ order.id }}</div>
        <label for="code">Kod:</label>
        <input v-model="order.code" :placeholder="order.code" @input="validateInput(order, 'code')" />
        <label for="quantity">Ilość:</label>
        <input v-model="order.quantity" :placeholder="order.quantity" type="number" @input="validateInput(order, 'quantity')" />
        <label for="machineId">ID maszyny:</label>
        <input v-model="order.machineId" :placeholder="order.machineId" type="number" @input="validateInput(order, 'machineId')" />
        <label for="productId">ID produktu:</label>
        <input v-model="order.productId" :placeholder="order.productId" type="number" @input="validateInput(order, 'productId')" />
        <button @click="updateOrder(order)">Aktualizuj</button>
        <button @click="confirmDeleteOrder(order.id)">Usuń</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
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

onMounted(async () => {
  try {
    const response = await axios.get('http://localhost:5146/api/orders');
    orders.value = response.data;
  } catch (error) {
    console.error('Błąd podczas pobierania zleceń:', error);
  }
});

const addOrder = async () => {
  try {
    const order = { 
      Code: newOrderCode.value, 
      Quantity: newOrderQuantity.value,
      MachineId: { Id: newOrderMachineId.value },
      ProductId: { Id: newOrderProductId.value },
    };
    await axios.post('http://localhost:5146/api/orders/Add', order);
    console.log('Zlecenie dodane!');
    await fetchData();
  } catch (error) {
    console.error('Błąd podczas dodawania zlecenia:', error);
  }
};

const updateOrder = async (order) => {
  try {
    const updatedOrderData = { 
      Code: order.code, 
      Quantity: order.quantity,
      MachineId: order.machineId,
      ProductId: order.productId,
    };
    await axios.put(`http://localhost:5146/api/orders/update/${order.id}`, updatedOrderData);
    console.log('Zlecenie zaktualizowane!');
  } catch (error) {
    console.error('Błąd podczas aktualizowania zlecenia:', error);
  }
};

const deleteOrder = async (orderId) => {
  try {
    await axios.delete(`http://localhost:5146/api/orders/delete/${orderId}`);
    orders.value = orders.value.filter(order => order.id !== orderId);
    console.log('Zlecenie usunięte!');
  } catch (error) {
    console.error('Błąd podczas usuwania zlecenia:', error);
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

const validateInput = (order, field) => { 
  const target = order[field];
  target.value = target.value.replace(/[^\w\s]/gi, ''); // Usuwa znaki specjalne
};

const confirmDeleteOrder = (orderId) => {
  deletingOrderId.value = orderId;
  if (confirm('Czy na pewno chcesz usunąć to zlecenie?')) {
    deleteOrder(orderId);
    deletingOrderId.value = null;
  }
};
</script>
