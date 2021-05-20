import { AlertActionTypes } from '../types'


const setSuccessAlert = (message: string, time?: number) => {
  return {type: AlertActionTypes.SUCCESS, payload: {message, time}}
}

const setErrorAlert = (message: string, time?: number) => {
  return {type: AlertActionTypes.ERROR, payload: {message, time}}
}

const clearAlert = () => {
  return {type: AlertActionTypes.CLEAR}
}

export const alertActions = {
  setSuccessAlert,
  setErrorAlert,
  clearAlert
}