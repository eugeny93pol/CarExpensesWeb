import { Dispatch } from 'react'
import { AlertAction } from '../store/types'
import { HttpError } from '../exceptions'
import { alertActions } from '../store/action-creators/alert.actions'

export function errorHandler(dispatch: Dispatch<AlertAction>, error: any, message?: string) {
  if (error instanceof HttpError) {
    dispatch(alertActions.setErrorAlert(message ? message : error.message, error.time))
  } else {
    dispatch(alertActions.setErrorAlert(error.message))
  }
}