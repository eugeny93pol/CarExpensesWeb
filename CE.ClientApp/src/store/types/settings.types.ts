export enum SettingsActionTypes {
  CHANGE_THEME = 'SETTINGS/CHANGE_THEME',
  CHANGE_LANGUAGE = 'SETTINGS/CHANGE_LANGUAGE',
  CHANGE_MEASUREMENT_SYSTEM = 'SETTINGS/CHANGE_MEASUREMENT_SYSTEM',
  INIT_SETTINGS = 'SETTINGS/INIT'
}

export interface ISettingsState {
  theme: string,
  language: string,
  measurementSystem: string,
  [key: string]: any
}

export type SettingsAction = IActionChangeSettings | IActionLoadSettings


interface IActionChangeSettings {
  type: SettingsActionTypes.CHANGE_THEME
    | SettingsActionTypes.CHANGE_LANGUAGE
    | SettingsActionTypes.CHANGE_MEASUREMENT_SYSTEM
  payload: string
}

interface IActionLoadSettings {
  type: SettingsActionTypes.INIT_SETTINGS
  payload: ISettingsState
}