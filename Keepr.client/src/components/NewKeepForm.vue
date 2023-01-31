<template>
  <div class="modal-dialog modal-dialog-centered modal-l">
    <div class="modal-content">
      <div class="modal-header">
        <h4>ADD YOUR KEEP</h4>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <form @submit.prevent="createKeep">
        <div class="modal-body">
          <div class="form-floating elevation-5 my-3">
            <input v-model="editable.name" type="text" maxlength="50" class="form-control" id="name"
              placeholder="Name of keep..." required>
            <label for="Keep name" class="form-label">Keep name...</label>
          </div>
          <div class="form-floating elevation-5 my-3">
            <input v-model="editable.img" type="url" class="form-control" id="img" placeholder="Keep image url..."
              required>
            <label for="Keep image" class="form-label">Keep image url...</label>
          </div>
          <div class="form-floating elevation-5 my-3">
            <textarea v-model="editable.description" class="form-control" id="description"
              placeholder="Keep description..." required></textarea>
            <label for="keep description" class="form-label">Keep description...</label>
          </div>
          <div class="">
            <button type="submit" class="btn btn-success bg-success">Create Keep</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { Modal } from 'bootstrap';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#NewKeepForm').hide()
          Modal.getOrCreateInstance('#keepDetailsModal').show()
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