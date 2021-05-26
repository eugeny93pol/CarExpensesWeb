import React from 'react'
import { Modal } from '../Modal'

//TODO
export const ModalAddRepair: React.FC = () => {
  const submitHandler = () => {
    console.log('submit')
  }

  return (
    <Modal title="Add repair" submitButtonText="Save" onSubmit={submitHandler}>
      <h3>Repair</h3>
    </Modal>
  )
}