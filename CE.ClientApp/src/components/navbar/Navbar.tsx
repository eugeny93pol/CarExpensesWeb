import React, { useEffect, useState } from 'react'
import { NavLink, Route } from 'react-router-dom'
import { MainNavigationButtons } from '../button-groups/MainNavigationButtons'
import { useActions, useTypedSelector } from '../../hooks'
import { ModalTypes } from '../../types'
import { CarType } from '../../types/CarType'


export const Navbar: React.FC = () => {
  const {theme} = useTypedSelector(state => state.settings)
  const {cars, defaultCarId} = useTypedSelector(state => state.cars)
  const {openModal} = useActions()
  const [defaultCar, setDefaultCar] = useState<CarType>()

  useEffect(() => {
    let car = cars.find(car => car.id === defaultCarId)
    if (car) {
      setDefaultCar(car)
    }
  }, [defaultCarId])

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

      <nav className="navbar-nav">
        { defaultCar &&
          <NavLink to={`/car/${defaultCar.id}`} className="nav-link">{
            defaultCar.brand + ' ' + defaultCar.model
          }</NavLink>
        }
        {cars.length === 0 &&
          <button className="btn nav-link" onClick={() => openModal(ModalTypes.ADD_CAR)}>
            <i className="bi bi-plus-circle"/> Add car
          </button>
        }
      </nav>


      </div>

    </nav>
  )
}