import React, { useEffect, useMemo } from 'react'
import { toast, ToastContainer, ToastOptions } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import { Portal } from '../portal/Portal'
import { useActions, useTypedSelector } from '../../hooks'
import { useTranslation } from 'react-i18next'


export const Toast: React.FC = () => {
  const {clearAlert} = useActions()
  const {message, time} = useTypedSelector(state => state.alert)
  const {t} = useTranslation()

  const toastOptions: ToastOptions = useMemo(() => {
    return {
      hideProgressBar: true,
      pauseOnHover: true,
      draggable: true,
      pauseOnFocusLoss: false,
      position: "bottom-right",
      onClose: clearAlert,
      bodyClassName: "text-break"
    }
  }, [clearAlert])

  useEffect(() => {
    if (message) {
      toast(t(message), {...toastOptions, toastId: time || message})
    }
  }, [message, toastOptions, t, time])

  return (
    <Portal className="toasts-portal text-wrap">
      <ToastContainer/>
    </Portal>
  )
}