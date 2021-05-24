export enum AppActionsTypes {
  SET_VIEW = 'APP/SET_VIEW',
  SHOW_LOADER = 'APP/SHOW_LOADER',
  HIDE_LOADER = 'APP/HIDE_LOADER',
  OPEN_MODAL = 'APP/OPEN_MODAL',
  CLOSE_MODAL = 'APP/CLOSE_MODAL'
}

export interface IAppState {
  view: string,
  modal: string | null
  isLoading: boolean
}

export type AppAction = IActionSetView
  | IActionShowLoader
  | IActionHideLoader
  | IActionOpenModal
  | IActionCloseModal


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

interface IActionOpenModal {
  type: AppActionsTypes.OPEN_MODAL
  payload: string
}

interface IActionCloseModal {
  type: AppActionsTypes.CLOSE_MODAL
}
