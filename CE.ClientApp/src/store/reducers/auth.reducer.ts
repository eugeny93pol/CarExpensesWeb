import { AuthAction, AuthActionTypes, IAuthState } from '../types'

const initialState: IAuthState = {
  isPending: false,
  loggedIn: false,
  user: null,
  token: null
}

export const authReducer = (state = initialState, action: AuthAction): IAuthState => {
  switch (action.type) {
    case AuthActionTypes.LOGIN_REQUEST:
      return {isPending: true, loggedIn: false, user: null, token: null}
    case AuthActionTypes.LOGIN_SUCCESS:
      return {isPending: false, loggedIn: true, user: action.payload.user, token: action.payload.token}
    case AuthActionTypes.LOGIN_FAILURE:
      return {isPending: false, loggedIn: false, user: null, token: null}
    case AuthActionTypes.LOGOUT:
      return {isPending: false, loggedIn: false, user: null, token: null}
    case AuthActionTypes.INIT:
      return {...action.payload}
    default:
      return state
  }
}