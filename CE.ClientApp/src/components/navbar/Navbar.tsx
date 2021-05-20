import React from 'react'
import { Dropdown } from './Dropdown'


export const Navbar: React.FC = () => {
  return (
    <nav className="navbar sticky-top navbar-dark bg-primary">

      <div className="container">

        <button className="navbar-toggler" type="button" data-bs-toggle="offcanvas"
                data-bs-target="#sidebar" aria-controls="sidebar">
          <span className="navbar-toggler-icon"/>
        </button>
        <Dropdown/>

      </div>

    </nav>
  )
}