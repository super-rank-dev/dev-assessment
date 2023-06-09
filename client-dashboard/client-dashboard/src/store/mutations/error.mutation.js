export const state = {
    firstNameError: '',
    lastNameError: '',
    phoneNumberError: ['']
}

export const mutations = {
    
    initErrorMsg (state) {
        state.firstNameError = ''
        state.lastNameError = ''
        state.phoneNumberError = ['']
    },

    setFirstNameError (state, error) {
        state.firstNameError = error
    },
    
    setLastNameError (state, error) {
        state.lastNameError = error
    },

    setPhoneNumberError (state, error) {
        state.phoneNumberError = error
    }
}