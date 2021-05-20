import React from 'react'
import { useActions, useTypedSelector } from '../../hooks'


export const Dropdown: React.FC = () => {
  const {logout} = useActions()
  const {theme} = useTypedSelector(store => store.settings)

  return (
    <div className="dropdown">
      <button className={`btn dropdown-toggle ${theme === 'dark' ? 'btn-outline-secondary' : 'btn-outline-light'}`}
              id="navbarDropdown"
              data-bs-toggle="dropdown" aria-expanded="false">
        Dropdown
      </button>
      <ul className={`dropdown-menu dropdown-menu-end dropdown-menu-md-start dropdown-menu-${theme}`}
          aria-labelledby="navbarDropdown">
        <li><button className="dropdown-item">Profile</button></li>
        <li><button className="dropdown-item" onClick={logout}>Logout</button></li>
      </ul>
    </div>
  )
}