import React from 'react'
import { useActions } from '../../hooks'

interface IAddActionButton {
  color: string,
  icon: string,
  action: string
}

export const AddActionButton: React.FC<IAddActionButton> = (props) => {
  const {color, icon, action} = props
  const {openModal} = useActions()

  const clickHandler = () => {
    openModal(action)
  }

  return (
    <button type="button"
            className={`btn-add-action btn-add-action-${color}`}
            onClick={clickHandler}>
      <i className={`bi bi-${icon}`}/>
    </button>
  )
}