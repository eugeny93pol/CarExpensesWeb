import React from 'react'
import { useActions, useTypedSelector } from '../../../hooks'
import measurementConfig from '../../../config/measurementSystems.json'
import { useTranslation } from 'react-i18next'


export const MeasurementSystemSelector: React.FC = () => {
  const {theme, measurementSystem, language} = useTypedSelector(store => store.settings)
  const {setMeasurementSystem} = useActions()
  const {t} = useTranslation()
  const systems = measurementConfig.systems

  const clickHandler = async (event: React.MouseEvent<HTMLButtonElement>) => {
    await setMeasurementSystem(event.currentTarget.value)
  }

  return (
    <div className="custom-selector">
      <button className="sidebar-button" type="button"
              id="measurementDropdown" data-bs-toggle="dropdown" aria-expanded="false">
        {t('sidebar.settings.measurement')}
        <i className="bi bi-rulers"/>
      </button>
      <ul className={`dropdown-menu dropdown-menu-end dropdown-menu-md-start mt-1 dropdown-menu-${theme}`}
          aria-labelledby="measurementDropdown">
        {systems.map(system => (
          <li key={system.value}>
            <button className="dropdown-item d-flex justify-content-between"
                    value={system.value} onClick={clickHandler}>
              {
                // @ts-ignore
                system.labels[language]
              }
              {measurementSystem === system.value && <i className="bi bi-check2"/>}
            </button>
          </li>
        ))}
      </ul>
    </div>
  )
}