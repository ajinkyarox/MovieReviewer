import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import MovieReviewer from '@/components/MovieReviewer'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/moviereviewer',
      name: 'MovieReviewer',
      component: MovieReviewer
     
    }
  ]
})
