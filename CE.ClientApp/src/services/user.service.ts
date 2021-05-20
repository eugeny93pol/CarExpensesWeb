import { getAuthHeader, request } from '../helpers'
import { ISettingsState } from '../store/types'


const getUserData = async (id: string, token: string): Promise<{ settings: ISettingsState }> => {
  const response = await request(
    `/api/users/${id}?include=settings&include=cars`,
    'GET',
    null,
    getAuthHeader(token))
  const data = await response.json()

  return {settings: data.settings}
}

export const userService = {
  getUserData
}