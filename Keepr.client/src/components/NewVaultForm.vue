<template>
  <div class="modal-dialog modal-dialog-centered modal-l">
    <div class="modal-content">
      <div class="modal-header">
        <h4>ADD YOUR VAULT</h4>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <form @submit.prevent="createVault()">
        <div class="modal-body">
          <div class="form-floating elevation-5 my-3">
            <input v-model="editable.name" type="text" maxlength="50" class="form-control" id="name"
              placeholder="Name of vault..." required>
            <label for="Vault name" class="form-label">Vault name...</label>
          </div>
          <div class="form-floating elevation-5 my-3">
            <input v-model="editable.img" type="url" class="form-control" id="img"
              placeholder="Vault cover image url..." required>
            <label for="Vault cover image" class="form-label">Vault cover image url...</label>
          </div>
          <div class="form-floating elevation-5 my-3">
            <textarea v-model="editable.description" class="form-control" id="description"
              placeholder="Vault description..." required></textarea>
            <label for="Vault description" class="form-label">Vault description...</label>
          </div>
          <div class="form-check my-3">
            <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
            <label class="form-check-label" for="flexCheckDefault">
              Make Vault Private?
            </label>
            <br />
            <i>Private vaults can only be seen by you.</i>
          </div>
          <div class="my-3">
            <button type="submit" class="btn btn-success bg-success" title="Create a new vault"
              aria-label="Create a new vault">Create Vault</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js"
import { Modal } from 'bootstrap';
import { useRouter } from 'vue-router';

export default {
  setup() {
    const editable = ref({ isPrivate: false })
    const router = useRouter()

    return {
      editable,

      async createVault() {
        try {
          const vault = await vaultsService.createVault(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#NewVaultForm').hide();
          router.push({ name: 'Vault', params: { vaultId: vault.id } })
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