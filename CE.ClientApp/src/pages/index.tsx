import React from 'react'
import { MainNavigationButtons } from '../components/button-groups/MainNavigationButtons'
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
      <div className="pt-2">
        {view === 'overview' && <ViewOverview/>}
        {view === 'records' && <ViewRecords/>}
      </div>
    </>
  )
}