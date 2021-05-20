import React, { useState } from 'react'
import { validateInput } from '../helpers'


export const useForm = () => {

  const [form, setForm] = useState({
    name: '',
    email: '',
    password: '',
    isValid: {
      name: false,
      email: false,
      password: false
    }
  })

  const changeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    let name = event.target.name
    let value = event.target.value.replaceAll(' ', '')
    if (validateInput(name, value)) {
      event.target.classList.remove('is-invalid')
      event.target.classList.add('is-valid')
      setForm({...form, [name]: value, isValid: {...form.isValid, [name]: true}})
    } else {
      event.target.classList.add('is-invalid')
      setForm({...form, [name]: value, isValid: {...form.isValid, [name]: false}})
    }
    setForm(prev => ({...prev, [name]: value}))
  }

  return { form, changeHandler }
}