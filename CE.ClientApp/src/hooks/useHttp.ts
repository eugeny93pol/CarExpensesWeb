import { useCallback } from 'react'


export const useHttp = () => {
  const request = useCallback(async (url: string, method: string = 'GET', body: any = null, headers = {}) => {
    //setLoading

    try {
      if (body) {
        body = JSON.stringify(body)
        headers['Content-type'] = 'application/json'
      }
      const response = await fetch(url, {method, body, headers})
      const fetched = await response.json()

      // if (!response.ok) {
      //   throw new Error(
      //     JSON.stringify({ message: fetched.message, status: response.status })
      //   )
      // }

      return fetched

    } catch (e) {
      throw e
    }


  }, [])

  return {request}
}