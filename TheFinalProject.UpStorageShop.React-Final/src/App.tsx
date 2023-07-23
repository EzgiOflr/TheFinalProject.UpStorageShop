import React, { Component, Suspense } from 'react'
import { HashRouter, Route, Routes } from 'react-router-dom'
import './scss/style.scss'
import { RootState } from './redux/store'
import { useSelector } from 'react-redux'

const loading = (
    <div className="pt-3 text-center">
        <div className="sk-spinner sk-spinner-pulse"></div>
    </div>
)

// Containers
const DefaultLayout = React.lazy(() => import('./layout/DefaultLayout'))

// Pages
const Login = React.lazy(() => import('./views/pages/login/Login'))
const SocialLogin = React.lazy(() => import('./views/pages/social-login/SocialLogin'))
const Register = React.lazy(() => import('./views/pages/register/Register'))
const Page404 = React.lazy(() => import('./views/pages/page404/Page404'))
const Page500 = React.lazy(() => import('./views/pages/page500/Page500'))

const App = () => {
    const auth = useSelector((state: RootState) => state.auth)

    return (
        <HashRouter>
            <Suspense fallback={loading}>
                {
                    auth.user ?
                        (
                            <Routes>
                                <Route path="*" element={<DefaultLayout />} />
                            </Routes>
                        )
                        :
                        (
                            <Routes>
                                <Route path="*" element={<Login />} />
                                <Route path="/social-login" element={<SocialLogin />} />
                            </Routes>
                        )
                }
            </Suspense>
        </HashRouter>
    )
}

export default App;