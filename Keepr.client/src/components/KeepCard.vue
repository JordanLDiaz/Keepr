<template>
  <div @click="setActiveKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
    <div class="card my-3 elevation-5">
      <img :src="keep.img" class="rounded">
      <div class="row">
        <h4>{{ keep.name }}</h4>
        <div class="col-2">
          <img :src="keep.creator.picture" alt="keep.creator.name" class="img-fluid rounded-circle m-1">
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    keep: {
      type: Object, required: true
    }
  },

  setup(props) {
    return {
      async setActiveKeep(keep) {
        try {
          AppState.activeKeep = keep
          await keepsService.getOne(keep.id)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>