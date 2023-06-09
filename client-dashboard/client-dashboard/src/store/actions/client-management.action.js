import axios from 'axios'

export const actions = {

    async getAllClients({ commit }) {

        const { data } = await axios.get('/Client/api/clients/')

        commit('initAllClients')

        data.forEach(client => {
            commit('addClient', client)
        })
    },

    async getClient({ commit }, clientId) {
        const { data } = await axios.get(`/Client/api/clients/${clientId}`)

        commit('getClient', data)
    },

    async addClient({ commit }, client) {

        const { data } = await axios.post('/Client/api/clients/', client)

        commit('addClient', data)

        window.location.href = '/dashboard';
    },

    async updateClient({ commit }, client) {

        const { data } = await axios.put('/Client/api/clients/', client)

        commit('updateClient', data)

        window.location.href = '/dashboard';
    },

    async archiveClient({ commit }, clientId) {

        await axios.post(`/Client/api/clients/${clientId}/archive`)

        commit('archiveClient', clientId)
    }
}