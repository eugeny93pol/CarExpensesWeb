import React, { useEffect, useState } from 'react'
import { validateInput } from '../helpers'

export function useForm<T>(initialState: T) {
  interface IValidationResult {
    [key: string]: boolean
  }

  const [form, setForm] = useState<T>(initialState)
  const [validationResult, setValidationResult] = useState<IValidationResult>({})

  useEffect(() => {
    for (let [key, value] of Object.entries(initialState)) {
      setValidationResult(prev => (
        {...prev, [key]: validateInput(key, value)}
      ))
    }
  }, [])

  function cleanup (name: string, value: string): string {
    value = value.replaceAll(/\s+\s/g, ' ')
    if (name === 'password' || name === 'email' || name === 'vin'){
      value = value.replaceAll(' ', '')
    }
    return value
  }

  const changeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    let name = event.target.name
    let value = cleanup(name, event.target.value)
    if (validateInput(name, value)) {
      event.target.classList.remove('is-invalid')
      event.target.classList.add('is-valid')
      setValidationResult({...validationResult, [name]: true})
    } else {
      event.target.classList.remove('is-valid')
      event.target.classList.add('is-invalid')
      setValidationResult({...validationResult, [name]: false})
    }
    setForm(prev => ({...prev, [name]: value}))
  }
  return {form, validationResult, changeHandler}
}