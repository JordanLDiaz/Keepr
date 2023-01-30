import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "../AppState.js";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log(['GETTING ALL KEEPS', res.data])
    AppState.keeps = res.data
  }
}

export const keepsService = new KeepsService();