import React from 'react'
import { useTypedSelector } from '../../hooks'
import { Portal } from '../portal/Portal'
import { ModalAddMileage } from './modals/ModalAddMileage'
import { ModalTypes } from '../../types'
import { ModalAddRepair } from './modals/ModalAddRepair'
import { ModalAddPurchases } from './modals/ModalAddPurchases'
import { ModalAddRefill } from './modals/ModalAddRefill'
import { ModalAddCar } from './modals/ModalAddCar'

export const ModalContainer: React.FC = () => {
  const {modal} = useTypedSelector(state => state.app)

  return (
    <Portal className="modal-root">
      {modal === ModalTypes.ADD_MILEAGE && <ModalAddMileage/>}
      {modal === ModalTypes.ADD_REPAIR && <ModalAddRepair/>}
      {modal === ModalTypes.ADD_PURCHASES && <ModalAddPurchases/>}
      {modal === ModalTypes.ADD_REFILL && <ModalAddRefill/>}
      {modal === ModalTypes.ADD_CAR && <ModalAddCar/>}
    </Portal>
  )
}