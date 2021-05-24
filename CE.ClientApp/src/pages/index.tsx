import React from 'react'
import { MainNavigationButtons } from '../components/navigation/MainNavigationButtons'
import { useTypedSelector } from '../hooks'
import { ViewOverview } from './views/ViewOverview'
import { ViewRecords } from './views/ViewRecords'


export const Index: React.FC = () => {
  const {view} = useTypedSelector(state => state.app)

  return (
    <>
      <div className="pt-2 d-sm-none">
        <MainNavigationButtons/>
      </div>

      <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalContainer">
        Launch static backdrop modal
      </button>

      <div className="pt-2">
        {
          view === 'overview' &&
          <ViewOverview/>
        }
        {
          view === 'records' &&
          <ViewRecords/>
        }
      </div>

    </>
  )
}