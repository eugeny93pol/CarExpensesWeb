import React, { useEffect } from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { Index } from '../pages'
import { Navbar } from '../components/navbar/Navbar'
import { Sidebar } from '../components/sidebar/Sidebar'
import { useActions, useTypedSelector } from '../hooks'
import { Profile } from '../pages/profile'
import { AddActionButtons } from '../components/navigation/AddActionButtons'
import { Modal } from '../components/modal/Modal'
import { ModalContainer } from '../components/modal/ModalContainer'


export const PrivateRoutes: React.FC = () => {
  const {user, token} = useTypedSelector(state => state.auth)
  const {loadUserData} = useActions()

  useEffect(() => {
    if (user && token) {
      loadUserData(user.id, token)
    }
  }, [user, token])
  return (
    <>
      <Navbar/>
      <Sidebar/>
      <ModalContainer/>
      <div className="overflow-auto scroll-area">
        <div className="container">
          <AddActionButtons/>
          <Switch>
            <Route path="/home" component={Index}/>
            <Route path="/profile" exact component={Profile}/>
            <Redirect to="/home"/>
          </Switch>
        </div>
      </div>
    </>
  )
}