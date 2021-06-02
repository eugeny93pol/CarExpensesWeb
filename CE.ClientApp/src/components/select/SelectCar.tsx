import React, { useMemo } from 'react'
import { IOptionSelect, Select } from './Select'
import { ModalTypes } from '../../types'
import { useActions, useTypedSelector } from '../../hooks'


export const SelectCar: React.FC = () => {
  const {cars} = useTypedSelector(state => state.cars)
  const {openModal, setDefaultCar} = useActions()

  const options: IOptionSelect[] = useMemo(() => cars.map(car => ({
    key: car.id,
    value: `${car.brand} ${car.model}`,
    selected: true
  })), [cars])

  const selectHandler = (option: IOptionSelect) => {
    setDefaultCar(option.key)
  }

  return (
    <Select id={'carsSelect'}
            placeholder={'Select a car'}
            options={options}
            onSelect={selectHandler}>
      <button className="dropdown-item text-center"
              onClick={() => openModal(ModalTypes.ADD_CAR)}>
        <i className="bi bi-plus-circle"/>
      </button>
    </Select>
  )
}