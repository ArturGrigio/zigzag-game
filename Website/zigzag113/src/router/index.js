import Vue from 'vue'
import Router from 'vue-router'
import landing1 from '@/components/Landing1'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'landing1',
      component: landing1
    }
  ]
})
