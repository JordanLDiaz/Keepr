<template>
  <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
    <div class="card my-3 elevation-5">
      <img :src="vault?.img" class="vault-cover rounded">
      <div class="card-img-overlay d-flex align-items-end" title="See vault details" aria-label="See vault details">
        <h3 class="col-11 my-0 text-white text-shadow text-uppercase" :title="vault.name" :aria-label="vault.name"> {{
          vault?.name
        }}</h3>
        <h3 class="my-0">
          <span v-if="vault.isPrivate"><i class="mdi mdi-lock lock" title="Private Vault"
              aria-label="Private Vault"></i></span>
        </h3>
      </div>
    </div>
  </router-link>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { profilesService } from "../services/ProfilesService.js";
export default {
  props: {
    vault: {
      type: Object, required: true
    }
  },

  setup(props) {
    return {
      async setActiveVault(vault) {
        try {
          AppState.activeVault = vault
          await profilesService.getUserVaults(vault.id)
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

.vault-cover {
  max-height: 30vh;
  min-height: 30vh;
  background-size: contain;
  background-position: center;
}

.lock {
  color: #74B9FF;
  text-shadow: 2px 2px 2px #000000;
}
</style>