import React from 'react'
import { useActions } from '../../hooks'
import { useTranslation } from 'react-i18next'

export const ButtonSidebarLogout: React.FC = () => {
  const {logout} = useActions()
  const {t} = useTranslation()

  return (
    <button type="button" className="sidebar-button" onClick={logout}>
      {t('sidebar.logout')}
      <i className="bi bi-door-open"/>
    </button>
  )
}