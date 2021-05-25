import React, { useEffect, useRef } from 'react'
import { Tooltip } from 'bootstrap'

interface IButtonTooltip extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  placement?: 'auto' | 'top' | 'bottom' | 'left' | 'right',
  tooltip: string,
}

export const ButtonTooltip: React.FC<IButtonTooltip> = (props) => {
  const btnRef = useRef<HTMLButtonElement>(null)

  useEffect(() => {
    if (btnRef.current) {
      new Tooltip(btnRef.current)
    }
  }, [btnRef])
  return (
    <button {...props} ref={btnRef} data-bs-toggle="tooltip"
            data-bs-placement={props.placement}
            title={props.tooltip}/>
  )
}