import React, { ButtonHTMLAttributes, FC } from 'react'
import { useTypedSelector } from '../../hooks'

export const ButtonPrimary: FC<ButtonHTMLAttributes<HTMLButtonElement>> = (props) => {
  const {theme} = useTypedSelector(state => state.settings)

  let themeClass = theme === 'dark' ? 'btn-dark' : 'btn-primary'

  return (
    <button {...props} className={`btn ${themeClass} ${props.className}`}/>
  )
}