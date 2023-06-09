export const actions = {

    async validateClientForm({ commit }, client) {
        let sign = true
        commit('initErrorMsg')
        if (client.first_name.length <= 0) {
            commit('setFirstNameError', 'Input your first name.')
            sign = false
        }
        if (client.last_name.length <= 0) {
            commit('setLastNameError', 'Input your last name.')
            sign = false
        }
        const phoneNumberError = client.phone_numbers.map(({ phone_number }) => {
            if (phone_number.length <= 0) {
                sign = false
                return 'Input your phone number.'
            } else {
                return ''
            }
        })
        if (phoneNumberError.length <= 0) {
            sign = false
        }
        commit('setPhoneNumberError', phoneNumberError)
        return sign;
    }

}