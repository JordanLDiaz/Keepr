<template>
  <!-- TODO if on vault page, give remove vaultKEEP button for vaultkeep owner, but else give remove keep button for keep creator only. Remove keep is hard delete, while remove vaultkeep is soft delete-->
  <div class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
      <div class="container-fluid modal-body">
        <div class="d-flex justify-content-center justify-content-evenly stack">

          <div class="col-md-6 d-flex">
            <img :src="activeKeep?.img" class="img-fluid rounded" :alt="activeKeep?.name" :title="activeKeep?.name"
              :aria-label="activeKeep?.name">
          </div>

          <div class="col-md-5 d-flex">
            <div class="row">
              <h3 class="text-center">
                <span class="mx-2" title="Keep view count"><i class="mdi mdi-eye-outline"></i> {{ activeKeep?.views }}
                </span>
                <span class="mx-2" title="Vault keeps count"><i class="mdi mdi-alpha-k-box-outline"></i> {{
                  activeKeep?.kept
                }} </span>
              </h3>
              <h1 class="text-center" :title="activeKeep?.name" aria-label="activeKeep?.name">{{
                activeKeep?.name
              }}</h1>
              <p class="ms-1" :title="activeKeep?.description" aria-label="activeKeep?.description">{{
                activeKeep?.description
              }}</p>

              <div class="row justify-content-between text-center align-items-end">
                <div class="col-4 d-flex justify-content-start">
                  <div class="dropdown btn-group">
                    <button v-if="user?.isAuthenticated" type="button" class="btn drop-btn dropdown-toggle"
                      data-bs-toggle="dropdown" aria-expanded="false" title="Add keep to my vault"
                      aria-label="Add keep to my vault">
                      Add to Vault
                    </button>
                    <ul class="dropdown-menu">
                      <AddVaultKeep v-for="v in vaults" :key="v?.id" :vault="v" />
                    </ul>
                  </div>
                </div>

                <div class="col-3">
                  <div @click.prevent="removeKeep(keep.id)" class="">
                    <button v-show="activeKeep?.creatorId == account.id" class="btn remove-btn fs-3">
                      <i class="mdi mdi-trash-can" title="Remove Keep" aria-label="Remove Keep"></i>
                    </button>
                  </div>
                </div>

                <div class="col-4 d-flex justify-content-end" v-if="activeKeep" data-bs-dismiss="modal">
                  <router-link :to="{ name: 'Profile', params: { profileId: activeKeep?.creatorId } }">
                    <div class="">
                      <img @click.stop :src="activeKeep?.creator.picture" alt=""
                        class="img-fluid rounded-circle elevation-5" height="50" width="50"
                        :title="`See ${keep.creator?.name}'s Profile`"
                        :aria-label="`See ${keep.creator?.name}'s Profile`">
                    </div>
                  </router-link>
                  <div class="ms-2 d-flex align-items-center">{{ activeKeep?.creator?.name }}</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from 'bootstrap';
export default {
  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      user: computed(() => AppState.user),
      userVaults: computed(() => AppState.userVaults),

      async removeKeep(id) {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this keep?', 'This will delete the entire keep, not just from a vault!')
          if (!yes) {
            return
          }
          await keepsService.removeKeep(id)
          Modal.getOrCreateInstance('#keepDetailsModal').hide()
          Pop.success("Keep successfully deleted.")
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
@media screen AND (max-width: 768px) {
  .stack {
    flex-direction: column;
  }
}

.remove-btn {
  color: #6C5CE7;
}
</style>