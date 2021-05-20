import { alertActions } from './alert.actions'
import { authActions } from './auth.actions'
import { settingsActions } from './settings.actions'
import { userActions } from './user.actions'
import { appActions } from './app.actions'

export const ActionCreators = {
  ...alertActions,
  ...authActions,
  ...settingsActions,
  ...userActions,
  ...appActions
}