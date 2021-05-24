import { AppAction, AppActionsTypes, IAppState } from '../types'

const initialState: IAppState = {
  view: 'overview',
  modal: null,
  isLoading: false
}

export const appReducer = (state = initialState, action: AppAction): IAppState => {
  switch (action.type) {
    case AppActionsTypes.SET_VIEW:
      return {...state, view: action.payload}
    case AppActionsTypes.SHOW_LOADER:
      return {...state, isLoading: true}
    case AppActionsTypes.HIDE_LOADER:
      return {...state, isLoading: false}
    case AppActionsTypes.OPEN_MODAL:
      return {...state, modal: action.payload}
    case AppActionsTypes.CLOSE_MODAL:
      return {...state, modal: null}
    default:
      return state
  }
}