import { userService } from '../_services';
import { router } from '../_helpers';

const state = {
    all: {},
    emp: {
        fullName: '',
        email: '',
        phoneNumber: '',
        graduateDegree: null,
        image: ''
    },
    submitting: false
};

const actions = {
    getAll({ commit }) {
        commit('getAllRequest');
        ;
        userService.getAll()
            .then(
                users => commit('getAllSuccess', users),
                error => commit('getAllFailure', error)
            );
    },

    delete({ commit, dispatch }, id) {
        commit('deleteRequest', id);

        userService.delete(id)
            .then(
                () => dispatch('getAll'),
                error => commit('deleteFailure', { id, error: error.toString() })
            );
    },

    logout({ commit }) {
        // commit('logout');

        userService.logout()
        router.push({name: '/login'})
    },

    editEmployee({ commit }, id){
        router.push({name: `/register?id=${id}`})
    },

    addEditEmployee({ commit }, emp) {
        commit('addEditPending');
        userService.addEditEmployee(emp)
            .then(
                () => {
                    commit('addEditSuccess');
                    router.push('/');
                    // setTimeout(() => {
                    //     // display success message after route change completes
                    //     dispatch('alert/success', 'Registration successful', { root: true });
                    // })
                },
                error => {
                    // commit('registerFailure', error);
                    // dispatch('alert/error', error, { root: true });
                }
            );
    },

    getById({ commit }, id) {
        if (id) {
            userService.getById(id).then(
                emp => {
                    commit('GetEmpSuccess', emp)
                }
            );
        }
        else {
            commit('GetEmpSuccess', {
                fullName: '',
                email: '',
                phoneNumber: '',
                graduateDegree: null,
                image: ''
            })
        }
    }
};

const mutations = {
    getAllRequest(state) {
        state.all = { loading: true };
    },
    getAllSuccess(state, users) {
        state.all = { items: users };
    },
    getAllFailure(state, error) {
        state.all = { error };
    },
    addEditPending(state, error) {
        state.submitting = true;
    },
    addEditSuccess(state, error) {
        state.submitting = false;
    },
    GetEmpSuccess(state, emp) {
        state.emp = emp;
    }
};

export const users = {
    namespaced: true,
    state,
    actions,
    mutations
};
