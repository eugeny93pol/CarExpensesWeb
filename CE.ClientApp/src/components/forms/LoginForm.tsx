import React from 'react'
import { useActions, useForm, useTypedSelector } from '../../hooks'
import { ButtonPrimary } from '../buttons/ButtonPrimary'
import { LinkSecondary } from '../links/LinkSecondary'

export const LoginForm: React.FC = () => {
  const {form, validationResult, changeHandler} = useForm(
    {email: '', password: ''}
  )
  const {login} = useActions()
  const {isPending} = useTypedSelector(store => store.auth)

  const submitHandler = async (event: React.FormEvent) => {
    event.preventDefault()
    await login(form.email, form.password)
  }

  return (
    <form onSubmit={submitHandler}>
      <h3 className="mb-3">Login</h3>
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
          disabled={!(validationResult.email && validationResult.password) || isPending}
        >Login</ButtonPrimary>

      </div>
      <div className="text-center">
        <LinkSecondary to="/registration">Create account</LinkSecondary>
      </div>
    </form>
  )
}