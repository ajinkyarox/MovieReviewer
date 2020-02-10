<template>
<div>
    <h1>Movie Details</h1>
    
   {{Id}}
</div>
</template>
<script>
export default {
    data(){
        console.log(this.$route.query.Id)
     return{  Id: this.$route.query.Id};
    
    }
}
</script>