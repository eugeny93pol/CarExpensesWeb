import { combineReducers } from 'redux'
import { alertReducer } from './alert.reducer'
import { appReducer } from './app.reducer'
import { authReducer } from './auth.reducer'
import { settingsReducer } from './settings.reducer'
import { carsReducer } from './cars.reducer'

export const rootReducer = combineReducers({
  alert: alertReducer,
  app: appReducer,
  auth: authReducer,
  settings: settingsReducer,
  cars: carsReducer
})

export type RootState = ReturnType<typeof rootReducer>