import React from 'react';
import { CCard, CCardBody, CCardGroup, CCol, CContainer, CRow } from '@coreui/react';
import './Login.css';


const Login = () => {
    const logo = require('../../../assets/images/project_logo.png');

    return (
        <div className="bg-light min-vh-100 cont">
            <CContainer>
                <CRow className="justify-content-center">
                    <CCol md="1"></CCol>
                    <CCol md="5">
                        <CCardGroup className="login-container">
                            <CCard className="p-4">
                                <CCardBody className="text-center">
                                    <img
                                        style={{ width: 150, marginBottom: 20 }}
                                        src={logo} 
                                        alt="Project Logo" 
                                    />
                                    <a href="https://localhost:7172/api/Authentication/GoogleSignInStart" className="google-login-button">
                                        Log in with Google
                                    </a>
                                </CCardBody>
                            </CCard>
                        </CCardGroup>
                    </CCol>
                </CRow>
            </CContainer>
        </div>
    );
};

export default Login;
