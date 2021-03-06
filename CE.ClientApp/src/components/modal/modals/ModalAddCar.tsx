import React from 'react'
import { Modal } from '../Modal'
import { useTranslation } from 'react-i18next'
import { useActions, useForm, useTypedSelector } from '../../../hooks'
import config from '../../../config/default.json'


export const ModalAddCar: React.FC = () => {
  const {user, token} = useTypedSelector(state => state.auth)
  const {temp} = useTypedSelector(state => state.cars)
  const {form, validationResult, changeHandler} = useForm(temp ? temp : {
    brand: '', model: '', year: '', vin: ''
  })

  const {t} = useTranslation()
  const {createCar} = useActions()

  const submitHandler = () => {
    if (user && token) {
      createCar(user.id, token, form)
    }
  }

  return (
    <Modal title={t('modal.addCar.title')}
           submitButtonText={t('modal.addCar.button.save')}
           onSubmit={submitHandler}
           className="modal-fullscreen-lg-down modal-lg"
           isStaticBackdrop
           isSubmitDisabled={!(validationResult.brand
             && validationResult.year
             && (form.vin ? validationResult.vin : true)
           )}>
      <div className="container-fluid">
        <div className="row mb-2">
          <div className="col-sm">
            <label htmlFor="brand"><h6>Brand</h6></label>
            <input type="text"
                   className="form-control"
                   name="brand" id="brand"
                   value={form.brand}
                   onChange={changeHandler}
                   placeholder="Input brand"/>
          </div>
          <div className="col-sm mt-3 mt-sm-0">
            <label htmlFor="model"><h6>Model</h6></label>
            <input type="text"
                   className="form-control"
                   name="model" id="model"
                   value={form.model}
                   onChange={changeHandler}
                   placeholder={'Input model'}/>
          </div>
        </div>
        <div className="row mb-2">
          <div className="col-sm">
            <label htmlFor="year"><h6 className="mt-3">Year of manufacture</h6></label>
            <input type="number"
                   className="form-control"
                   value={form.year}
                   onChange={changeHandler}
                   name="year"
                   min={config.startCarYear}
                   max={(new Date()).getFullYear()}
                   placeholder={'' + (new Date()).getFullYear()}/>
          </div>
          <div className="col-sm mt-3 mt-sm-0">
            <label htmlFor="vin"><h6 className="mt-3">VIN</h6></label>
            <input type="text"
                   className="form-control"
                   name="vin" id="vin"
                   value={form.vin}
                   onChange={changeHandler}
                   maxLength={config.vinLength}
                   placeholder={'Input VIN'}/>
          </div>
        </div>
        <h6 className="mt-3">Options</h6>
      </div>
    </Modal>
  )
}