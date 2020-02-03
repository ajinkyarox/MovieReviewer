import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'https://localhost:44380/movies/',
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
  getAll() {
    return this.execute('get', '/GETALL')
  },
  create(data) {
    return this.execute('post', '/POSTMOVIE', data)
  },
  update(data){
    return this.execute('put','/PUTMOVIE',data)
  }
}