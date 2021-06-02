import { Dispatch } from 'react'
import { AlertAction, AppAction, CarsAction, SettingsAction } from '../types'
import { appActions } from './app.actions'
import { userService } from '../../services'
import { errorHandler } from '../../helpers'
import { settingsActions } from './settings.actions'
import { carsActions } from './cars.actions'


const loadUserData = (id: string, token: string) => {
  return async (dispatch: Dispatch<SettingsAction | AlertAction | AppAction | CarsAction>) => {
    try {
      dispatch(appActions.showLoader())
      const {settings, cars} = await userService.getUserData(id, token)
      await settingsActions.applySettings(dispatch, settings)
      dispatch(carsActions.setCars(cars))
    } catch (e) {
      errorHandler(dispatch, e)
    }
    finally {
      dispatch(appActions.hideLoader())
    }
  }
}

export const userActions = {
  loadUserData
}