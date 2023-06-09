<script>
import { mapState, mapActions } from 'vuex'

export default {
    data() {
        return {
            clients: []
        }
    },
    created() {
        this.getAllClients()
    },
    computed: {
        ...mapState('clientManagement', {
            vuexClients: state => state.clients
        })
    },
    methods: {
        ...mapActions('clientManagement', {
            getAllClients: 'getAllClients',
            archiveClient: 'archiveClient'
        }),
        onEditClient(clientId) {
            this.$router.push(`/edit-client/${clientId}`);
        },
        onArchiveClient(clientId) {
            if (!confirm('Do you want to archive this client?'))
                return;
            this.archiveClient(clientId);
        },
        onAddNewClient() {
            this.$router.push('/add-client');
        }
    },
    watch: {
        vuexClients(newValue) {
            if (newValue) {
                const sign = {}
                const formattedClients = []
                newValue.forEach(client => {
                    if (sign[client.id]) {
                        const formattedClient = formattedClients.filter(formattedClient =>
                            formattedClient.id === client.id)[0].total_phone_numbers++
                        if (client.archived) formattedClient.archived = true
                    } else {
                        formattedClients.push({
                            ...client,
                            total_phone_numbers: 1
                        })
                        sign[client.id] = true
                    }
                })
                this.clients = formattedClients.filter(formattedClient =>
                    !formattedClient.archived)
            }
        }
    }
}
</script>

<template>
    <div class="client-dashboard">
        <h1 class="title">Client Dashboard</h1>
        <table class="table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Total Phone Numbers</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="client, index in clients" :key="index">
                    <td>{{ index + 1 }}</td>
                    <td>{{ client.first_name }}</td>
                    <td>{{ client.last_name }}</td>
                    <td>{{ client.total_phone_numbers }}</td>
                    <td>
                        <button class="edit-button" @click="onEditClient(client.id)">Edit</button>
                        <button class="archive-button" @click="onArchiveClient(client.id)">Archive</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <button class="add-button" @click="onAddNewClient()">Add New Client</button>
    </div>
</template>
  
<style scoped>
.client-dashboard {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.title {
    font-size: 36px;
    margin-bottom: 20px;
    font-style: italic;
    padding: 20px;
    text-shadow: 2px 2px 2px;
}

.table {
    width: 100%;
    max-width: 800px;
    border-collapse: collapse;
}

th,
td {
    padding: 10px;
    text-align: center;
}

th {
    background-color: #eee;
}

td {
    border-bottom: 1px solid #ddd;
}

.actions {
    display: flex;
    justify-content: space-between;
}

.edit-button,
.archive-button {
    padding: 6px 10px;
    border: none;
    border-radius: 4px;
    color: #fff;
    margin: 2px;
    cursor: pointer;
}

.edit-button {
    background-color: #4caf50;
}

.archive-button {
    background-color: #f44336;
}

.add-button {
    margin-top: 20px;
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    background-color: #2196f3;
    color: #fff;
    font-size: 16px;
    cursor: pointer;
}
</style>