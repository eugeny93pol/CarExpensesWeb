import React from 'react'
import { Modal } from '../Modal'

//TODO
export const ModalAddPurchases: React.FC = () => {
  const submitHandler = () => {
    console.log('submit')
  }

  return (
    <Modal title="Add purchases" submitButtonText="Add" onSubmit={submitHandler}>
      <h3>Purchases</h3>
    </Modal>
  )
}