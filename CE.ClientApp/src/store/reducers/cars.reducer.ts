import { CarsAction, CarsActionTypes, ICarsState } from '../types'


const initialState: ICarsState = {
  cars: [],
  temp: null,
  isPending: false
}

export const carsReducer = (state = initialState, action: CarsAction): ICarsState => {
  switch (action.type) {
    case CarsActionTypes.CREATE_CAR_REQUEST:
      return {...state, isPending: true}
    case CarsActionTypes.CREATE_CAR_SUCCESS:
      return {cars: [...state.cars, action.payload], isPending: false, temp: null}
    case CarsActionTypes.CREATE_CAR_FAILURE:
      return {...state, isPending: false, temp: action.payload}
    default:
      return state
  }
}