<template>
 <div align="center">
   <h1>MovieReviewer</h1>
  <div align="right">
    <button v-on:click="logout">Logout</button>
  </div>
   
<transition name="modal">
      <div v-if="isOpen">
        
        <div class="overlay" @click.self="isOpen = false;">
          <div class="modal">
            <h1 v-if="!deleteMovie">Movie Details</h1>
            <div v-if="!deleteMovie">
            <p>Name <input v-model="name"></p>
            <p>Genre <input v-model="genre"></p>
            </div>
            <div v-if="deleteMovie">
              <p>Are you sure you want to delete the selected movie?</p>
            </div>
            <button v-if="addMovie" v-on:click="add">Add</button>
            <button v-if="updateMovie" v-on:click="update">Update</button>
            <button v-if="deleteMovie" v-on:click="deleteItem" >Delete</button>
            &nbsp;
            <button v-if="!deleteMovie" v-on:click="clearAddForm">Clear</button>&nbsp;
            <button @click="isOpen = false;addMovie=false;updateMovie=false;deleteMovie=false;">Close</button>

          </div>
        </div>
      </div>
    </transition>
    
    <button @click="isOpen = true;addMovie=true;">Add Movie</button> 
    
    <br>
    <br>
    <br>
   <table class="table-hover">
     <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Genre</th>
                    <th>Update</th>
                    <th>Delete</th>
                   
                </tr>
            </thead>
            <tbody v-for="(item) in result" :key="item.Id">
<tr>
<td> {{item.Id}}</td>
<td><button @click="movieDetails(item.Id,item.Name)" >{{item.Name}}</button></td>
<td>{{item.Genre}}</td>
<td><button @click="updateEvent(item.Id)">Update Movie</button></td>
<td><button @click="deleteEvent(item.Id)">Delete Movie</button></td>
</tr>
            </tbody>
   </table>

 </div>
</template>

<script>

import api from '@/MovieReviewer';
import axios from 'axios'
import router from '../router'

export default {
  data() {
    return {
      loading: false,
      name:'',
      genre:'',
      result: '',
      response: '',
      responseFlag:false,
      model: {},
      isOpen: false,
      addMovie: false,
      updateMovie: false,
      tempId:-1,
      deleteMovie:false
    };
  },
  async created() {
    this.getAll()
  },
  methods: {
    async getAll() {
      this.loading = true

      try {
        this.result = await api.getAll()
      } finally {
        this.loading = false
      }
    },

   async add(){
      if(this.name.trim()!='' && this.name!=null && this.name!=undefined &&
      this.genre.trim()!='' && this.genre!=null && this.genre!=undefined
      ){
        var data={'Name':this.name,'Genre':this.genre};
        
        var jsonData=JSON.stringify(data)
        console.log(jsonData)
        try {
        this.response= await api.create(jsonData)
        this.responseFlag=true
        
      } finally {
        location.reload();
        this.clearAddForm()
        console.log( "In finally block.")

      }
      }
      else{
        alert("Enter all values")
      }
    },
    updateEvent(id){
      this.tempId=id
this.isOpen = true
this.updateMovie=true
    },
    deleteEvent(id){
      this.tempId=id
      this.deleteMovie=true
      this.isOpen=true
    },
    movieDetails(id,name){
router.push({ path: 'movie', query: { Id: id,Name:name }})
    },
    async update(){
      if(this.name.trim()!='' && this.name!=null && this.name!=undefined &&
      this.genre.trim()!='' && this.genre!=null && this.genre!=undefined && this.tempId!=-1
      ){
        var data={'Id':this.tempId,'Name':this.name,'Genre':this.genre};
        
        var jsonData=JSON.stringify(data)
        console.log(jsonData)
        try {
        this.response= await api.update(jsonData)
        this.responseFlag=true
        alert(this.response)
        
      } finally {
        location.reload();
        this.clearAddForm()
        console.log( "In finally block.")

      }
      }
      else{
        alert("Enter all values")
      }
    },
    
    async deleteItem(){
      
      if(this.tempId!=-1){
       try {
        this.response= await api.delete(this.tempId)
        this.responseFlag=true
        alert(this.response)
        
      } finally {
       location.reload();
       this.clearAddForm()
        console.log( "In finally block.")

      } 
      }
      else{
        alert("Incorrect Id")
      }
    },
    logout(){
router.push({name:'HelloWorld'})
    },
    clearAddForm: function(event){
      this.name=''
      this.genre=''
      this.tempId=-1
    }
  }
}


</script>
<style scoped>
.modal {
  width: 500px;
  margin: 0px auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px 3px;
  transition: all 0.2s ease-in;
  font-family: Helvetica, Arial, sans-serif;
}
.fadeIn-enter {
  opacity: 0;
}

.fadeIn-leave-active {
  opacity: 0;
  transition: all 0.2s step-end;
}

.fadeIn-enter .modal,
.fadeIn-leave-active.modal {
  transform: scale(1.1);
}


.overlay {
  position: fixed;
  top: 0;
  left: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  background: #00000094;
  z-index: 999;
  transition: opacity 0.2s ease;
}
</style>