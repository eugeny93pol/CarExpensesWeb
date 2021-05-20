import { AlertAction, AlertActionTypes, IAlertState } from '../types'

const initialState: IAlertState = {
  type: null,
  message: null,
  time: null
}

export const alertReducer = (state = initialState, action: AlertAction): IAlertState => {
  switch (action.type) {
    case AlertActionTypes.SUCCESS:
      return {type: 'alert-success', message: action.payload.message, time: action.payload.time}
    case AlertActionTypes.ERROR:
      return {type: 'alert-error', message: action.payload.message, time: action.payload.time}
    case AlertActionTypes.CLEAR:
      return {type: null, message: null, time: null}
    default:
      return state
  }
}