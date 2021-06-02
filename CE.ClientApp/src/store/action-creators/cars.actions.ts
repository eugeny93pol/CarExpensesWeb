import { Dispatch } from 'react'
import { AlertAction, AppAction, CarsAction, CarsActionTypes, ICarCreate } from '../types'
import { appActions } from './app.actions'
import { carsService } from '../../services/cars.service'
import { errorHandler } from '../../helpers'
import { CarType } from '../../types/CarType'


const createCar = (userId: string, token: string, form: ICarCreate) => {
  return async (dispatch: Dispatch<CarsAction | AppAction | AlertAction>) => {
    try {
      dispatch(appActions.showLoader())
      dispatch({type: CarsActionTypes.CREATE_CAR_REQUEST})
      const {car} = await carsService.createCar(userId, token, form)
      dispatch({type: CarsActionTypes.CREATE_CAR_SUCCESS, payload: car})
    } catch (e) {
      dispatch({type: CarsActionTypes.CREATE_CAR_FAILURE, payload: form})
      errorHandler(dispatch, e)
    } finally {
      dispatch(appActions.hideLoader())
    }
  }
}

const setCars = (cars: CarType[]): CarsAction => ({
  type: CarsActionTypes.SET_CARS, payload: cars
})

const setDefaultCar = (carId: string): CarsAction => ({
  type: CarsActionTypes.SET_DEFAULT_CAR, payload: carId
})

export const carsActions = {
  createCar,
  setCars,
  setDefaultCar
}