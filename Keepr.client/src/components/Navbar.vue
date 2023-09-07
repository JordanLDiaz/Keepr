<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light px-3 elevation-2">
    <div class="d-flex">
      <router-link :to="{ name: 'Home' }">
        <button class="border-secondary bg-grey rounded-pill my-2 px-3 py-1" title="Home"
          aria-label="Home button">Home</button>
      </router-link>

      <div v-if="user.isAuthenticated">
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDarkDropdown"
          aria-controls="navbarNavDarkDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavDarkDropdown">
          <ul class="navbar-nav">
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle my-1 px-4" href="#" role="button" data-bs-toggle="dropdown"
                aria-expanded="false" aria-label="Create New Keep or Vault" title="Create New Keep or Vault">
                Create
              </a>
              <ul class="dropdown-menu dropdown-menu-dark">
                <div class="selectable" data-bs-toggle="modal" data-bs-target="#NewKeepForm" title="Create Keep"
                  aria-label="Create Keep">
                  <li><a class="dropdown-item" href="#">Create Keep</a></li>
                </div>
                <div class="selectable" data-bs-toggle="modal" data-bs-target="#NewVaultForm" title="Create Vault"
                  aria-label="Create Vault">
                  <li><a class="dropdown-item" href="#">Create Vault</a>
                  </li>
                </div>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </div>

    <router-link :to="{ name: 'Home' }">
      <div class="">
        <img alt="logo" src="../assets/img/Keepr-logo.png" height="50" title="Home" aria-label="Home button" />
      </div>
    </router-link>

    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>

  <ModalComponent id="NewKeepForm">
    <NewKeepForm />
  </ModalComponent>

  <ModalComponent id="NewVaultForm">
    <NewVaultForm />
  </ModalComponent>
</template>

<script>
import Login from './Login.vue'
import { AppState } from "../AppState.js";
import { computed } from 'vue';

export default {
  setup() {
    return {
      user: computed(() => AppState.user)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
