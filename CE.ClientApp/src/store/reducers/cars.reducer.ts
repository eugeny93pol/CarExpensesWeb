import { CarsAction, CarsActionTypes, ICarsState } from '../types'


const initialState: ICarsState = {
  cars: [],
  isPending: false
}

export const carsReducer = (state = initialState, action: CarsAction): ICarsState => {
  switch (action.type) {
    case CarsActionTypes.CREATE_CAR_REQUEST:
      return {...state, isPending: true}
    case CarsActionTypes.CREATE_CAR_SUCCESS:
      return {cars: [...state.cars, action.payload], isPending: false}
    case CarsActionTypes.CREATE_CAR_FAILURE:
      return {...state, isPending: false}
    default:
      return state
  }
}