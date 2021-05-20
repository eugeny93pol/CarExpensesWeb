import React, { Suspense, useEffect } from 'react'
import { BrowserRouter as Router } from 'react-router-dom'
import { useActions, useTypedSelector } from './hooks'
import { PrivateRoutes } from './routes/PrivateRoutes'
import { PublicRoutes } from './routes/PublicRoutes'
import { Toast } from './components/toast/Toast'


export const App: React.FC = () => {
  const {loggedIn} = useTypedSelector(state => state.auth)
  const {initializeSettings, initializeAuth} = useActions()

  useEffect(() => {
    initializeAuth()
    initializeSettings()
  }, [])
  return (
    <Suspense fallback={<div>loading</div>}>
      <Toast/>
      <Router>
        {loggedIn && <PrivateRoutes/>}
        {!loggedIn && <PublicRoutes/>}
      </Router>
    </Suspense>
  )
}