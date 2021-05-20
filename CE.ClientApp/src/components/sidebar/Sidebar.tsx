import React from 'react'
import { useTranslation } from 'react-i18next'
import { useTypedSelector } from '../../hooks'
import { SidebarSettings } from './settings/SidebarSettings'
import { Portal } from '../portal/Portal'
import { NavLink } from 'react-router-dom'
import { ButtonSidebarLogout } from '../buttons/ButtonSidebarLogout'


export const Sidebar: React.FC = () => {
  const {theme} = useTypedSelector(state => state.settings)
  const {t} = useTranslation()
  const darkClass = theme === 'dark' ? 'btn-close-white' : ''

  return (
    <Portal>
      <div className="offcanvas offcanvas-start user-select-none" tabIndex={-1} id="sidebar"
           aria-labelledby="sidebarLabel">
        <div className="offcanvas-header">
          <h5 className="offcanvas-title" id="sidebarLabel">{t('sidebar.title')}</h5>
          <button type="button"
                  className={`btn-close text-reset ${darkClass}`}
                  data-bs-dismiss="offcanvas"
                  aria-label="Close"/>
        </div>
        <div className="offcanvas-body">
          <nav className="nav flex-column" id="btn-list-tab" role="tablist" aria-orientation="vertical">
            <NavLink to="/home" className="sidebar-button" role="button">{t('sidebar.home')}</NavLink>
            <NavLink to="/profile" className="sidebar-button">{t('sidebar.profile')}</NavLink>
          </nav>
          <SidebarSettings/>
          <ButtonSidebarLogout/>
        </div>
      </div>
    </Portal>
  )
}