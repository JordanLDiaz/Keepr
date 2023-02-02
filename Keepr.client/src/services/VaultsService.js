import { AppState } from "../AppState.js";
import { router } from "../router.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

export class VaultsService {

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    logger.log('[CREATING VAULT]', res.data)
    // AppState.activeVault = res.data
    AppState.vaults.push(res.data)
    return res.data
  }

  async getVault(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('[GETTING VAULT BY ID]', res.data)
    AppState.activeVault = res.data
  }

  async removeVault(vaultId) {
    const res = await api.delete('api/vaults/' + vaultId)
    logger.log('[REMOVING VAULT]', res.data)
    AppState.myVaults = AppState.myVaults.filter(v => v.id != vaultId)
    router.push({ name: 'Home' })
  }
}

export const vaultsService = new VaultsService();