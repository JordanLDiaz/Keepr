<template>
  <div class="text-center">
    <div>
      <img class="mt-4 rounded cover-img" :src="account.coverImg" alt="">
    </div>
    <div>
      <img class="rounded-circle prof-pic" :src="account.picture" alt="" />
    </div>
    <h1> {{ account.name }}</h1>
    <div><span>{{ vaults.length }}</span> Vaults | <span>{{ keeps.length }}</span> Keeps</div>
  </div>

  <div class="">
    <h2>Vaults</h2>
  </div>

  <div class="">
    <h2>Keeps</h2>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped>
.cover-img {
  max-height: 40vh;
}

.prof-pic {
  max-width: 80px;
  justify-content: center;
}
</style>
