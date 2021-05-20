export enum AppActionsTypes {
  SET_VIEW = 'APP/SET_VIEW',
  SHOW_LOADER = 'APP/SHOW_LOADER',
  HIDE_LOADER = 'APP/HIDE_LOADER',
}

export interface IAppState {
  view: string,
  isLoading: boolean
}

export type AppAction = IActionSetView | IActionShowLoader | IActionHideLoader


interface IActionSetView {
  type: AppActionsTypes.SET_VIEW
  payload: string
}

interface IActionShowLoader {
  type: AppActionsTypes.SHOW_LOADER
}

interface IActionHideLoader {
  type: AppActionsTypes.HIDE_LOADER
}
