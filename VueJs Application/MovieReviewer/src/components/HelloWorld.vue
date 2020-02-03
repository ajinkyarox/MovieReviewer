<template>
  <div align="center" class="hello">
    <h1>{{ msg }}</h1>
    <br>
<table>
  <tr><td> Username</td> <td><input type="text" v-model="username"></td></tr>
  <tr><br><tr>
  <tr><td>Password</td> <td><input type="password" v-model="password"></td></tr>
 
</table>
<br>
   <button  v-on:click="login">Login</button>
  </div>
</template>

<script>
import api from '@/MovieReviewer';
import VueRouter from 'vue-router'
import MovieReviewer from './MovieReviewer'

const router = new VueRouter({
  mode: 'history',
  base: __dirname,
  routes: [
    { path: '/moviereviewer', name: 'moviereviewer', component: MovieReviewer },
    
  ]
})

export default {
  name: 'HelloWorld',
  data () {
    return {
      msg: 'MovieReviewer',
      username:'',
      password:''
    }
  },
  methods:{
login(){
  console.log("SSS")
  if(this.username.trim()!='' && this.username!=null && this.username!=undefined &&
  this.password.trim()!='' && this.password!=null && this.password!=undefined){
    router.push("/#/moviereviewer")
    location.reload();
  }
  else{
    alert("Enter all the values")
  }
}
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
