<template>
  <div class="row text-center">
    <div>
      <img class="mt-4 rounded cover-img" :src="account.coverImg" alt="">
    </div>
    <div>
      <img class="rounded-circle prof-pic" :src="account.picture" alt="" />
    </div>
    <h1> {{ account.name }}</h1>
    <div><span>{{ vaults.length }}</span> Vaults | <span>{{ keeps.length }}</span> Keeps</div>
  </div>

  <div class="container">

    <div class="row">
      <h2>Vaults</h2>
      <div class="col-3">
        <img
          src="https://images.unsplash.com/photo-1454991727061-be514eae86f7?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTB8fHNlYSUyMGNyZWF0dXJlc3xlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60"
          class="vault-cover rounded">
      </div>
    </div>

    <div class="row">
      <h2>Keeps</h2>
      <div class="col-3" v-for="k in keeps">
        <KeepCard :keep="k" />
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
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => getAllKeeps())
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
};
</script>


<style lang="scss" scoped>
.cover-img {
  max-height: 40vh;
}

.vault-cover {
  max-width: 40vh;
  max-height: 40vh;
  background-position: center;
  background-size: cover;
}

.prof-pic {
  max-width: 80px;
  justify-content: center;
}
</style>