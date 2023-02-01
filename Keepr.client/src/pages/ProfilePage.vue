<template>
  <div class="row text-center">
    <div>
      <img class="mt-4 rounded cover-img" :src="account.coverImg" alt="">
    </div>
    <div>
      <img class="rounded-circle prof-pic" :src="account.picture" alt="" />
    </div>
    <h1> {{ account.name }}</h1>
    <div><span> {{ vaults.length }} </span> Vaults | <span> {{ keeps.length }} </span> Keeps</div>
  </div>

  <div class="container">
    <div class="row">
      <h2>Vaults</h2>
      <div class="col-3">
        <VaultCard />
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
import { vaultsService } from "../services/VaultsService.js";
import { profilesService } from "../services/ProfilesService.js";
import { useRoute } from 'vue-router';

export default {
  setup() {
    const route = useRoute();
    async function getUserProfile() {
      try {
        await profilesService.getUserProfile(route.params.profileId);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function getUserKeeps() {
      try {
        await profilesService.getUserKeeps(route.params.profileId);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function getUserVaults() {
      try {
        await profilesService.getUserVaults(route.params.profileId);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      getUserProfile()
      getUserKeeps()
      getUserVaults()
    }
    )
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.userKeeps),
      vaults: computed(() => AppState.userVaults),
      getUserProfile,
      getUserKeeps,
      getUserVaults
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