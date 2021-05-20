import React from 'react'
import { Link } from 'react-router-dom'
import { useTypedSelector } from '../../hooks'


export const LinkSecondary: React.FC<{
  children: any,
  to: string,
  className?: string
}> = ({
  children,
  to,
  className= ''
}) => {
  const {theme} = useTypedSelector(state => state.settings)
  const baseClass = theme === 'dark' ? 'link-light' : 'link-secondary'

  return (
    <Link to={to} className={`${baseClass} ${className}`}>{children}</Link>
  )
}