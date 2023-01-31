<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <button class="rounded-pill m-2 p-2" title="Home">Home</button>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDarkDropdown"
      aria-controls="navbarNavDarkDropdown" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNavDarkDropdown">
      <ul class="navbar-nav">
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"
            title="Create New Keep or Vault">
            Create
          </a>
          <ul class="dropdown-menu dropdown-menu-dark">
            <div class="selectable" data-bs-toggle="modal" data-bs-target="#NewKeepForm">
              <li><a class="dropdown-item" href="#">Create Keep</a></li>
            </div>

            <!-- TODO add createVault to below -->
            <div class="selectable" data-bs-toggle="modal" data-bs-target="#NewVaultForm">
              <li><a class="dropdown-item" href="#">Create Vault</a></li>
            </div>
          </ul>
        </li>
      </ul>
    </div>

    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <img alt="logo" src="../assets/img/keepr logo.png" height="45" title="Home" />
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
export default {
  setup() {
    return {}
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
