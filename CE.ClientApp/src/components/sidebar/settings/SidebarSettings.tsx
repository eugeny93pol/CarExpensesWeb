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
        <h2 className="accordion-header" id="flush-headingOne">
          <button className="sidebar-button collapsed" type="button" data-bs-toggle="collapse"
                  data-bs-target="#flush-collapseOne" aria-expanded="false" aria-controls="flush-collapseOne">
            {t('sidebar.settings')}<i className="bi bi-gear"/>
          </button>
        </h2>
        <div id="flush-collapseOne" className="accordion-collapse collapse" aria-labelledby="flush-headingOne">
          <ThemeSelector/>
          <LanguageSelector/>
          <MeasurementSystemSelector/>
        </div>
      </div>
    </div>
  )
}