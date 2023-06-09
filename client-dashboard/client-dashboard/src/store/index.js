import Vue from 'vue'
import Vuex from 'vuex'
import clientManagementModule from '@/store/modules/client-management.module'
import errorModule from '@/store/modules/error.module'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        clientManagement: clientManagementModule,
        error: errorModule
    }
})
