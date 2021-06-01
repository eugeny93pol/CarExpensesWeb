import { getAuthHeader, request } from '../helpers'
import { ICarCreate } from '../store/types'
import { CarType } from '../types/CarType'


const createCar = async (userId: string, token: string, car: ICarCreate): Promise<{ car: CarType }> => {
  const response = await request(
    `/api/cars/`,
    'POST',
    {userId, ...car},
    getAuthHeader(token))
  const data = await response.json()

  return {car: data}
}

export const carsService = {
  createCar
}