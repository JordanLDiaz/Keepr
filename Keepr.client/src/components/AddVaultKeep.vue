<template>
  <li><a class="dropdown-item" @click.prevent="addVaultKeep">{{ vault.name }}</a></li>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultKeepsService } from "../services/VaultKeepsService.js";
export default {
  props: { vault: { type: Object, required: true } },

  setup(props) {
    return {
      async addVaultKeep() {
        try {
          const vaultId = props.vault.vaultId
          const keepId = AppState.activeKeep.id
          await vaultKeepsService.addVaultKeep(vaultId, keepId)
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