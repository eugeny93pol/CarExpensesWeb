import React from 'react'

interface IAddActionButton extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  color: string,
  icon: string,
}

export const AddActionButton: React.FC<IAddActionButton> = (props) => {
  const {color, icon} = props

  return (
    <button type="button" {...props}
            className={`btn-add-action btn-add-action-${color}`}>
      <i className={`bi bi-${icon}`}/>
    </button>
  )
}