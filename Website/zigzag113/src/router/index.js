import Vue from 'vue'
import Router from 'vue-router'
import landing1 from '@/components/pages/landing1/Landing1'
import landing2 from '@/components/pages/landing2/Landing2'

Vue.use(Router)

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            name: 'landing1',
            component: landing1
        },
        {
            path: '/download',
            name: 'download',
            component: landing2
        },
    ]
})
