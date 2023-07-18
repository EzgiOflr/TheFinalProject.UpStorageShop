import 'react-app-polyfill/stable'
import 'core-js'
import React from 'react'
import { createRoot } from 'react-dom/client'
import reportWebVitals from './reportWebVitals'
import { Provider, useSelector } from 'react-redux'
import { RootState, store } from './redux/store'
import App from './App'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css';


createRoot(document.getElementById('root') as HTMLElement).render(
    <Provider store={store}>
        <App />
        <ToastContainer />
    </Provider>
)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()