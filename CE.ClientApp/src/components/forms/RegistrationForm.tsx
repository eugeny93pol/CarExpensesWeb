import React from 'react'
import { useActions, useForm, useTypedSelector } from '../../hooks'
import { ButtonPrimary } from '../buttons/ButtonPrimary'
import { LinkSecondary } from '../links/LinkSecondary'

export const RegistrationForm: React.FC = () => {
  const {form, changeHandler} = useForm()
  const {registration} = useActions()
  const {isPending} = useTypedSelector(store => store.auth)

  const submitHandler = async (event: React.FormEvent) => {
    event.preventDefault()
    await registration(form.name, form.email, form.password)
  }

  return (
    <form onSubmit={submitHandler}>
      <h3 className="mb-3">Registration</h3>
      <div className="mb-3">
        <label htmlFor="inputName" className="form-label">Name</label>
        <input
          id="inputName"
          type="text"
          name="name"
          value={form.name}
          onChange={changeHandler}
          className="form-control"
          placeholder="Enter your name"
        />
      </div>
      <div className="mb-3">
        <label htmlFor="inputEmail" className="form-label">Email address</label>
        <input
          id="inputEmail"
          type="email"
          name="email"
          value={form.email}
          onChange={changeHandler}
          className="form-control"
          placeholder="name@example.com"
        />
        <div className="invalid-feedback">Please input correct email</div>
      </div>
      <div className="mb-3">
        <label htmlFor="inputPassword" className="form-label">Password</label>
        <input
          id="inputPassword"
          type="password"
          name="password"
          value={form.password}
          onChange={changeHandler}
          className="form-control"
          placeholder="Your password"
        />
      </div>
      <div className="mb-3">
        <ButtonPrimary
          type="submit"
          className="w-100"
          disabled={!(form.isValid.name && form.isValid.email && form.isValid.password) || isPending}
        >Register
        </ButtonPrimary>
      </div>
      <div className="text-center">
        <LinkSecondary to="/login">Login</LinkSecondary>
      </div>
    </form>
  )
}