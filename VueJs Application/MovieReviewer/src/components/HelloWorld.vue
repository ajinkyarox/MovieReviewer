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
   <button  v-on:click="login">Login</button> <br><br>
   <button   @click="createFlag=!createFlag;">Create Account</button>
   <br><br><br>
   <div align="center" v-if="createFlag">
     <table>
  <tr><td> Username</td> <td><input type="text" v-model="uname"></td></tr>
  <tr><br><tr>
  <tr><td>Password</td> <td><input type="password" v-model="pword"></td></tr>
 <tr><br><tr>
  <tr><td>Re-Enter Password</td> <td><input type="password" v-model="repword"></td></tr>
  <tr><br><tr>
  <tr><td></td><td><button  v-on:click="createAccount">Create Account</button>&nbsp; <button  v-on:click="clear">Clear</button></td></tr>
</table>
   </div>
  </div>
</template>

<script>
import api from '@/Login'
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
      password:'',
      createFlag:false,
      uname:'',
      pword:'',
      repword:''
    }
  },
  methods:{
async login(){
  console.log("SSS")
  if(this.username.trim()!='' && this.username!=null && this.username!=undefined &&
  this.password.trim()!='' && this.password!=null && this.password!=undefined){

var data={"username":this.username,"password":this.password}
var jsonData=JSON.stringify(data)
try {
        this.response= await api.login(jsonData)
        
        if(this.response=="Success"){
router.push("/#/moviereviewer")
localStorage.setItem('username', this.username)
console.log(localStorage.getItem('username'))
        }
        else{
          alert(this.response.responseMessage)
        }
      } finally {
        location.reload();
        this.clear()
        console.log( "In finally block.")

      }

   
    //location.reload();
  }
  else{
    alert("Enter all the values")
  }
},
async createAccount(){
  if(this.uname.trim()!='' && this.pword.trim()!='' && this.repword.trim()!='' &&
  this.uname!=null && this.pword!=null && this.repword!=null &&
  this.uname!=undefined && this.pword!=undefined && this.repword!=undefined &&
  this.pword==this.repword){
var data={"username":this.uname,"password":this.pword}
var jsonData=JSON.stringify(data)
try {
        this.response= await api.create(jsonData)
        
        alert(this.response)
      } finally {
        location.reload();
        this.clear()
        console.log( "In finally block.")

      }
  }
  else if(this.pword!=this.repword){
    alert("Enter password correctly.")
  }
  else{
    alert("Enter all the fields.")
  }
},
clear(){
  this.uname=''
  this.pword=''
  this.repword=''
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
