import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'https://localhost:44355/api/Hello',
  json: true
})

export default {
  async execute(method, resource, data) {
    
    return client({
      method,
      url: resource,
      data
      
    }).then(req => {
      return req.data
    })
  },
  getAll() {
    return this.execute('get', '/')
  }
}