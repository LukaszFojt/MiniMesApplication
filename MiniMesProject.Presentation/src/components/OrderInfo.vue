<template>
  <div class="main-container">
    <h2>Zlecenia</h2>
    <div class="container">
      <h2>Dodaj nowe zlecenie</h2>
      <form @submit.prevent="addOrder">
        <input v-model="newOrderCode" placeholder="Kod zlecenia" @input="validateInput" />
        <input v-model="newOrderQuantity" placeholder="Ilość sztuk zlecenia" type="number" @input="validateInput" />
        <input v-model="newOrderMachineId" placeholder="ID maszyny" type="number" @input="validateInput" />
        <input v-model="newOrderProductId" placeholder="ID produktu" type="number" @input="validateInput" />
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
        <div>
          <p>ID zlecenia:</p>
          <div class="link"><a :href="'/orders/' + order.id">{{ order.id }}</a></div>
        </div>      
        <br/>
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
import { ref, computed, onMounted } from 'vue';
import { orders, newOrderCode, newOrderQuantity, newOrderMachineId, newOrderProductId, sortByField, searchTerm, deletingOrderId, updatingOrderId, fetchData, addOrder, updateOrder, confirmUpdateOrder, deleteOrder, confirmDeleteOrder, sortedOrders, sortBy, searchOrder, validateInput } from '../scripts/orders';

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>
.error {
  color: red;
}
</style>
