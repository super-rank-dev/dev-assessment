<script>
import { mapActions, mapState } from 'vuex';

export default {
    data() {
        return {
            client: {
                id: '',
                first_name: '',
                last_name: '',
                phone_numbers: [{ phone_number: '' }]
            }
        }
    },
    created() {
        this.getClient(this.$route.params.clientId)
    },
    computed: {
        ...mapState('clientManagement', {
            vuexClient: state => state.client
        }),
        ...mapState('error', {
            firstNameError: state => state.firstNameError,
            lastNameError: state => state.lastNameError,
            phoneNumberError: state => state.phoneNumberError
        }),
    },
    methods: {
        ...mapActions('clientManagement', {
            getClient: 'getClient',
            updateClient: 'updateClient'
        }),
        ...mapActions('error', {
            validateClientForm: 'validateClientForm'
        }),
        addPhoneNumber() {
            this.client.phone_numbers.push({ phone_number: '' })
        },
        deletePhoneNumber(index) {
            this.client.phone_numbers.splice(index, 1)
        },
        async saveChanges() {
            if (!confirm('Do you really want to update data?'))
                return;
            if (!await this.validateClientForm(this.client)) {
                console.log(this.phoneNumberError)
                return;
            }
            this.updateClient(this.client)
        }
    },
    watch: {
        vuexClient(newValue) {
            this.client = { ...newValue }
        },
        client: {
            handler(newValue) {
                this.validateClientForm(newValue)
            },
            deep: true
        }
    }
}
</script>

<template>
    <div class="edit-client">
        <h1 class="title">Edit Client</h1>
        <form class="form">
            <div class="form-group">
                <label for="firstName">First Name</label>
                <input type="text" id="firstName" v-model="client.first_name" class="form-control" />
                <div v-if="firstNameError" class="error">
                    {{ firstNameError }}
                </div>
                <label for="lastName">Last Name</label>
                <input type="text" id="lastName" v-model="client.last_name" class="form-control" />
                <div v-if="lastNameError" class="error">
                    {{ lastNameError }}
                </div>
            </div>
            <div v-if="phoneNumberError.length <= 0" class="error">
                You must have an address at least.
            </div>
            <div v-for="(phoneNumber, index) in client.phone_numbers" :key="index" class="form-group">
                <label :for="'phoneNumber' + index">Phone Number</label>
                <div>
                    <input :id="'phoneNumber' + index" type="tel" v-model="phoneNumber.phone_number" class="form-control" />
                    <button class="delete-button" type="button" @click="deletePhoneNumber(index)">Delete</button>
                </div>
                <div v-if="phoneNumberError[index]" class="error">
                    {{ phoneNumberError[index] }}
                </div>
            </div>
            <div class="form-group">
                <button class="add-button" type="button" @click="addPhoneNumber()">Add Phone Number</button>
            </div>
            <div class="form-group">
                <button class="save-button" type="submit" @click.prevent="saveChanges()">Save</button>
            </div>
        </form>
    </div>
</template>
  
<style scoped>
.edit-client {
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

.form {
    width: 100%;
    max-width: 500px;
}

.form-group {
    display: flex;
    flex-direction: column;
    margin-bottom: 20px;
}

.form-group div {
    display: flex;
}

.form-group div input {
    width: 80%;
}

.form-group div button {
    margin-left: 10px;
    height: 100%;
}

label {
    font-size: 18px;
    margin-bottom: 5px;
}

input {
    padding: 8px;
    border: 1px solid #ddd;
    border-radius: 4px;
    font-size: 16px;
    margin-bottom: 10px;
}

button {
    padding: 8px 12px;
    font-size: 16px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.delete-button {
    background-color: #f44336;
    color: #fff;
}

.add-button {
    background-color: #2196f3;
    color: #fff;
}

.save-button {
    background-color: #4caf50;
    color: #fff;
}

.archive-button {
    background-color: #f44336;
    color: #fff;
}

.error {
    border: 1px solid #f44336;
    border-radius: 4px;
    padding: 10px;
    margin: 4px;
    display: flex;
    justify-content: center;
    background-color: #f443363e;
}
</style>