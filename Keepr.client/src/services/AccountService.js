import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from "../utils/Pop.js"
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults() {
    try {
      const res = await api.get('account/vaults')
      AppState.myVaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getMyKeeps(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      logger.log('[GETTING MY KEEPS]', res.data)
      AppState.myKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async editAccount(editData) {
    const res = await api.put('account', editData)
    logger.log('[UPDATING ACCOUNT]', res.data)
    AppState.account = res.data
    Pop.toast('Account updated', "success")
  }
}

export const accountService = new AccountService()
