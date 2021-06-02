import React, { useEffect, useState } from 'react'
import { useTypedSelector } from '../../hooks'
import { useTranslation } from 'react-i18next'

export interface IOptionSelect {
  key: string,
  value: string,
  selected?: boolean
}

interface IDropdownSelect {
  id: string
  placeholder: string
  options?: IOptionSelect[]
  onSelect?: (option: IOptionSelect) => void
  searchable?: boolean
  disabled?: boolean
  noOptionsText?: string
}

export const Select: React.FC<IDropdownSelect> = (props) => {
  const {
    id, placeholder,
    options = [],
    searchable = false,
    disabled = false,
    onSelect = () => {},
    noOptionsText = 'No options'
  } = props

  const [input, setInput] = useState<string>('')
  const [selected, setSelected] = useState<IOptionSelect | null>(null)
  const [show, setShow] = useState<IOptionSelect[]>([])
  const {theme} = useTypedSelector(store => store.settings)

  const {t} = useTranslation()

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
    if (selected) {
      onSelect(selected)
    }
  }, [selected])

  useEffect(() => {
    let defaultSelected = options.find(op => op.selected)
    if (defaultSelected) {
      setSelected(defaultSelected)
      setInput(defaultSelected.value)
    }
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
        {!show.length && <li className="dropdown-header">{t(noOptionsText)}</li>}
        {props.children}
      </ul>
    </div>
  )
}