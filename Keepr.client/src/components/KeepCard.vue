<template>
  <!-- NOTE doesnt have to show name of creator on keep when in vault page... v-if to remove somehow... VaultKeep will need delete button, see below? Make a separate vaultKeep modal just like keep modal! may as well make vault keep component too -->
  <!-- <div class="">
          <button v-if="vaultKeep.creatorId == account.Id" @click="deleteVaultKeep()" class="btn btn-danger text-shadow"><i class="mdi mdi-delete"></i></button>
        </div> -->

  <div @click="setActiveKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
    <div class="bg-img card my-3 elevation-5">
      <img :src="keep.img" class="rounded card-img" :title="keep.name" :aria-label="keep.name">
      <div class="card-img-overlay d-flex align-items-end justify-content-between pb-1">

        <h4 class="text-white text-shadow text-lowercase" :title="keep.name" :aria-label="keep.name">{{ keep.name }}
        </h4>
        <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
          <img :src="keep.creator.picture" :alt="keep.creator.name" class="img-fluid rounded-circle m-1 elevation-3"
            height="50" width="50" :title="keep.creator.name" :aria-label="keep.creator.name">
        </router-link>
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
    keep: { type: Object, required: true },
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
.text-shadow {
  text-shadow: 2px 2px 4px #000000;
}
</style>