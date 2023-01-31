<template>
  <div class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
      <div class="container-fluid py-0 ps-0 modal-body">
        <div class="row">
          <div class="col-6">
            <img :src="activeKeep?.img" class="img-fluid rounded-start" :alt="activeKeep?.name"
              :title="activeKeep?.name" :aria-label="activeKeep?.name">
          </div>

          <div class="col-6">

            <div class="row mt-4">
              <h3 class="text-center mb-5">
                <span class="mx-1" title="Keep view count"><i class="mdi mdi-eye-outline"></i> {{ activeKeep?.views }}
                </span>
                <span class="mx-1" title="Kept keeps count"><i class="mdi mdi-alpha-k-box-outline"></i> {{
                  activeKeep?.kept
                }} </span>
              </h3>
              <h1 class="text-center mt-5" :title="activeKeep?.name" aria-label="activeKeep?.name">{{
                activeKeep?.name
              }}</h1>
              <p class="mt-2 mb-5 px-5" :title="activeKeep?.description" aria-label="activeKeep?.description">{{
                activeKeep?.description
              }}</p>
            </div>

            <div class="row justify-content-between">
              <div class="col-5 mx-2 d-flex flex-row">
                <h5 class="">Vault Dropdown</h5>
                <button type="button" class="btn btn-secondary rounded-pill">Save</button>
              </div>
              <div class="col-5 d-flex flex-row">
                <router-link :to="{ name: 'Profile' }">
                  <img @click.stop :src="activeKeep?.creator.picture" alt="" class="img-fluid rounded-circle m-1"
                    height="50" width="50">
                </router-link>
                <div class="">{{ activeKeep?.creator.name }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      // keep: computed(() => AppState.activeKeep)
    }
  }
};
</script>


<style lang="scss" scoped>

</style>