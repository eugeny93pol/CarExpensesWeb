import React, { useEffect, useRef } from 'react'
import { Portal } from '../portal/Portal'
import { useActions, useTypedSelector } from '../../hooks'
import { Modal } from './Modal'
import { Modal as BootstrapModal} from 'bootstrap'

export const ModalContainer: React.FC = () => {
  const {modal} = useTypedSelector(state => state.app)
  const {closeModal} = useActions()
  const modalRef = useRef<HTMLDivElement>(null)

  useEffect(() => {
    if (modalRef.current) {
      modalRef.current.addEventListener('hidden.bs.modal', () => {
        closeModal()
      })
    }
  },[modalRef])

  useEffect(() => {
    if (modal && modalRef.current) {
      new BootstrapModal(modalRef.current).show()
    }
  }, [modal])

  return (
    <Portal className="modal-portal">
      <div className="modal fade"
           id="modalContainer"
           ref={modalRef}
           tabIndex={-1}
           data-bs-backdrop="static"
           data-bs-keyboard="true"
           aria-labelledby="modalTitle"
           aria-hidden="true">
        {modal &&
        <Modal title="Test modal"
               cancelButtonText="Cancel"
               onSubmit={() => alert('aaa')}
               onCancel={() => console.log('close')}
               submitButtonText="Save">
          <h1>Lorem ipsum.</h1>
          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Adipisci cum fuga labore, molestiae quaerat
            reiciendis.</p>
        </Modal>
        }
      </div>
    </Portal>
  )
}