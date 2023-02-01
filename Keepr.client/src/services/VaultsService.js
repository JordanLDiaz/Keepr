import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

export class VaultsService {

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    logger.log('[CREATING VAULT]', res.data)
    AppState.activeVault = res.data
  }

  async getVault(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('[GETTING VAULT BY ID]', res.data)
    AppState.activeVault = res.data
  }
}

export const vaultsService = new VaultsService();