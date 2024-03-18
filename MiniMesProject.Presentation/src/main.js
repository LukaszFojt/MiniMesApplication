import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router'
import OrderInfo from './components/OrderInfo.vue'
import OrderDetail from './components/OrderDetail.vue'
import Home from './components/Home.vue'
import MachineInfo from './components/MachineInfo.vue'
import ProductInfo from './components/ProductInfo.vue'
import ProcessesInfo from './components/ProcessesInfo.vue'
import ParametersInfo from './components/ParametersInfo.vue'
import ProcessParameters from './components/ProcessParameters.vue'
import OperatorPanel from './components/OperatorPanel.vue'
import MachineParameters from './components/MachineParameters.vue'
import NotFound from './components/NotFound.vue'

const router = createRouter({
    routes:[
        {path: "/", component: Home },
        {path: "/machines", component: MachineInfo },
        {path: "/products", component: ProductInfo },
        {path: "/orders", component: OrderInfo },
        {path: "/orders/:id([0-9]+)", component: OrderDetail },
        {path: "/processes", component: ProcessesInfo },
        {path: "/parameters", component: ParametersInfo },
        {path: "/process-parameters", component: ProcessParameters },
        {path: "/operator-panel", component: OperatorPanel },
        {path: "/machine-parameters", component: MachineParameters},
        {path: "/:pathMatch(.*)*", component: NotFound}
    ],
    history: createWebHistory()
})

createApp(App).use(router).mount('#app')
