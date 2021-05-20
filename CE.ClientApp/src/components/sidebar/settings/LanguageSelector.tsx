import React from 'react'
import { useActions, useTypedSelector } from '../../../hooks'
import languagesConfig from '../../../config/languages.json'
import { useTranslation } from 'react-i18next'


export const LanguageSelector: React.FC = () => {
  const {theme, language} = useTypedSelector(store => store.settings)
  const {setLanguage} = useActions()
  const {t} = useTranslation()
  const languages = languagesConfig.languages

  const clickHandler = async (event: React.MouseEvent<HTMLButtonElement>) => {
    await setLanguage(event.currentTarget.value)
  }

  return (
    <div className="custom-selector">
      <button className="sidebar-button" type="button"
           id="languageDropdown"
           data-bs-toggle="dropdown" aria-expanded="false">
        {t('sidebar.settings.language')}
        <i className="bi bi-translate"/>
      </button>
      <ul className={`dropdown-menu dropdown-menu-end dropdown-menu-md-start mt-1 dropdown-menu-${theme}`}
          aria-labelledby="languageDropdown">
        {languages.map(lng => (
          <li key={lng.value}>
            <button className="dropdown-item d-flex justify-content-between"
                    value={lng.value} onClick={clickHandler}>
              {lng.label}
              {language === lng.value && <i className="bi bi-check2"/>}
            </button>
          </li>
        ))}
      </ul>
    </div>
  )
}