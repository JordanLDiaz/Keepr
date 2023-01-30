<template>
  <div class="card my-2 elevation-5">
    <img
      src="https://images.unsplash.com/photo-1675002713677-98516abcb92c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwzM3x8fGVufDB8fHx8&auto=format&fit=crop&w=500&q=60"
      class="rounded">
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

export default {
  props: {
    keep: {
      type: Object, required: true
    }
  },

  setup(props) {
    return {

    }
  }
};
</script>


<style lang="scss" scoped>

</style>