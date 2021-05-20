import React, { useEffect } from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { Index } from '../pages'
import { Navbar } from '../components/navbar/Navbar'
import { Sidebar } from '../components/sidebar/Sidebar'
import { useActions, useTypedSelector } from '../hooks'
import { Profile } from '../pages/profile'


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
      <div className="container">
        <Switch>
          <Route path="/home" exact component={Index}/>
          <Route path="/profile" exact component={Profile}/>
          <Redirect to="/home"/>
        </Switch>
      </div>
    </>
  )
}