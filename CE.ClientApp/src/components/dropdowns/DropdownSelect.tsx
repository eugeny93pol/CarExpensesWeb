import React, { useEffect, useState } from 'react'
import { useTypedSelector } from '../../hooks'

export interface IOptionSelect {
  key: number,
  value: string,
}

interface IDropdownSelect {
  id: string
  placeholder: string
  options?: IOptionSelect[],
  onSelect?: (option: IOptionSelect | null) => void
  searchable?: boolean
  disabled?: boolean
}

export const DropdownSelect: React.FC<IDropdownSelect> = (props) => {
  const {
    id, placeholder,
    options = [],
    searchable = false,
    disabled = false,
    onSelect = () => {}
  } = props
  const [input, setInput] = useState<string>('')
  const [selected, setSelected] = useState<IOptionSelect | null>(null)
  const [show, setShow] = useState<IOptionSelect[]>([])
  const {theme} = useTypedSelector(store => store.settings)

  const clickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    let option = options.find(op => op.key.toString() === event.currentTarget.value)
    if (option) {
      setSelected(option)
      setInput(option.value)
      setShow(options)
    }
  }

  const changeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    let value = event.currentTarget.value
    setInput(value)
    setSelected(null)
    setShow(options.filter(op => op.value.toLowerCase().includes(value.toLowerCase())))
  }

  useEffect(() => {
    onSelect(selected)
  }, [selected])

  useEffect(() => {
    setShow(options)
  }, [options])

  return (
    <div className={`dropdown select ${disabled ? 'disabled' : ''}`} aria-disabled={disabled}>
      <div className="btn btn-select" data-bs-toggle={!disabled && 'dropdown'} aria-expanded="false">
        <input className={`input-select ${theme === 'dark' ? '' : ''}`}
               id={id} placeholder={placeholder}
               value={input}
               onChange={changeHandler}
               disabled={!searchable || disabled}
        />
      </div>

      <ul className={`dropdown-menu dropdown-menu-${theme}`}
          aria-labelledby={id}>
        {show.map(op =>
          <li key={op.key}>
            <button className="dropdown-item" onClick={clickHandler} value={op.key}>
              {op.value}
            </button>
          </li>
        )}
        {!show.length && <li className="dropdown-header">No options</li>}
      </ul>
    </div>
  )
}