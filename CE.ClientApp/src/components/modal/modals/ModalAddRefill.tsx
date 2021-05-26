import React from 'react'
import { Modal } from '../Modal'

//TODO
export const ModalAddRefill: React.FC = () => {
  const submitHandler = () => {
    console.log('submit')
  }

  return (
    <Modal title="Add refill" submitButtonText="Add refill" onSubmit={submitHandler}>
      <h3>Refill</h3>
    </Modal>
  )
}