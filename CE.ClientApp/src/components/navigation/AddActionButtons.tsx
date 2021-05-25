import React from 'react'
import { Portal } from '../portal/Portal'
import { AddActionButton } from '../buttons/AddActionButton'

export const AddActionButtons = () => {
  return (
    <Portal className="action-buttons-portal">
      <div className="container position-relative">
        <div className="action-buttons-wrapper">
          <div className="add-action-buttons collapse pb-2" id="add-action-buttons-collapse">
            <AddActionButton color={'blue'} icon={'truck'} action={'distance'}/>
            <AddActionButton color={'indigo'} icon={'wrench'} action={'distance'}/>
            <AddActionButton color={'purple'} icon={'currency-dollar'} action={'distance'}/>
            <AddActionButton color={'pink'} icon={'droplet'} action={'distance'}/>
          </div>
          <button type="button"
                  className="btn-add-action btn-add-action-cyan collapsed show"
                  data-bs-toggle="collapse" data-bs-target="#add-action-buttons-collapse"
                  aria-expanded="false" aria-controls="add-action-buttons-collapse">
            <i className="bi bi-plus-lg"/>
          </button>
        </div>
      </div>
    </Portal>
  )
}