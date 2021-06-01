import React, { useMemo } from 'react'
import { Portal } from '../portal/Portal'
import { AddActionButton } from '../buttons/AddActionButton'
import { useActions } from '../../hooks'
import { ModalTypes } from '../../types'

export const AddActionButtons = () => {
  const actionButtons = useMemo(() => {
    return [
      {color: 'blue', icon: 'truck', value: ModalTypes.ADD_MILEAGE},
      {color: 'indigo', icon: 'wrench', value: ModalTypes.ADD_REPAIR},
      {color: 'purple', icon: 'currency-dollar', value: ModalTypes.ADD_PURCHASES},
      {color: 'pink', icon: 'droplet', value: ModalTypes.ADD_REFILL}
    ]
  }, [])

  const {openModal} = useActions()

  const buttonClickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    openModal(event.currentTarget.value)
  }

  return (
    <Portal className="action-buttons-portal">
      <div className="container position-relative">
        <div className="action-buttons-wrapper">
          <div className="add-action-buttons collapse pb-2" id="add-action-buttons-collapse">
            {actionButtons.map(btn =>
              <AddActionButton
                key={btn.icon}
                color={btn.color}
                icon={btn.icon}
                value={btn.value}
                onClick={buttonClickHandler}/>
            )}
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