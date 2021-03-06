import { Dispatch } from 'react'
import { authService } from '../../services'
import { AlertAction, AuthAction, AuthActionTypes, IAuthState } from '../types'
import { UserType } from '../../types'
import { errorHandler } from '../../helpers'


const request = (): AuthAction => { return {type: AuthActionTypes.LOGIN_REQUEST} }
const failure = (): AuthAction => { return {type: AuthActionTypes.LOGIN_FAILURE} }
const success = (user: UserType, token: string): AuthAction => {
  return {type: AuthActionTypes.LOGIN_SUCCESS, payload: {user, token}}
}

const login = (email: string, password: string) => {
  return async (dispatch: Dispatch<AuthAction | AlertAction>) => {
    dispatch(request())
    try {
      let {user, token} = await authService.login(email, password)
      dispatch(success(user, token))
    } catch (e) {
      dispatch(failure())
      errorHandler(dispatch, e)
    }
  }
}

const registration = (name: string, email: string, password: string) => {
  return async (dispatch: Dispatch<AuthAction | AlertAction>) => {
    dispatch(request())
    try {
      let data = await authService.registration(name, email, password)
      dispatch(success(data.user, data.token))
    } catch (e) {
      dispatch(failure())
      errorHandler(dispatch, e)
    }
  }
}

const initializeAuth = () => {
  return (dispatch: Dispatch<AuthAction>) => {
    let initialState: IAuthState = {
      isPending: false,
      loggedIn: false,
      user: null,
      token: null
    }
    let token = localStorage.getItem('access_token')
    if (token) {
      let user = authService.decodeToken(token)
      initialState.loggedIn = true
      initialState.user = user
      initialState.token = token
    }
    dispatch({type: AuthActionTypes.INIT, payload: initialState})
  }
}

const logout = (): AuthAction => {
  authService.logout()
  return {type: AuthActionTypes.LOGOUT}
}

export const authActions = {
  login,
  logout,
  registration,
  initializeAuth
}