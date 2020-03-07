<template>
<div>
    <h1>Movie Details</h1>
    
   {{Name}}

   <br><br><br>
   Review the movie<br><br>
<transition name="modal">
      <div v-if="isOpen">
        
        <div class="overlay" @click.self="isOpen = false;">
          <div class="modal">
            <h1 v-if="!deleteMovie">Review Details</h1>
            <div v-if="!deleteMovie">
            <p>Review <input v-model="name"></p>
           
            </div>
            <div v-if="deleteMovie">
              <p>Are you sure you want to delete the selected review?</p>
            </div>
            
            <button v-if="updateMovie" v-on:click="update">Update</button>
            <button v-if="deleteMovie" v-on:click="deleteItem" >Delete</button>
            &nbsp;
            <button v-if="!deleteMovie" v-on:click="clearAddForm">Clear</button>&nbsp;
            <button @click="isOpen = false;updateMovie=false;deleteMovie=false;">Close</button>

          </div>
        </div>
      </div>
    </transition>
    <br><br>


<textarea id="message"/>
<br><button v-on:click="addReview">Add Review</button>
<br><br><br>
<div align="center">
<table class="table-hover">
     
            <tbody v-for="(item) in result" :key="item.Rid">
<tr>
<td> <h2>{{item.username}} </h2></td>
<td>{{item.Review}}</td>

<td><button @click="updateEvent(item.Rid)">Update Review</button></td>
<td><button @click="deleteEvent(item.Rid)">Delete Review</button></td>
</tr>
            </tbody>
   </table>



</div>

</div>



</template>
<script>
import api from '@/Movie';
export default {
    data(){
        
     return{  
       name:'',
         result:'',
         Id: this.$route.query.Id,
         Name: this.$route.query.Name,
         loading: false,
         isOpen: false,
      updateMovie: false,
       deleteMovie:false,
     Did:''
     };
    
    },
    async created() {
    this.getAll()
  },
    methods: {
      async getAll() {
      this.loading = true

      try {
        this.result = await api.getAll(this.Id)
        console.log(this.result)
      } finally {
        this.loading = false
      }
    },

updateEvent(id){
      this.Did=id
this.isOpen = true
this.updateMovie=true
    },
        deleteEvent(id){
      this.Did=id
      this.deleteMovie=true
      this.isOpen=true
    },
    
    async deleteItem(){
      
      if(this.Did!=undefined || this.Did!='' || this.Did!=null){
       try {
        this.response= await api.delete(Did)
        this.responseFlag=true
        alert(this.response)
        
      } finally {
       location.reload();
      
        console.log( "In finally block.")

      } 
      }
      
    },


async update(){
      if(this.name.trim()!='' && this.name!=null && this.name!=undefined 
      ){
        var data={'Rid':this.Did,'Review':this.name};
        
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
    },async addReview(){
  console.log(this.result)
        if(message.value.trim()!='' && message.value!=null && message.value!=undefined 
      
      ){
        console.log(localStorage.getItem('username'))
        var data={'Id':this.Id,'Review':message.value,'username':localStorage.getItem('username')};
        
        var jsonData=JSON.stringify(data)
        console.log(jsonData)
        try {
        this.response= await api.create(jsonData)
        this.responseFlag=true
        
      } finally {
        message.value=''
        location.reload();
        
        console.log( "In finally block.")

      }
      }
      else{
        alert("Enter all values")
      }
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