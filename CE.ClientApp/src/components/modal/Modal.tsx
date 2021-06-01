import React, { useEffect, useRef } from 'react'
import { useActions, useTypedSelector } from '../../hooks'

interface IModalProps {
  title: string,
  children: React.ReactNode,
  cancelButtonText?: string,
  submitButtonText: string,
  isSubmitDisabled?: boolean,
  isStaticBackdrop?: boolean,
  className?: string

  onSubmit(event: React.MouseEvent<HTMLElement>): void,

  onCancel?(event: React.MouseEvent<HTMLElement>): void
}

export const Modal: React.FC<IModalProps> = (props) => {
  const {
    title, children, cancelButtonText, submitButtonText, onSubmit, onCancel,
    className = '', isStaticBackdrop = false, isSubmitDisabled = false
  } = props
  const {theme} = useTypedSelector(state => state.settings)
  const {modal} = useTypedSelector((state => state.app))
  const {closeModal} = useActions()
  const modalRef = useRef<HTMLDivElement>(null)
  const body = document.getElementById('body')

  useEffect(() => {
    if (modal && modalRef.current) {
      body?.classList.add('overflow-hidden')
      setTimeout(() => {
        modalRef.current?.classList.add('show')
      }, 100)
    }
    return () => {
      body?.classList.remove('overflow-hidden')
    }
  }, [body?.classList, modal])

  if (!modal) {
    return null
  }

  const modalClasses = 'modal-dialog modal-dialog-centered modal-dialog-scrollable ' + className

  const clearClasses = () => {
    modalRef.current?.classList.remove('show')
  }

  const closeWithTimeout = () => {
    setTimeout(() => {
      closeModal()
    }, 150)
  }

  const onSubmitHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    clearClasses()
    onSubmit(event)
    closeWithTimeout()
  }

  const onCancelHandler = (event: React.MouseEvent<HTMLButtonElement | HTMLDivElement>) => {
    clearClasses()
    if (onCancel) {
      onCancel(event)
    }
    closeWithTimeout()
  }

  const onBackdropClickHandler = (event: React.MouseEvent<HTMLDivElement>) => {
    if (event.target === modalRef.current) {
      if (!isStaticBackdrop) {
        onCancelHandler(event)
      } else {
        let current = modalRef.current
        current.style.transition = 'transform .15s ease-in-out'
        current.style.transform = 'scale(1.01)'
        setTimeout(() => {
          current.style.transform = ''
          setTimeout(() => {
            current.style.transition = ''
          }, 150)
        }, 150)
      }
    }
  }

  return (
    <div className="modal fade d-block" id="modalContainer"
         onClick={onBackdropClickHandler} ref={modalRef}
         aria-labelledby="modalTitle" aria-hidden="false">
      <div className={modalClasses}>
        <div className={`modal-content border-0 bg-${theme === 'dark' ? 'gray-dark text-light' : theme}`}>
          <div className="modal-header border-0">
            <h5 className="modal-title user-select-none" id="modalTitle">{title}</h5>
            <button type="button" className={`btn-close${theme === 'dark' ? ' btn-close-white' : ''}`}
                    aria-label="Close" onClick={onCancelHandler}/>
          </div>
          <div className="modal-body">
            {children}
          </div>
          <div className="modal-footer border-0">
            {cancelButtonText &&
            <button type="button"
                    className={`btn btn-${theme === 'dark' ? theme : 'secondary'}`}
                    onClick={onCancelHandler}>
              {cancelButtonText}
            </button>}
            <button type="button"
                    disabled={isSubmitDisabled}
                    className="btn btn-info text-light"
                    onClick={onSubmitHandler}
                    data-bs-dismiss="modal">
              {submitButtonText}
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}