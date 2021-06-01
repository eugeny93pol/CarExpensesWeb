import React from 'react'
import { NavbarDropdown } from '../dropdowns/NavbarDropdown'
import { Route } from 'react-router-dom'
import { MainNavigationButtons } from '../button-groups/MainNavigationButtons'
import { useTypedSelector } from '../../hooks'


export const Navbar: React.FC = () => {
  const {theme} = useTypedSelector(state => state.settings)
  return (
    <nav className={`navbar sticky-top navbar-dark bg-${theme === 'dark' ? theme : 'primary'}`}>

      <div className="container">

        <button className="navbar-toggler" type="button" data-bs-toggle="offcanvas"
                data-bs-target="#sidebar" aria-controls="sidebar">
          <span className="navbar-toggler-icon"/>
        </button>


          <Route path="/home" exact>
            <div className="d-none d-sm-block">
              <MainNavigationButtons/>
            </div>
          </Route>




        <NavbarDropdown/>

      </div>

    </nav>
  )
}