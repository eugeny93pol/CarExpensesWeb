import { Dispatch } from 'react'
import { AlertAction, SettingsAction, SettingsActionTypes } from '../types'
import { alertActions } from './alert.actions'
import { settingsService, userService } from '../../services'
import { store } from '../index'
import { HttpError } from '../../exceptions'


const loadUserData = (id: string, token: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction>) => {
    const initialSettings = store.getState().settings
    try {
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
  }
}

export const userActions = {
  loadUserData
}