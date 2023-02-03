<template>
  <div class="container">
    <div class="masonry-with-columns">
      <div class="" v-for="k in keeps">
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
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => getKeeps());
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      getKeeps,
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 4;
  column-gap: 1.0rem;

  div {
    display: inline-block;
  }
}

@media screen AND (max-width: 768px) {
  .masonry-with-columns {
    columns: 2;
    column-gap: 1.0rem;

    div {
      display: inline-block;
    }
  }
}
</style>
