import React from 'react'
import { Portal } from './portal/Portal'


export const Loader: React.FC = () => {
  return (
    <Portal className="loader-wrapper">
      <div className="loader">
        <div className="indeterminate"/>
      </div>
    </Portal>
  )
}