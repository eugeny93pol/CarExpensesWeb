import React from 'react'
import { Modal } from '../Modal'

//TODO
export const ModalAddMileage: React.FC = () => {
  const submitHandler = () => {
    console.log('submit')
  }

  return (
    <Modal title="Add mileage" submitButtonText="Add" onSubmit={submitHandler}>
      <h3>Mileage</h3>
    </Modal>
  )
}