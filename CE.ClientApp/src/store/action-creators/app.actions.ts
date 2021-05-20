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

export const appActions = {
  setView,
  showLoader,
  hideLoader
}