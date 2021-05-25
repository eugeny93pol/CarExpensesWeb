import React from 'react'
import { useTypedSelector } from '../../hooks'

interface IModalProps {
  title: string,
  children: React.ReactNode,
  cancelButtonText?: string,
  submitButtonText: string,

  onSubmit?(event?: React.MouseEvent<HTMLElement>): void,

  onCancel?(event?: React.MouseEvent<HTMLElement>): void
}

export const Modal: React.FC<IModalProps> = (props) => {
  const {
    title, children,
    cancelButtonText,
    submitButtonText,
    onSubmit, onCancel
  } = props
  const {theme} = useTypedSelector(state => state.settings)

  return (
    <div className="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div className={`modal-content border-0 bg-${theme === 'dark' ? 'secondary text-light' : theme}`}>
        <div className="modal-header border-0">
          <h5 className="modal-title user-select-none" id="modalTitle">{title}</h5>
          <button type="button" className={`btn-close${theme === 'dark' ? ' btn-close-white' : ''}`}
                  data-bs-dismiss="modal" aria-label="Close" onClick={onCancel}/>
        </div>
        <div className="modal-body">
          {children}
        </div>
        <div className="modal-footer border-0">
          {cancelButtonText &&
          <button type="button"
                  className={`btn btn-${theme === 'dark' ? theme : 'secondary'}`}
                  onClick={onCancel}
                  data-bs-dismiss="modal">
            {cancelButtonText}
          </button>}
          <button type="button"
                  className="btn btn-info text-light"
                  onClick={onSubmit}
                  data-bs-dismiss="modal">
            {submitButtonText}
          </button>
        </div>
      </div>
    </div>
  )
}