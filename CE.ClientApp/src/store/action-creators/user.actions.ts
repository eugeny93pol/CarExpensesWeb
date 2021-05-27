import { Dispatch } from 'react'
import { AlertAction, AppAction, SettingsAction, SettingsActionTypes } from '../types'
import { alertActions } from './alert.actions'
import { appActions } from './app.actions'
import { settingsService, userService } from '../../services'
import { store } from '../index'
import { HttpError } from '../../exceptions'


const loadUserData = (id: string, token: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction | AppAction>) => {
    const initialSettings = store.getState().settings
    try {
      dispatch(appActions.showLoader())
      const {settings} = await userService.getUserData(id, token)
      settingsService.combineSettings(initialSettings, settings)
      settingsService.saveSettingsToLocalStorage(initialSettings)
      await settingsService.applyUserSettings()
      dispatch({type: SettingsActionTypes.SET_SERVER_SETTINGS, payload: initialSettings})
    } catch (e) {
      if (e instanceof HttpError) {
        dispatch(alertActions.setErrorAlert(e.message, e.time))
      } else {
        dispatch(alertActions.setErrorAlert(e.message))
      }
    }
    finally {
      dispatch(appActions.hideLoader())
    }
  }
}

export const userActions = {
  loadUserData
}