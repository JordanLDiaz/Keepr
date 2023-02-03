<template>
  <span v-if="routeHomePage">
    <div @click="setActiveKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
      <div class="card my-3 elevation-5">
        <img :src="keep.img" class="rounded card-img" :title="keep.name" :aria-label="keep.name">
        <div class="card-img-overlay d-flex align-items-end justify-content-between pb-1" title="See keep details"
          aria-label="See keep details">
          <h4 class="text-white text-shadow text-lowercase" :title="keep.name" :aria-label="keep.name">{{ keep.name }}
          </h4>
          <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
            <img v-if="keep" :src="keep.creator?.picture" :alt="keep.creator?.name"
              class="img-fluid rounded-circle m-1 elevation-3" height="50" width="50"
              :title="`See ${keep.creator?.name}'s Profile`" :aria-label="`See ${keep.creator?.name}'s Profile`">
          </router-link>
        </div>
      </div>
    </div>
  </span>

  <span v-else-if="routeVaultPage">
    <div @click="setActiveKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
      <div class="card my-3 elevation-5">
        <img :src="keep.img" class="rounded card-img" :title="keep.name" :aria-label="keep.name">
        <div class="card-img-overlay d-flex align-items-end justify-content-between pb-1" title="See keep details"
          aria-label="See keep details">
          <h4 class="text-white text-shadow text-lowercase" :title="keep.name" :aria-label="keep.name">{{ keep.name }}
          </h4>
        </div>
      </div>
    </div>
    <div @click.stop="removeVaultKeep(keep.vaultKeepId)">
      <button v-if="user.isAuthenticated" class="btn remove-btn fs-5">
        <i class="mdi mdi-trash-can" title="Remove Vault Keep" aria-label="Remove Vault Keep"></i>
      </button>
    </div>
  </span>

  <span v-else-if="routeAccountPage">
    <div @click="setActiveKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
      <div class="card my-3 elevation-5">
        <img :src="keep.img" class="rounded card-img" :title="keep.name" :aria-label="keep.name">
        <div class="card-img-overlay d-flex align-items-end justify-content-between pb-1" title="See keep details"
          aria-label="See keep details">
          <h4 class="text-white text-shadow text-lowercase" :title="keep.name" :aria-label="keep.name">{{ keep.name }}
          </h4>
          <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
            <img v-if="keep" :src="keep.creator?.picture" :alt="keep.creator?.name"
              class="img-fluid rounded-circle m-1 elevation-3" height="50" width="50"
              :title="`See ${keep.creator?.name}'s Profile`" :aria-label="`See ${keep.creator?.name}'s Profile`">
          </router-link>
        </div>
      </div>
    </div>
  </span>


  <span v-else-if="routeProfilePage">
    <div @click="setActiveKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
      <div class="card my-3 elevation-5">
        <img :src="keep.img" class="rounded card-img" :title="keep.name" :aria-label="keep.name">
        <div class="card-img-overlay d-flex align-items-end justify-content-between pb-1" title="See keep details"
          aria-label="See keep details">
          <h4 class="text-white text-shadow text-lowercase" :title="keep.name" :aria-label="keep.name">{{ keep.name }}
          </h4>
          <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
            <img v-if="keep" :src="keep.creator?.picture" :alt="keep.creator?.name"
              class="img-fluid rounded-circle m-1 elevation-3" height="50" width="50"
              :title="`See ${keep.creator?.name}'s Profile`" :aria-label="`See ${keep.creator?.name}'s Profile`">
          </router-link>
        </div>
      </div>
    </div>
  </span>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
import { useRoute } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

export default {
  props: {
    keep: { type: Object, required: true },
    // vaultKeep: { type: Object, required: true }
  },

  setup(props) {
    const route = useRoute()
    return {
      routeVaultPage: computed(() => route.name == "Vault"),
      routeHomePage: computed(() => route.name == "Home"),
      routeAccountPage: computed(() => route.name == "Account"),
      routeProfilePage: computed(() => route.name == "Profile"),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      user: computed(() => AppState.user),

      async setActiveKeep(keep) {
        try {
          AppState.activeKeep = keep
          await keepsService.getOne(keep.id)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async removeVaultKeep(vaultKeepId) {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this keep from your vault?', 'This cannot be undone')
          if (!yes) {
            return
          }
          await vaultKeepsService.removeVaultKeep(vaultKeepId)
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
.text-shadow {
  text-shadow: 2px 2px 4px #000000;
}

.remove-btn {
  color: #6C5CE7;
}
</style>