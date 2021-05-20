import { useTypedSelector } from './useTypedSelector'
import { request } from '../helpers'
import { useCallback } from 'react'

export const useRequest = () => {
  const {token} = useTypedSelector(state => state.auth)

  return useCallback(async (url: string, method: string = 'GET', body: any = null) => {
    return await request(url, method, body, { Authorization: 'Bearer ' + token })
  }, [token])
}