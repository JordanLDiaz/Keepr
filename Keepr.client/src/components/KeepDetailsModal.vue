<template>
  <div class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
      <div class="container-fluid py-0 ps-0 modal-body">
        <div class="row">
          <div class="col-6">
            <img :src="activeKeep?.img" class="img-fluid rounded-start" :alt="activeKeep?.name"
              :title="activeKeep?.name" :aria-label="activeKeep?.name">
          </div>

          <div class="col-6">

            <div class="row mt-4">
              <h3 class="text-center mb-5">
                <span class="mx-1" title="Keep view count"><i class="mdi mdi-eye-outline"></i> {{ activeKeep?.views }}
                </span>
                <span class="mx-1" title="Kept keeps count"><i class="mdi mdi-alpha-k-box-outline"></i> {{
                  activeKeep?.kept
                }} </span>
              </h3>
              <h1 class="text-center mt-5" :title="activeKeep?.name" aria-label="activeKeep?.name">{{
                activeKeep?.name
              }}</h1>
              <p class="mt-2 mb-5 px-5" :title="activeKeep?.description" aria-label="activeKeep?.description">{{
                activeKeep?.description
              }}</p>
            </div>

            <!-- FIXME almost there! drop down closes immediately and sometimes doesn't show list at all wth... :(  -->
            <div class="row justify-content-between text-center">
              <div class="col-5 mx-2">
                <div class="dropdown btn-group">
                  <button type="button" class="btn drop-btn dropdown-toggle" data-bs-toggle="dropdown"
                    aria-expanded="false">
                    Add to Vault
                  </button>
                  <ul class="dropdown-menu">
                    <AddVaultKeep v-for="v in vaults" :key="v?.id" :vault="v" />
                  </ul>
                </div>
              </div>


              <div class="col-5 d-flex flex-row" v-if="activeKeep">
                <router-link :to="{ name: 'Profile', params: { profileId: activeKeep?.creatorId } }">
                  <div class="">
                    <img @click.stop :src="activeKeep?.creator.picture" alt="" class="img-fluid rounded-circle m-1"
                      height="50" width="50">
                  </div>
                </router-link>
                <div class="">{{ activeKeep?.creator.name }}</div>
              </div>
            </div>

            <div class="row justify-content-center">
              <div class="col-3">
                <div @click.prevent="removeKeep(keep.id)" class="">
                  <button v-show="activeKeep?.creatorId == account.id" class="btn btn-success p-2 my-2 hover">
                    <i class="mdi mdi-trash-can">Remove</i>
                  </button>
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
export default {
  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      user: computed(() => AppState.user),
      // userVaults: computed(() => AppState.userVaults)

      async removeKeep(id) {
        try {
          if (await Pop.confirm('Are you sure you want to delete this keep?', 'This cannot be undone'))
            await keepsService.removeKeep(id)
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