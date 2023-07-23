import { CCard, CCardBody, CCardFooter, CCardHeader, CFormSwitch } from "@coreui/react";
import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import api from "../../../utils/axiosInstance";
import { toast } from "react-toastify";
import { UserSetIsNotifactionAllowedResponse } from "../../../types/UserSetIsNotifactionAllowed-Response";
import { storeNewMailStatus, storeNewNotificationStatus, storeUser } from "../../../redux/reducers/authSlice";

const Settings = () => {
    const dispatch = useDispatch();
    const auth = useSelector((state: RootState) => state.auth)

    const changeNotificationState = async (_newState: boolean) => {
        try {
            var response = await api.post('/User/UserSetIsNotifactionAllowed', {
                newState: _newState
            });

            var responseData: UserSetIsNotifactionAllowedResponse = response.data;
            toast(responseData.message)

            dispatch(storeNewNotificationStatus(_newState));
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    const changeMailState = async (_newState: boolean) => {
        try {
            var response = await api.post('/User/UserSetIsMailAllowed', {
                newState: _newState
            });

            var responseData: UserSetIsNotifactionAllowedResponse = response.data;
            toast(responseData.message)

            dispatch(storeNewMailStatus(_newState));
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    return (
        <CCard>
            <CCardHeader>
                Settings
            </CCardHeader>
            <CCardBody>
                <div>
                    <CFormSwitch onChange={(e) => changeNotificationState(e.target.checked)} label="I want to get notifications" checked={auth.user?.isNotificationAllowed} />
                </div>
                <div className="mt-3">
                    <CFormSwitch onChange={(e) => changeMailState(e.target.checked)} label="I want to get Mails" checked={auth.user?.isMailAllowed} />
                </div>
            </CCardBody>
        </CCard>
    )
}

export default Settings;