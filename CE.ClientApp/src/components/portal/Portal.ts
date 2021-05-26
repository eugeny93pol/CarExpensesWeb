import React, { useEffect, useState } from 'react'
import ReactDOM from 'react-dom'

interface IPortal {
  children?: React.ReactNode,
  className?: string,
  el?: string,
  id?: string,
  ref?: React.RefObject<HTMLElement>
}

export const Portal: React.FC<IPortal> = ({
  children,
  className = 'root-portal',
  el = 'div',
  id,
  ref
}) => {
  const [container] = useState(document.createElement('div'))

  const modalRoot = document.getElementById('root')

  className.split(' ').forEach(cl => {
    if (cl.length) {
      container.classList.add(cl)
    }
  })

  if (id) {
    container.id = id
  }

  useEffect(() => {
    modalRoot?.appendChild(container)
    return () => {
      modalRoot?.removeChild(container)
    }
  }, [container, modalRoot])

  return ReactDOM.createPortal(children, container)
}