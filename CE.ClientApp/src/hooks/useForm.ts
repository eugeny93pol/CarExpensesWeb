import React, { useEffect, useState } from 'react'
import { validateInput } from '../helpers'


export const useForm = (fields: Array<'name' | 'email' | 'password' | 'brand' | 'model' | 'year' | 'vin'>) => {
  interface IForm {
    [key: string]: string
  }

  interface IValidationResult {
    [key: string]: boolean
  }

  let initialForm: IForm = {}
  let initialValidationResult: IValidationResult = {}

  const cleanup = (name: string, value: string): string => {
    value = value.replaceAll(/\s+\s/g, ' ')
    if (name === 'password' || name === 'email' || name === 'vin'){
      value = value.replaceAll(' ', '')
    }
    return value
  }

  useEffect(() => {
    fields.forEach(arg => {
      initialForm[arg] = ''
      initialValidationResult[arg] = false
    })
    setForm(initialForm)
    setValidationResult(initialValidationResult)
  }, [])

  const [form, setForm] = useState<IForm>(initialForm)
  const [validationResult, setValidationResult] = useState<IValidationResult>(initialValidationResult)

  const changeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    let name = event.target.name
    let value = cleanup(name, event.target.value)
    if (validateInput(name, value)) {
      event.target.classList.remove('is-invalid')
      event.target.classList.add('is-valid')
      setValidationResult({...validationResult, [name]: true})
    } else {
      event.target.classList.add('is-invalid')
      setValidationResult({...validationResult, [name]: false})
    }
    setForm(prev => ({...prev, [name]: value}))
  }
  return {form, validationResult, changeHandler}
}