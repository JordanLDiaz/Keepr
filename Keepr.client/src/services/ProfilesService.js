import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class ProfilesService {
  async getUserProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId)
    logger.log('[GETTING ACTIVE USER PROFILE]', res.data)
    AppState.activeProfile = res.data
  }

  async getUserKeeps(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    logger.log('[GETTING USERS KEEPS]', res.data)
    AppState.userKeeps = res.data
  }

  async getUserVaults(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/vaults')
    logger.log('[GETTING USERS VAULTS]', res.data)
    AppState.userVaults = res.data
  }
}

export const profilesService = new ProfilesService();