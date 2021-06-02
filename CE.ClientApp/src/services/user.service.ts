import { getAuthHeader, request } from '../helpers'
import { ISettingsState } from '../store/types'
import { CarType } from '../types/CarType'


const getUserData = async (id: string, token: string): Promise<{ settings: ISettingsState, cars: CarType[] }> => {
  const response = await request(
    `/api/users/${id}?include=settings&include=cars`,
    'GET',
    null,
    getAuthHeader(token))
  const data = await response.json()

  return {settings: data.settings, cars: data.cars}
}

export const userService = {
  getUserData
}