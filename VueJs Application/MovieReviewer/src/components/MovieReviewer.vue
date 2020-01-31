<template>
 <div align="center">
   <h1>MovieReviewer</h1>
  
   
<transition name="modal">
      <div v-if="isOpen">
        <div class="overlay" @click.self="isOpen = false;">
          <div class="modal">
            <h1>Movie Details</h1>
            <p>Name <input v-model="name" placeholder="edit me"></p>
            <p>Genre <input v-model="genre" placeholder="edit me"></p>
            <button v-on:click="addMovie">Add</button>&nbsp;
            <button v-on:click="clearAddForm">Clear</button>&nbsp;
            <button @click="isOpen = false;">Close</button>

          </div>
        </div>
      </div>
    </transition>
    <div v-if="!isOpen">
    <button @click="isOpen = true;">Add Movie</button> 
    </div> 
    <br>
   <table class="table-hover">
     <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Genre</th>
                    
                   
                </tr>
            </thead>
            <tbody v-for="(item) in result" :key="item.Id">
<tr>
<td>{{item.Id}}</td>
<td>{{item.Name}}</td>
<td>{{item.Genre}}</td>
</tr>
            </tbody>
   </table>

 </div>
</template>

<script>

import api from '@/MovieReviewer';
import axios from 'axios'

export default {
  data() {
    return {
      loading: false,
      name:'',
      genre:'',
      result: '',
      model: {},
      isOpen: false
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

    addMovie: function(event){
      if(this.name.trim()!='' && this.name!=null && this.name!=undefined &&
      this.genre.trim()!='' && this.genre!=null && this.genre!=undefined
      ){
        var data={'Name':this.name,'Genre':this.genre};
        
        var jsonData=JSON.stringify(data)
        console.log(jsonData)
        axios.post('https://localhost:44380/movies/POSTMOVIE',  jsonData ,
                {
        headers: {
          'Content-type': 'application/json'
        }
      }
                )
                .then(function (response) {
                    currentObj.output = response.data;
                    alert(currentObj.output)
                    //vm.$forceUpdate();
                })
                .catch(function (error) {
                    currentObj.output = error;
                   alert(currentObj.output )
                    //vm.$forceUpdate();
                });
      }
    },
    clearAddForm: function(event){
      this.name=''
      this.genre=''
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