<template>
  <div class="bg-img card my-3 elevation-5">
    <img :src="vault?.img" class="vault-cover rounded">
    <div class="card-img-overlay d-flex align-items-end">
      <h4 class="text-white text-shadow text-uppercase"> {{ vault?.name }}</h4>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultsService } from "../services/VaultsService.js";
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
</style>