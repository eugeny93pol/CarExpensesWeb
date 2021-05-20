import { HttpError } from '../exceptions'

export const request = async (url: string, method: string = 'GET', body: any = null, headers: any = {}) => {
  if (body) {
    body = JSON.stringify(body)
    headers['Content-type'] = 'application/json'
  }
  const response = await fetch(url, {method, body, headers})

  if (!response.ok) {
    throw new HttpError(response.status, response.statusText, Date.now())
  }
  return response
}

export const getAuthHeader = (token: string | null) => {
  return {
    Authorization: 'Bearer ' + token
  }
}