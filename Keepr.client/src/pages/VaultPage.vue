<template>
  <div class="container">
    <div class="row text-center justify-content-center">
      <div class="col-6 my-2">
        <div class="card">
          <img :src="vault?.img" class="vault-cover rounded elevation-5">
          <div class="card-img-overlay text-white text-shadow d-flex flex-column justify-content-between">
            <h3 class="text-uppercase">{{ vault?.name }} <span v-if="vault?.isPrivate" class="mdi mdi-lock lock"></span>
            </h3>
            <h5>by <span class="text-lowercase">{{ vault?.creator.name }}</span></h5>
          </div>
        </div>
        <h5 class="mt-2"><span>{{ vaultKeeps.length }}</span> Keeps</h5>
        <div @click.prevent="removeVault(vault?.id)" class="">
          <button v-show="vault?.creatorId == account?.id" class="btn remove-btn fs-1">
            <i class="mdi mdi-trash-can" title="Remove Vault" aria-label="Remove Vault"></i>
          </button>
        </div>
      </div>
    </div>


    <!-- TODO add delete button on keep card for keep vaults -->
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
import { computed, reactive, onMounted, ref } from 'vue';
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { router } from "../router.js";

export default {
  setup() {
    const route = useRoute();
    const isPrivate = ref(false);

    async function getVault() {
      try {
        await vaultsService.getVault(route.params.vaultId);
      } catch (error) {
        isPrivate.value = true
        Pop.error("You do not have permission to view others private vaults", "error")
        setTimeout(() => {
          router.push({ name: 'Home' })
        });
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
      account: computed(() => AppState.account),
      getVault,
      getVaultKeeps,

      async removeVault(vaultId) {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this vault?', 'This cannot be undone')
          if (!yes) {
            return
          }
          await vaultsService.removeVault(vaultId)
          Pop.success("Vault was successfully deleted.")
          router.push({ name: 'Home' })
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
.vault-cover {
  min-width: 40vh;
  max-height: 40vh;
  background-position: center;
  background-size: cover;
}

.text-shadow {
  text-shadow: 2px 2px 4px #000000;
}

.remove-btn {
  color: #6C5CE7;
}

.lock {
  color: #74B9FF;
  text-shadow: 2px 2px 2px #000000;
}
</style>