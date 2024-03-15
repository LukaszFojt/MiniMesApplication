<template>
    <div class="main-container">
      <h2>Panel Operatora</h2>
        <ul class="container panel-options">
          <li>
              <select class="select-button" v-model="selectedOrderCode">
                  <option selected="selected">Wybierz zlecenie</option>
                  <option v-for="order in sortedOrders" :key="order.id" :value="order.id">{{ order.code }}</option>             
              </select>         
          </li>
          <li>
              <select class="select-button" v-model="selectedProductName">
                <option selected="selected">Wybierz produkt</option>
                  <option v-for="product in sortedProducts" :key="product.id" :value="product.id">{{ product.name }}</option>             
              </select>         
          </li>
          <li>
            <div class="select-button" v-if="selectedOrder">
              <div>ID zlecenia: 
                <p>{{ selectedOrder.id }}</p>
              </div>
              <div>Kod zlecenia: 
                <p>{{ selectedOrder.code }}</p>
              </div>
              <div>Ilość sztuk zlecenia: 
                <p>{{ selectedOrder.quantity }}</p>
              </div>
            </div>
          </li>
          <li>
            <div class="select-button" v-if="selectedProduct">
              <div>ID produktu:
                <p>{{ selectedProduct.id }}</p>
              </div>
              <div>Nazwa produktu:
                <p>{{ selectedProduct.name }}</p>
              </div>
            </div>
          </li>
          <li>
            <div class="select-button"></div>
          </li>
          <li>
            <div class="select-button"></div>
          </li>
        </ul>

      <div class="container">
        <h2>Rejestracja realizacji procesu zlecenia produkcyjnego</h2>
        <form @submit.prevent="addProcess">
          <input v-model="newOperatorSerialNumber" placeholder="Numer seryjny procesu" />
          <input v-model="newOperatorStatus" placeholder="Status procesu"/>
          <input v-model="selectedOrderCode" placeholder="ID zlecenia" type="number"/>
          <input v-model="newOperatorDateTime" type="datetime-local"/>               
          <button type="submit">Dodaj</button>
        </form>
      </div>

      <div class="container">
        <h3>Historia</h3>
        <div class="sorting-menu">
          <button @click="sortBy('id')">Sortuj po ID</button>
          <button @click="sortBy('serialNumber')">Sortuj po numerze seryjnym</button>
          <button @click="sortBy('dateTime')">Sortuj po dacie dodania</button>
          <input v-model="searchTerm" class="search" placeholder="Wyszukaj proces" />
          <button @click="searchProcess">Szukaj</button>
        </div>
        <ul>
          <div class="elements">
            <p>ID</p>
            <p>Numer seryjny procesu</p>
            <p>Status procesu</p>
            <p>Data rozpoczęcia procesu</p>
            <p>ID zlecenia</p>
          </div>
          <li class="elements" v-for="process in sortedProcesses" :key="process.id">
            <p>{{ process.id }}</p> 
            <input v-model="process.serialNumber" placeholder="Numer seryjny"/>
            <input v-model="process.status" placeholder="Status procesu" />
            <input v-model="process.dateTime" type="datetime-local" />
            <input v-model="process.orderId" placeholder="ID zlecenia" type="number" />
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script setup>
  import { onMounted } from 'vue';
  import { 
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
} from '../scripts/operatorPanel';
  
  onMounted(async () => {
    await fetchOrders();
    await fetchProducts();
    await fetchProcesses();
  });
  </script>
  
  <style scoped>

    .select-button {
      width: 290px;
      height: 200px;
      border-radius: 10px;
      background-color: #0056b3;
      color: white;

      font-weight: bold;
      font-size: large;
      text-align: center;
      display: flex;
      justify-content: center;
      align-items: center;

      margin-right: 10px;
      margin-bottom: 10px;
    }

    .select-button:hover {
      background-color: #1c87fa;
    }

    .select-button div {
      font-size: small;
      padding: 5px;
      margin: auto;
    }

    .select-button div p {
      font-size: medium;
    }

    .panel-options {
      display: flex;
      justify-content: center;
      margin-right: 20px;
    }

    .elements {
      list-style: none;
      display: grid;
      grid-template-columns: 1fr 4fr 2fr 6fr 2fr;
    }

    li {
      list-style-type: none;
    }

  </style>
  