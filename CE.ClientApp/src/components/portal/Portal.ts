import React, { useEffect, useState } from 'react'
import ReactDOM from 'react-dom'

export const Portal: React.FC<{ children: any, className?: string, el?: string }> = ({
  children,
  className = 'root-portal',
  el = 'div'
}) => {
  const [container] = useState(document.createElement(el))
  const modalRoot = document.getElementById('root')

  className.split(' ').forEach(cl => {
    if (cl.length) {
      container.classList.add(cl)
    }
  })

  useEffect(() => {
    modalRoot!.appendChild(container)
    return () => {
      modalRoot!.removeChild(container)
    }
  }, [container, modalRoot])

  return ReactDOM.createPortal(children, container)
}