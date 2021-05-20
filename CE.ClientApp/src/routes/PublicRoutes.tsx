import React from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { LoginForm } from '../components/forms/LoginForm'
import { RegistrationForm } from '../components/forms/RegistrationForm'


export const PublicRoutes: React.FC = () => {
  return (
    <div className="container">
      <div className="row align-items-center min-vh-100">
        <div className="col-9 col-sm-8 col-md-6 col-lg-5 col-xl-4 m-auto">
          <Switch>
            <Route path="/login" exact component={LoginForm}/>
            <Route path="/registration" exact component={RegistrationForm}/>
            <Redirect to="/login"/>
          </Switch>
        </div>
      </div>
    </div>
  )
}