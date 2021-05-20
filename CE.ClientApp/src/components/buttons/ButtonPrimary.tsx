import React from 'react'
import { useTypedSelector } from '../../hooks'


export const ButtonPrimary: React.FC<{
  children?: any,
  type?: 'button' | 'submit' | 'reset',
  className?: string,
  disabled?: boolean,
  onClick?: React.MouseEventHandler<HTMLButtonElement>
}> = ({
  children = '',
  type,
  className= '',
  disabled = false,
  onClick = () => {}
}) => {
  const {theme} = useTypedSelector(state => state.settings)

  let baseClass = theme === 'dark' ? 'btn-dark' : 'btn-primary'

  return (
    <button type={type}
            className={`btn ${baseClass} ${className}`}
            disabled={disabled}
            onClick={onClick}
    >{children}</button>
  )
}