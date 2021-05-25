import React from 'react'
import { Portal } from '../portal/Portal'
import { useTypedSelector } from '../../hooks'
import { Modal } from './Modal'

export const ModalContainer: React.FC = () => {
  const {modal} = useTypedSelector(state => state.app)
  return (
    <Portal className="modal-portal">{true &&
      <div className="modal fade"
           id="modalContainer"
           //data-bs-backdrop="static"
           data-bs-keyboard="true"
           tabIndex={-1}
           aria-labelledby="modalTitle"
           aria-hidden="true">
        <Modal title="Test modal" cancelButtonText="Cancel" onSubmit={()=> {alert('aaa')}} submitButtonText="Save">
          <h1>Lorem ipsum.</h1>
          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Adipisci cum fuga labore, molestiae quaerat
            reiciendis.</p>
        </Modal>
      </div>
    }</Portal>
  )
}