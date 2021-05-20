export enum AlertActionTypes {
  SUCCESS = 'ALERT/SUCCESS',
  ERROR = 'ALERT/ERROR',
  CLEAR = 'ALERT/CLEAR'
}

export interface IAlertState {
  type: null | string
  message: null | string
  time?: null | number
}

export type AlertAction = IActionShowAlert | IActionHideAlert


interface IActionShowAlert {
  type: AlertActionTypes.SUCCESS | AlertActionTypes.ERROR
  payload: {
    message: string,
    time?: number
  }
}

interface IActionHideAlert {
  type: AlertActionTypes.CLEAR
}