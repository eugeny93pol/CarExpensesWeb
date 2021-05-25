import React from 'react'
import { useActions, useTypedSelector } from '../../../hooks'
import { useTranslation } from 'react-i18next'


export const ThemeSelector: React.FC = () => {
  const {setTheme} = useActions()
  const {theme} = useTypedSelector(store => store.settings)
  const {t} = useTranslation()

  const changeHandler = async (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      await setTheme('dark')
    } else {
      await setTheme('light')
    }
  }

  return (
    <div className="sidebar-button form-switch focus-within">
      <label className="form-check-label flex-grow-1" htmlFor="themeSelector">
        {t('sidebar.settings.theme')}
      </label>
      <input
        checked={theme === 'dark'}
        onChange={changeHandler}
        className="form-check-input"
        type="checkbox"
        id="themeSelector"/>
    </div>
  )
}