import React from 'react'
import { Portal } from '../portal/Portal'

export const AddActionButtons = () => {
  return (
    <Portal className="action-buttons-portal">
      <div className="container position-relative">
        <div className="action-buttons-wrapper">
          <div className="add-action-buttons collapse pb-2" id="add-action-buttons-collapse">
            <button className="btn-add-action btn-add-action-blue"><i className="bi bi-truck"/></button>
            <button className="btn-add-action btn-add-action-indigo"><i className="bi bi-wrench"/></button>
            <button className="btn-add-action btn-add-action-purple"><i className="bi bi-currency-dollar"/></button>
            <button className="btn-add-action btn-add-action-pink"><i className="bi bi-droplet"/></button>
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