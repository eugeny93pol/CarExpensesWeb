import React from 'react'
import ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import 'bootstrap/dist/js/bootstrap.bundle.min'
import { App } from './App'
import { store } from './store'
import './index.scss'
import './i18n'


ReactDOM.render(
  <Provider store={store}>
    <App/>
  </Provider>,
  document.getElementById('root')
)