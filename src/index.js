import Vue from 'vue';
import VeeValidate from 'vee-validate';
import VueImageChooser from 'vue-image-chooser'

import { store } from './_store';
import { router } from './_helpers';
import App from './app/App';

Vue.use(VeeValidate);
Vue.use(VueImageChooser);

// setup fake backend
import { configureFakeBackend } from './_helpers';
configureFakeBackend();

new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});