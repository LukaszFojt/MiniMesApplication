<template>
    <div v-if="order">
      <h2>Szczegóły dla zlecenia o ID: {{ order.id }}</h2>
      <p>Kod zlecenia: {{ order.code }}</p>
      <p>Ilość planowana: {{ order.quantity }}</p>
      <p>ID maszyny: {{ order.machineId }}</p>
      <p>ID produktu: {{ order.productId }}</p>
    </div>
    <div v-else>
      {{ loading ? 'Ładowanie...' : 'Nie znaleziono zamówienia o podanym ID.' }}
    </div>
  </template>
  
  <script setup>
  import axios from 'axios';
  import { ref, onMounted, computed } from 'vue';
  import { useRoute } from 'vue-router';
  
  const orderId = useRoute().params.id;
  const orders = ref([]);
  const order = ref();
  const loading = ref(false);
  
  onMounted(async () => {
    loading.value = true;
    try {
      const response = await axios.get('http://localhost:5146/api/orders');
      orders.value = response.data;
      for (let i = 0; i < orders.value.length; i++) {
        if (orders.value[i].id == parseInt(orderId)) {
          order.value = orders.value[i];
          loading.value = false;
          return; // jeśli znaleziono pasujące zamówienie, przerwij pętlę
        }
      }
      loading.value = false;
    } catch (error) {
      console.error('Błąd podczas pobierania szczegółów zlecenia:', error);
      loading.value = false;
    }
  });
  </script>
  
  <style scoped>
  /* Dodaj stylizację CSS */
  </style>
  