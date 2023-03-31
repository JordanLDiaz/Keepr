<template>
  <div class="container">
    <div class="row text-center">
      <div>
        <img class="mt-4 rounded cover-img" :src="profile?.coverImg" alt="">
      </div>
      <div>
        <img class="rounded-circle prof-pic" :src="profile?.picture" alt="" />
      </div>
      <h3> {{ profile?.name }}</h3>
      <div><span> {{ vaults.length }} </span> Vaults | <span> {{ keeps.length }} </span> Keeps</div>
    </div>
  </div>

  <div class="container">
    <div class="row">
      <div class="">
        <h2>Vaults</h2>
        <div class="col-3" v-for="v in vaults">
          <VaultCard :vault="v" />
        </div>
      </div>

      <h2>Keeps</h2>
      <div class="masonry-with-columns">
        <div class="" v-for="k in keeps">
          <KeepCard :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
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
    })
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
  min-width: 70vh;
  max-height: 40vh;
  background-position: center;
  background-size: cover;
}

.prof-pic {
  max-width: 80px;
  justify-content: center;
}

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