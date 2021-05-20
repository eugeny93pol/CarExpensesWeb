import { Dispatch } from 'react'
import { AlertAction, SettingsAction, SettingsActionTypes } from '../types'
import { store } from '../index'
import { settingsService } from '../../services'
import { alertActions } from './alert.actions'
import { HttpError } from '../../exceptions'


const setTheme = (theme: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction>) => {
    dispatch({type: SettingsActionTypes.CHANGE_THEME, payload: theme})
    await settingsService.applyUserSettings()
    await saveSettings(dispatch)
  }
}

const setLanguage = (language: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction>) => {
    dispatch({type: SettingsActionTypes.CHANGE_LANGUAGE, payload: language})
    await settingsService.applyUserSettings()
    await saveSettings(dispatch)
  }
}

const setMeasurementSystem = (system: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction>) => {
    dispatch({type: SettingsActionTypes.CHANGE_MEASUREMENT_SYSTEM, payload: system})
    await saveSettings(dispatch)
  }
}

const initializeSettings = (initSettings: any = null) => {
  return async (dispatch: Dispatch<SettingsAction>) => {
    const {settings} = store.getState()
    if (!initSettings) {
      initSettings = settingsService.loadSettingsFromLocalStorage()
    }
    settingsService.combineSettings(settings, initSettings)
    dispatch({type: SettingsActionTypes.INIT_SETTINGS, payload: settings})
    settingsService.saveSettingsToLocalStorage(settings)
    await settingsService.applyUserSettings()
  }
}

async function saveSettings(dispatch: Dispatch<SettingsAction | AlertAction>) {
  const {settings} = store.getState()
  settingsService.saveSettingsToLocalStorage(settings)
  try {
    await settingsService.saveSettingsToServer(settings)
  } catch (e) {
    if (e instanceof HttpError) {
      dispatch(alertActions.setErrorAlert('error.http.save', e.time))
    } else {
      dispatch(alertActions.setErrorAlert(e.message))
    }
  }
}


export const settingsActions = {
  setTheme,
  setLanguage,
  setMeasurementSystem,
  initializeSettings
}