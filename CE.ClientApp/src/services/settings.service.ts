import i18n from 'i18next'
import { store } from '../store'
import { request } from '../helpers'
import { ISettingsState } from '../store/types'


export const settingsService = {
  saveSettingsToServer,
  saveSettingsToLocalStorage,
  loadSettingsFromLocalStorage,
  combineSettings,
  applyUserSettings
}

function saveSettingsToLocalStorage(settings: ISettingsState) {
  localStorage.setItem('settings', JSON.stringify(settings))
}

function loadSettingsFromLocalStorage() {
  let local = localStorage.getItem("settings")
  return local ? JSON.parse(local) : null
}

function combineSettings(settings: ISettingsState, initSettings: any) {
  Object.keys(settings).forEach(key => {
    if(initSettings && initSettings[key]) {
      settings[key] = initSettings[key]
    }
  })
}

async function saveSettingsToServer(settings: ISettingsState) {
  const {auth} = store.getState()
  await request(`/api/userSettings/${auth.user?.id}`, 'PATCH', settings, {
    Authorization: 'Bearer ' + auth.token
  })
}

async function applyUserSettings() {
  const {settings} = store.getState()
  await i18n.changeLanguage(settings.language)
  document.getElementById('body')!.className = settings.theme
}