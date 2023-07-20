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
                {/* <div className="mb-3">
                    <CFormLabel htmlFor="exampleFormControlInput1">How many products do you want to crawl?</CFormLabel>
                    <CFormInput onChange={(e) => setHowManyProducts(parseInt(e.target.value))} value={howManyProducts} type="number" />
                </div>

                <CFormLabel htmlFor="exampleFormControlInput1">What type of products you want to crawl?</CFormLabel>
                <CFormSelect onChange={(e) => setProductType(e.target.value)} aria-label="Default select example">
                    <option value="">Select</option>
                    <option value="A">All</option>
                    <option value="B">On Sale</option>
                    <option value="C">Not On Sale</option>
                </CFormSelect> */}
            </CCardBody>
        </CCard>
    )
}

export default Settings;