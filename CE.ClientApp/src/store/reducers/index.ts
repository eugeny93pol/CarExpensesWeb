import { combineReducers } from 'redux'
import { alertReducer } from './alert.reducer'
import { appReducer } from './app.reducer'
import { authReducer } from './auth.reducer'
import { settingsReducer } from './settings.reducer'

export const rootReducer = combineReducers({
  alert: alertReducer,
  app: appReducer,
  auth: authReducer,
  settings: settingsReducer
})

export type RootState = ReturnType<typeof rootReducer>