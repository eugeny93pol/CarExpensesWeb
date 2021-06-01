import { Dispatch } from 'react'
import { AlertAction, AppAction, ISettingsState, SettingsAction, SettingsActionTypes } from '../types'
import { store } from '../index'
import { settingsService } from '../../services'
import { appActions } from './app.actions'
import { errorHandler } from '../../helpers'


const setTheme = (theme: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction | AppAction>) => {
    dispatch({type: SettingsActionTypes.CHANGE_THEME, payload: theme})
    await settingsService.applyUserSettings()
    await saveSettings(dispatch)
  }
}

const setLanguage = (language: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction | AppAction>) => {
    dispatch({type: SettingsActionTypes.CHANGE_LANGUAGE, payload: language})
    await settingsService.applyUserSettings()
    await saveSettings(dispatch)
  }
}

const setMeasurementSystem = (system: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction | AppAction>) => {
    dispatch({type: SettingsActionTypes.CHANGE_MEASUREMENT_SYSTEM, payload: system})
    await saveSettings(dispatch)
  }
}

const initializeSettings = (initSettings: any = null) => {
  return async (dispatch: Dispatch<SettingsAction>) => {
    if (!initSettings) {
      initSettings = settingsService.loadSettingsFromLocalStorage()
    }
    await applySettings(dispatch, initSettings)
  }
}

async function applySettings(dispatch: Dispatch<SettingsAction>, initSettings: ISettingsState) {
  let settings = settingsService.combineSettings(store.getState().settings, initSettings)
  dispatch({type: SettingsActionTypes.INIT_SETTINGS, payload: settings})
  settingsService.saveSettingsToLocalStorage(settings)
  await settingsService.applyUserSettings()
}

async function saveSettings(dispatch: Dispatch<SettingsAction | AlertAction | AppAction>) {
  const {settings} = store.getState()
  settingsService.saveSettingsToLocalStorage(settings)
  try {
    dispatch(appActions.showLoader())
    await settingsService.saveSettingsToServer(settings)
  } catch (e) {
    errorHandler(dispatch, e, 'error.http.save')
  } finally {
    dispatch(appActions.hideLoader())
  }
}


export const settingsActions = {
  applySettings,
  setTheme,
  setLanguage,
  setMeasurementSystem,
  initializeSettings
}