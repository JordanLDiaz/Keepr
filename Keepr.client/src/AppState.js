import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  activeProfile: null,

  keeps: [],
  activeKeep: null,
  myKeeps: [],
  userKeeps: [],

  vaults: [],
  activeVault: null,
  myVaults: [],
  vault: {},
  userVaults: [],

  vaultKeeps: [],
  vauktKeep: {}
})
