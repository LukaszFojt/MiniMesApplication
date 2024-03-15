<script setup>
</script>

<template>
  <div>
    <nav class="navbar">
      <ul>
        <li><a href="/">Strona Główna |</a></li>
        <li><a href="/machines">Maszyny |</a></li>
        <li><a href="/products">Produkty |</a></li>
        <li><a href="/orders">Zlecenia |</a></li>
        <li><a href="/processes">Procesy |</a></li>
        <li><a href="/parameters">Parametry |</a></li>
        <li><a href="/process-parameters">Parametry procesów |</a></li>
        <li><a href="/operator-panel">Panel Operatora</a></li>
      </ul>
    </nav>

    <router-view></router-view>

    <footer>
      <p>&copy; MiniMesApplication 2024</p>
    </footer>
  </div>
</template>

<style scoped>

</style>

