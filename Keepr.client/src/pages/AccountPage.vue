<template>
  <div class="row text-center">
    <div>
      <img class="mt-4 rounded cover-img" :src="account?.coverImg" alt="">
    </div>
    <div>
      <img class="rounded-circle prof-pic" :src="account?.picture" alt="" />
    </div>
    <h1> {{ account?.name }}</h1>
    <div><span>{{ vaults.length }}</span> Vaults | <span>{{ myKeeps.length }}</span> Keeps</div>

    <!-- Account Edit Collapse -->
    <div class="rounded text-white  text-center">
      <button class="btn text-shadow border selectable elevation-2" type="button" data-bs-toggle="collapse"
        data-bs-target="#collapseAccountForm" aria-expanded="false" aria-controls="collapseExample">Update Account
        Details <i class="mdi mdi-pencil"></i></button>
    </div>

    <div class="collapse" id="collapseAccountForm">
      <section class="row justify-content-center">
        <div class="col-6 d-flex justify-content-center">
          <form @submit.prevent="editAccount(account.id)" class="input-group p-3 justify-content-center">
            <div class="">
              <div class="d-flex flex-column">
                <label class="mt-2" for="name">Change Name</label>
                <input class="mt-2 col-12 form-input" type="text" name="name" placeholder="New account name..."
                  v-model="editable.name" />
                <label class="mt-2" for="picture">Profile Picture URL</label>
                <input class="mt-2 form-input" type="url" name="picture" placeholder="New profile picture..."
                  v-model="editable.picture" />
                <label class="mt-2" for="picture">Cover Image Picture URL</label>
                <input class="mt-2 form-input" type="url" name="coverImg" placeholder="New cover image..."
                  v-model="editable.coverImg" />
              </div>
              <div class="mt-2">
                <button type="submit" class="btn btn-info selectable mt-2 hover elevation-2">Submit</button>
              </div>
            </div>
          </form>
        </div>
      </section>
    </div>
  </div>

  <div class="container">
    <div class="row">
      <h2>Vaults</h2>
      <div class="col-3" v-for="v in vaults">
        <VaultCard :vault="v" />
      </div>
    </div>

    <h2>Keeps</h2>
    <div class="masonry-with-columns">
      <div class="" v-for="k in myKeeps">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>

</template>

<script>
import { computed, onMounted, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { useRoute } from 'vue-router';
import { profilesService } from "../services/ProfilesService.js"
import { accountService } from "../services/AccountService.js"

export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      if (AppState.account.id) {
        editable.value = { ...AppState.account }
      }
    })
    // const route = useRoute();
    async function getAccount() {
      try {
        await accountService.getAccount()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function getMyVaults() {
      try {
        await accountService.getMyVaults()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function getMyKeeps() {
      try {
        await accountService.getMyKeeps(AppState.account.id)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    onMounted(async () => {
      // FIXME getMyKeeps fails on refresh
      await getAccount()
      // console.log(AppState.account.id)
      getMyVaults()
      getMyKeeps()
    });
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.myVaults),
      myKeeps: computed(() => AppState.myKeeps),
      getAccount,
      getMyVaults,
      getMyKeeps,
      editable,

      // FIXME edit account not going through...now it is but still weird
      async editAccount() {
        try {
          await accountService.editAccount(editable.value);
          editable.value = {}
          // Pop.toast("Profile updated", "success")
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }

    }
  }
}
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

.text-shadow {
  text-shadow: 2px 2px 4px #777676;
}

.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;

  div {
    width: 100px;
    display: inline-block;
    width: 100%;
  }
}
</style>
