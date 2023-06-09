import Vue from 'vue'

export const state = {
    clients: [],
    client: {}
}

export const mutations = {

    initAllClients(state) {
        Vue.set(state, 'clients', [])
    },

    getClient(state, client) {
        Vue.set(state, 'client', client)
    },

    addClient(state, client) {
        Vue.set(state, 'clients', [...state.clients, client])
    },

    updateClient(state, updatedClient) {
        const index = state.clients.findIndex(client => client.id === updatedClient.id)
        if (index !== -1) {
            Vue.set(state.clients, index, updatedClient)
        }
    },

    archiveClient(state, clientId) {
        const index = state.clients.findIndex(client => client.id === clientId)
        if (index !== -1) {
            Vue.set(state.clients[index], 'archived', true)
        }
        state.clients = [...state.clients]
    }
}