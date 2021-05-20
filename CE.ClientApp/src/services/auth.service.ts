import jwtDecode from 'jwt-decode'
import { AccessTokenType, UserType } from '../types'
import { request } from '../helpers'


const decodeToken = (token: string): UserType => {
  const decoded = jwtDecode<AccessTokenType>(token)
  return {id: decoded.id, name: decoded.name, email: decoded.email, role: decoded.role}
}

const login = async (email: string, password: string): Promise<{user: UserType, token: string}> => {
  const response = await request('/api/auth/login', 'POST', {email, password})
  const data = await response.json()
  const token = data.access_token.toString()
  const user = decodeToken(token)

  localStorage.setItem('access_token', data.access_token)
  return { user, token }
}

const registration = async (name: string, email: string, password: string): Promise<{user: UserType, token: string}> => {
  await request('/api/users/', 'POST', {name, email, password})
  return await login(email, password)
}


const logout = (): void => {
  localStorage.clear()
}


export const authService = {
  login,
  logout,
  registration,
  decodeToken
}