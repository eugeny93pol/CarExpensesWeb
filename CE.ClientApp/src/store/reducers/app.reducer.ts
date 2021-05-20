import { AppAction, AppActionsTypes, IAppState } from '../types'

const initialState: IAppState = {
  view: 'main',
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
    default:
      return state
  }
}