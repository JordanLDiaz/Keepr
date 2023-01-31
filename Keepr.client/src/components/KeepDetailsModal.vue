<template>
  <div class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
      <div class="container-fluid py-0 ps-0 modal-body">
        <div class="row">
          <div class="col-6">
            <img :src="activeKeep?.img" class="img-fluid rounded-start">
          </div>
          <div class="col-6">
            <div class="row my-5">
              <h3 class="text-center my-5">
                <span class="mx-1"><i class="mdi mdi-eye-outline"></i> {{ activeKeep?.views }} </span>
                <span class="mx-1"><i class="mdi mdi-alpha-k-box-outline"></i> {{ activeKeep?.kept }} </span>
              </h3>
              <h1 class="text-center my-5">{{ activeKeep?.name }}</h1>
              <p class="my-5 px-5">{{ activeKeep?.description }}</p>
            </div>
            <div class="row">
              <div class="col-6">
                <h5>Vault Dropdown</h5>
                <button type="button" class="btn btn-primary">Save</button>
              </div>
              <div class="col-6">
                <!-- TODO add router to creator profile -->
                <img :src="activeKeep?.creator.picture" alt="" class="img-fluid rounded-circle m-1">
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