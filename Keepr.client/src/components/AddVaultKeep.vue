<template>
  <li><a class="dropdown-item" @click.prevent="addVaultKeep(vaultId)">{{ vault.name }}</a></li>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  props: { vault: { type: Object, required: true } },

  setup(props) {
    return {
      async addVaultKeep() {
        try {
          const vaultId = props.vault.id
          console.log(vaultId)
          console.log(props.vault)
          const keepId = AppState.activeKeep.id
          await vaultKeepsService.addVaultKeep(vaultId, keepId)
          Pop.toast("Keep was successfully added to vault", "success")
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