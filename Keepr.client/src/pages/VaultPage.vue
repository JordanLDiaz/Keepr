<template>
  <!-- TODO get vault stuff to draw to page... won't for some reason! -->
  <div class="container">
    <div class="row text-center justify-content-center">
      <div class="col-6 my-2">
        <div class="card">
          <img :src="vault?.img" class="vault-cover rounded elevation-5">
          <div class="card-img-overlay text-white text-shadow d-flex flex-column justify-content-between">
            <h3 class="text-uppercase">{{ vault?.name }}</h3>
            <h5>by <span class="text-lowercase">{{ vault?.creator.name }}</span></h5>
          </div>
        </div>
        <h5 class="mt-2"><span>{{ vaultKeeps.length }}</span> Keeps</h5>
      </div>
    </div>

    <div class="row text-center mt">
      <h1>Vault Keeps</h1>
      <div class="col-3" v-for="k in vaultKeeps">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

export default {
  setup() {
    const route = useRoute();

    async function getVault() {
      try {
        await vaultsService.getVault(route.params.vaultId);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function getVaultKeeps() {
      try {
        await vaultKeepsService.getVaultKeeps(route.params.vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      getVault(),
        getVaultKeeps()
    })
    return {
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      getVault,
      getVaultKeeps
    }
  }
};
</script>


<style lang="scss" scoped>
.vault-cover {
  min-width: 40vh;
  max-height: 40vh;
  background-position: center;
  background-size: cover;
}

.text-shadow {
  text-shadow: 2px 2px 4px #000000;
}
</style>