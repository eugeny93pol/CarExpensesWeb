import React from 'react'
import { ThemeSelector } from './ThemeSelector'
import { LanguageSelector } from './LanguageSelector'
import { MeasurementSystemSelector } from './MeasurementSystemSelector'
import { useTranslation } from 'react-i18next'


export const SidebarSettings: React.FC = () => {
  const {t} = useTranslation()

  return (
    <div className="accordion accordion-flush">
      <div className="accordion-item">
        <h3 className="accordion-header" id="sidebar-settings-header">
          <button className="sidebar-button collapsed" type="button"
                  data-bs-toggle="collapse" data-bs-target="#sidebar-settings-collapse"
                  aria-expanded="false" aria-controls="sidebar-settings-collapse">
            {t('sidebar.settings')}<i className="bi bi-gear"/>
          </button>
        </h3>
        <div id="sidebar-settings-collapse" className="accordion-collapse collapse"
             aria-labelledby="sidebar-settings-header">
          <ThemeSelector/>
          <LanguageSelector/>
          <MeasurementSystemSelector/>
        </div>
      </div>
    </div>
  )
}