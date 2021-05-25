import { AppActionsTypes } from '../types'


const setView = (view: string) => {
  return {type: AppActionsTypes.SET_VIEW, payload: view}
}

const showLoader = () => {
  return {type: AppActionsTypes.SHOW_LOADER}
}

const hideLoader = () => {
  return {type: AppActionsTypes.HIDE_LOADER}
}

const openModal = (name: string) => {
  return {type: AppActionsTypes.OPEN_MODAL, payload: name}
}

const closeModal = () => {
  return {type: AppActionsTypes.CLOSE_MODAL}
}

export const appActions = {
  setView,
  showLoader,
  hideLoader,
  openModal,
  closeModal
}