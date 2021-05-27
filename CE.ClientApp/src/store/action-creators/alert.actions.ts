import { AlertAction, AlertActionTypes } from '../types'


const setSuccessAlert = (message: string, time?: number): AlertAction => {
  return {type: AlertActionTypes.SUCCESS, payload: {message, time}}
}

const setErrorAlert = (message: string, time?: number): AlertAction => {
  return {type: AlertActionTypes.ERROR, payload: {message, time}}
}

const clearAlert = (): AlertAction => {
  return {type: AlertActionTypes.CLEAR}
}

export const alertActions = {
  setSuccessAlert,
  setErrorAlert,
  clearAlert
}