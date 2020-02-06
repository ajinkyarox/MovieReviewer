import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'https://localhost:44380/logincredentials/',
  json: true
})

export default {
  async execute(method, resource, data) {
    
  
    return client({
      method,
      url: resource,
      data,
      headers:{
        'Content-Type': 'application/json'
      }
      
    }).then(req => {
      return req.data
    })
  },
 
  create(data) {
    return this.execute('post', '/POSTLOGIN', data)
  },
  login(data){
      return this.execute('post','/LOGIN',data)
  }
}