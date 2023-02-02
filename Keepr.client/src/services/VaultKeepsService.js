import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {
  async getVaultKeeps(vaultId) {
    const res = await api.get('api/vaults/' + vaultId + '/keeps')
    logger.log('[GETTING VAULTKEEPS]', res.data)
    AppState.vaultKeeps = res.data
  }

  async addVaultKeep(vaultId, keepId) {
    let vaultKeep = { vaultId, keepId }
    const res = await api.post('api/vaultkeeps', vaultKeep)
    logger.log('[CREATING NEW VAULTKEEP]', res.data)
    AppState.activeKeep.kept++
    AppState.vaultKeeps.push(res.data)
    return res.data
  }
}

export const vaultKeepsService = new VaultKeepsService();