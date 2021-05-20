import { UserType } from '../../types'

export enum AuthActionTypes {
  LOGIN_REQUEST = 'AUTH/LOGIN_REQUEST',
  LOGIN_SUCCESS = 'AUTH/LOGIN_SUCCESS',
  LOGIN_FAILURE = 'AUTH/LOGIN_FAILURE',
  LOGOUT = 'AUTH/LOGOUT',
  INIT = 'AUTH/INIT',

  REFRESH_TOKEN_REQUEST = 'AUTH/REFRESH_TOKEN_REQUEST',
  REFRESH_TOKEN_SUCCESS = 'AUTH/REFRESH_TOKEN_SUCCESS',
  REFRESH_TOKEN_FAILURE = 'AUTH/REFRESH_TOKEN_FAILURE'
}

export interface IAuthState {
  isPending: boolean;
  loggedIn: boolean;
  user: UserType | null;
  token: string | null;
}

export type AuthAction = IActionLoginRequest |
  IActionLoginSuccess |
  IActionLoginFailure |
  IActionLogout |
  IActionRefreshTokenRequest |
  IActionRefreshTokenFailure |
  IActionRefreshTokenSuccess |
  IActionInitAuth;


interface IActionLoginRequest {
  type: AuthActionTypes.LOGIN_REQUEST
}

interface IActionLoginSuccess {
  type: AuthActionTypes.LOGIN_SUCCESS
  payload: {
    user: UserType,
    token: string
  };
}

interface IActionLoginFailure {
  type: AuthActionTypes.LOGIN_FAILURE;
}

interface IActionLogout {
  type: AuthActionTypes.LOGOUT;
}

interface IActionInitAuth {
  type: AuthActionTypes.INIT
  payload: IAuthState
}

interface IActionRefreshTokenRequest {
  type: AuthActionTypes.REFRESH_TOKEN_REQUEST
}

interface IActionRefreshTokenFailure {
  type: AuthActionTypes.REFRESH_TOKEN_FAILURE
}

interface IActionRefreshTokenSuccess {
  type: AuthActionTypes.REFRESH_TOKEN_SUCCESS
  payload: {
    user: UserType,
    token: string
  }
}