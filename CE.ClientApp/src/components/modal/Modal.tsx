import React, { useEffect, useRef } from 'react'
import { useActions, useTypedSelector } from '../../hooks'

interface IModalProps {
  title: string,
  children: React.ReactNode,
  cancelButtonText?: string,
  submitButtonText: string,
  onSubmit(event: React.MouseEvent<HTMLElement>): void,
  onCancel?(event: React.MouseEvent<HTMLElement>): void
}

export const Modal: React.FC<IModalProps> = (props) => {
  const {title, children, cancelButtonText, submitButtonText, onSubmit, onCancel} = props
  const {theme} = useTypedSelector(state => state.settings)
  const {modal} = useTypedSelector((state => state.app))
  const {closeModal} = useActions()
  const backdropRef = useRef<HTMLDivElement>(null)
  const modalRef = useRef<HTMLDivElement>(null)
  const body = document.getElementById('body')

  useEffect(() => {
    if (modal && modalRef.current && backdropRef.current) {
      body?.classList.add('overflow-hidden')
      setTimeout(() => {
        modalRef.current?.classList.add('show')
        backdropRef.current?.classList.add('show')
      }, 100)
    }
    return () => {
      body?.classList.remove('overflow-hidden')
    }
  }, [modal])

  if (!modal) {
    return null
  }

  const clearClasses = () => {
    modalRef.current?.classList.remove('show')
    backdropRef.current?.classList.remove('show')
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

  const onCancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    clearClasses()
    if (onCancel) {
      onCancel(event)
    }
    closeWithTimeout()
  }

  return (
    <>
      <div className="modal fade d-block"
           ref={modalRef}
           id="modalContainer"
           aria-labelledby="modalTitle"
           aria-hidden="true">
        <div className="modal-dialog modal-dialog-centered modal-dialog-scrollable">
          <div className={`modal-content border-0 bg-${theme === 'dark' ? 'secondary text-light' : theme}`}>
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
                      className="btn btn-info text-light"
                      onClick={onSubmitHandler}
                      data-bs-dismiss="modal">
                {submitButtonText}
              </button>
            </div>
          </div>
        </div>
      </div>
      <div className="modal-backdrop fade show" ref={backdropRef}/>
    </>
  )
}