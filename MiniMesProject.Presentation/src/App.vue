<script setup>
import { ref, computed } from 'vue'
import Home from './components/Home.vue'
import MachineInfo from './components/MachineInfo.vue'
import ProductInfo from './components/ProductInfo.vue'
import OrderInfo from './components/OrderInfo.vue'
import ProcessesInfo from './components/ProcessesInfo.vue'
import ParametersInfo from './components/ParametersInfo.vue'
import ProcessParameters from './components/ProcessParameters.vue'
import NotFound from './components/NotFound.vue'

const routes = {
  '/': Home,
  '/machines': MachineInfo,
  '/products': ProductInfo,
  '/orders': OrderInfo,
  '/processes': ProcessesInfo,
  '/parameters': ParametersInfo,
  '/processParameters': ProcessParameters,
}

const currentPath = ref(window.location.hash)

window.addEventListener('hashchange', () => {
  currentPath.value = window.location.hash
})

const currentView = computed(() => {
  return routes[currentPath.value.slice(1) || '/'] || NotFound
})
</script>

<template>
  <div>
    <!-- Navbar -->
    <nav class="navbar">
      <ul>
        <li><a href="#/">Home |</a></li>
        <li><a href="#/machines">Machines Info |</a></li>
        <li><a href="#/products">Products Info |</a></li>
        <li><a href="#/orders">Orders Info |</a></li>
        <li><a href="#/processes">Processes Info |</a></li>
        <li><a href="#/parameters">Parameters Info |</a></li>
        <li><a href="#/processParameters">Process Parameters Info</a></li>
      </ul>
    </nav>

    <!-- Main content -->
    <component :is="currentView" />

    <!-- Footer -->
    <footer>
      <p>&copy; MiniMesApplication 2024</p>
    </footer>
  </div>
</template>

<style scoped>

</style>

