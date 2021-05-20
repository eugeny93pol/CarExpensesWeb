import React, { useEffect, useRef } from 'react'
import { Tooltip } from 'bootstrap'


export const ButtonTooltip: React.FC<{
  className: string,
  children: any,
  disabled?: boolean,
  placement?: string,
  tooltip: string,
  type?: 'button' | 'submit' | 'reset',
  onClick?: React.MouseEventHandler<HTMLButtonElement>
}> = ({
        className,
        children,
        disabled = false,
        placement,
        tooltip,
        type,
        onClick = () => {}
      }) => {
  const btnRef = useRef<HTMLButtonElement>(null)

  useEffect(() => {
    if (btnRef.current) {
      new Tooltip(btnRef.current)
    }
  }, [btnRef])
  return (
    <button ref={btnRef}
            data-bs-toggle="tooltip"
            className={className}
            data-bs-placement={placement}
            title={tooltip}
            type={type}
            onClick={onClick}
            disabled={disabled}>
      {children}
    </button>
  )
}