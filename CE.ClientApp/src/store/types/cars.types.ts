import { CarType } from '../../types/CarType'

export enum CarsActionTypes {
  CREATE_CAR_REQUEST = 'CARS/CREATE_CAR_REQUEST',
  CREATE_CAR_SUCCESS = 'CARS/CREATE_CAR_SUCCESS',
  CREATE_CAR_FAILURE = 'CARS/CREATE_CAR_FAILURE'
}

export interface ICarsState {
  cars: CarType[],
  temp: ICarCreate | null,
  isPending: boolean
}

export interface ICarCreate {
  brand: string,
  model: string,
  year: string,
  vin: string
}

export type CarsAction = IActionCreateCar | IActionCreateCarSuccess | IActionCreateCarFailure

interface IActionCreateCar {
  type: CarsActionTypes.CREATE_CAR_REQUEST
}

interface IActionCreateCarSuccess {
  type: CarsActionTypes.CREATE_CAR_SUCCESS
  payload: CarType
}

interface IActionCreateCarFailure {
  type: CarsActionTypes.CREATE_CAR_FAILURE
  payload: ICarCreate
}