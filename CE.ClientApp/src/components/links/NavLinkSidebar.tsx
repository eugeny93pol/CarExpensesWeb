import React from 'react'
import { NavLink } from 'react-router-dom'


export const NavLinkSidebar: React.FC<{to: string}> = ({to, children}) => {
  return (
    <NavLink to={to} className="sidebar-button" role="button" data-bs-dismiss="offcanvas">
      {children}
    </NavLink>
  )
}