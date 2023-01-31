import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "../AppState.js";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log('[GETTING ALL KEEPS]', res.data)
    AppState.keeps = res.data
  }

  async getOne(keepId) {
    const res = await api.get('api/keeps/' + keepId)
    logger.log('[GETTING ONE KEEP]', res.data)
    AppState.activeKeep = res.data
  }

  async createKeep(formData) {
    const res = await api.post('api/keeps', formData)
    logger.log('[CREATING KEEP]', res.data)
    AppState.activeKeep = res.data
    AppState.keeps.push(res.data)
    return res.data
  }
}

export const keepsService = new KeepsService();