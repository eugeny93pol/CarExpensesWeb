import config from '../config/default.json'

export const validateInput = (name: string, value: string) => {
  if (name === 'name') {
    return value.length >= config.nameLength
  }

  if (name === 'password') {
    return value.length >= config.passwordLength
  }

  if (name === 'email') {
    return value.lastIndexOf('@') > 0
      && value.lastIndexOf('@') < value.lastIndexOf('.')
      && value.lastIndexOf('.') !== value.length - 1
      && value.length > 5
  }

  return true
}