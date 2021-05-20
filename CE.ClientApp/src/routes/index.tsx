import React from 'react'
import { useTypedSelector } from '../hooks/useTypedSelector'
import { PrivateRoutes } from './PrivateRoutes'
import { PublicRoutes } from './PublicRoutes'


export const Routes: React.FC = () => {
  const {loggedIn} = useTypedSelector(state => state.auth)
  return (
    <>
      {loggedIn && <PrivateRoutes/>}
      {!loggedIn && <PublicRoutes/>}
    </>
  )
}