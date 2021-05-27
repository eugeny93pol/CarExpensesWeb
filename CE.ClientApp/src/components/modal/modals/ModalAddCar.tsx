import React from 'react'
import { Modal } from '../Modal'

//TODO
export const ModalAddCar: React.FC = () => {
  const submitHandler = () => {
    console.log('submit')
  }

  return (
    <Modal title="Add Car" submitButtonText="Save" onSubmit={submitHandler}>
      <h3>Add Car</h3>
    </Modal>
  )
}