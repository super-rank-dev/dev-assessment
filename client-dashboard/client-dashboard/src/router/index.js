import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import About from '../views/About.vue'
import ClientDashboard from '../components/ClientDashboard.vue'
import AddClient from '../components/AddClient.vue'
import EditClient from '../components/EditClient.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/about',
        name: 'About',
        component: About
    },
    {
        path: '/dashboard',
        name: 'Client Dashboard',
        component: ClientDashboard
    },
    {
        path: '/add-client',
        name: 'Add Client',
        component: AddClient
    },
    {
        path: '/edit-client/:clientId',
        name: 'Edit Client',
        component: EditClient
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
