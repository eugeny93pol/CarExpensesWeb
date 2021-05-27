import { AppAction, AppActionsTypes } from '../types'


const setView = (view: string): AppAction => {
  return {type: AppActionsTypes.SET_VIEW, payload: view}
}

const showLoader = (): AppAction => {
  return {type: AppActionsTypes.SHOW_LOADER}
}

const hideLoader = (): AppAction => {
  return {type: AppActionsTypes.HIDE_LOADER}
}

const openModal = (name: string): AppAction => {
  return {type: AppActionsTypes.OPEN_MODAL, payload: name}
}

const closeModal = (): AppAction => {
  return {type: AppActionsTypes.CLOSE_MODAL}
}

export const appActions = {
  setView,
  showLoader,
  hideLoader,
  openModal,
  closeModal
}