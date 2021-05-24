import React from 'react'
import { useActions, useTypedSelector } from '../../hooks'


export const MainNavigationButtons: React.FC = () => {
  const {view} = useTypedSelector(state => state.app)
  const {setView} = useActions()

  const onClickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    setView(event.currentTarget.value)
  }

  return (
      <div className="d-flex justify-content-center gap-2">
        <button className={`btn-tab${view === 'overview' ? ' active' : ''}`}
                onClick={onClickHandler} value="overview">
          <i className="bi bi-speedometer2"/>
          <span className="btn-tab-label ms-2">Overview</span>
        </button>
        <button className={`btn-tab${view === 'records' ? ' active' : ''}`}
                onClick={onClickHandler} value="records">
          <i className="bi bi-card-list"/>
          <span className="btn-tab-label ms-2">Records</span>
        </button>
      </div>
  )
}