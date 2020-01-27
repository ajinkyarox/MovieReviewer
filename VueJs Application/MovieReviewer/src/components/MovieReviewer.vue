<template>
 <div>
{{result}}
 </div>
</template>

<script>

import api from '@/MovieReviewer';

export default {
  data() {
    return {
      loading: false,
      result: 'Hello from Vue JS',
      model: {}
    };
  },
  async created() {
    this.getAll()
  },
  methods: {
    async getAll() {
      this.loading = true

      try {
        this.result = this.result+' & '+await api.getAll()
      } finally {
        this.loading = false
      }
    }
  }
}
</script>