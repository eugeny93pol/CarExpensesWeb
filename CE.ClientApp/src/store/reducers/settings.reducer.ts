import { SettingsAction, SettingsActionTypes, ISettingsState } from '../types'

const initialState = {
  theme: 'light',
  language: 'en',
  measurementSystem: 'metric'
}

export const settingsReducer = (state = initialState, action: SettingsAction): ISettingsState => {
  switch (action.type) {
    case SettingsActionTypes.CHANGE_THEME:
      return { ...state, theme: action.payload }
    case SettingsActionTypes.CHANGE_LANGUAGE:
      return { ...state, language: action.payload }
    case SettingsActionTypes.CHANGE_MEASUREMENT_SYSTEM:
      return { ...state, measurementSystem: action.payload }
    case SettingsActionTypes.INIT_SETTINGS:
      return { ...action.payload }
    case SettingsActionTypes.SET_SERVER_SETTINGS:
      return { ...action.payload }
    default:
      return state
  }
}