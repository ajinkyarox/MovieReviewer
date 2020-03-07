import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'https://localhost:44380/Moviereviews/',
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
  getAll(Id) {
    return this.execute('get', '/GETALL?Id='+Id)
  },
  create(data) {
    return this.execute('post', '/POSTREVIEW', data)
  },
  update(data){
    return this.execute('put','/PUTREVIEW',data)
  },
  delete(id){
    return this.execute('delete','/DELETEREVIEW?Rid='+id)
  }
}