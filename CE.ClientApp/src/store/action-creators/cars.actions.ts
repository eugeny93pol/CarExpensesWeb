import { Dispatch } from 'react'
import { AlertAction, AppAction, CarsAction, CarsActionTypes, ICarCreate } from '../types'
import { appActions } from './app.actions'
import { carsService } from '../../services/cars.service'
import { errorHandler } from '../../helpers'


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

export const carsActions = {
  createCar
}