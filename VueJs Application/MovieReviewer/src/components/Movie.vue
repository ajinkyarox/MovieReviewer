<template>
<div>
    <h1>Movie Details</h1>
    
   {{Name}}

   <br><br><br>
   Review the movie<br><br>



<textarea id="message"/>
<br><button v-on:click="addReview">Add Review</button>
</div>
</template>
<script>
import api from '@/Movie';
export default {
    data(){
        console.log(this.$route.query.Id)
     return{  
         message:'',
         Id: this.$route.query.Id,
         Name: this.$route.query.Name,
         
     
     };
    
    },
    methods: {
    async addReview(){
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