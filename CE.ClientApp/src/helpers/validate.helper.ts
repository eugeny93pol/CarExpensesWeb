import config from '../config/default.json'

export const validateInput = (name: string, value: string) => {
  switch (name) {
    case 'name':
      return value.length >= config.nameLength
    case 'password':
      return value.length >= config.passwordLength
    case 'email':
      return value.lastIndexOf('@') > 0
        && value.lastIndexOf('@') < value.lastIndexOf('.')
        && value.lastIndexOf('.') !== value.length - 1
        && value.length > 5
    case 'brand':
      return value.length >= config.brandLength
    case 'vin':
      return value.length === config.vinLength || value.length === 0
    case 'year':
      let year = Number.parseInt(value)
      return year >= config.startCarYear && year <= new Date().getFullYear()
    default:
      return true
  }
}