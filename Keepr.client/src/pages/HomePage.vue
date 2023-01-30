<template>
  <div class="container">
    <div class="row">
      <div class="col-3" v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => getAllKeeps());
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      getAllKeeps,
    }
  }
}
</script>

<style scoped lang="scss">

</style>
