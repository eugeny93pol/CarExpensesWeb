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

const loadCars = async (userId: string, token: string): Promise<{cars: CarType[]}> => {
  const response = await request(
    '/api/cars/',
    'GET',
    null,
    getAuthHeader(token)
  )
  const data = await response.json()
  return {cars: data}
}

export const carsService = {
  createCar
}