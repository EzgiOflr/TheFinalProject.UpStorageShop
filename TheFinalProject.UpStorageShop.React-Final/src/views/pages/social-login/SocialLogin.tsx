import React, { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { getClaimsFromJwt } from "../../../utils/jwtHelper";
import { LocalJwt } from "../../../types/AuthTypes";
import { useDispatch, useSelector } from "react-redux";
import { storeNotification, storeUser } from "../../../redux/reducers/authSlice";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { MessageModel } from "../../../types/MessageModel";
import { RootState } from "../../../redux/store";

const SocialLogin = () => {
    const [searchParams] = useSearchParams();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const auth = useSelector((state: RootState) => state.auth)

    useEffect(() => {

        prepareLogin()


    }, []);

    const prepareLogin = async () => {
        const accessToken = searchParams.get("access_token");
        const expiryDate = searchParams.get("expiry_date");

        const { uid, email, given_name, family_name, isMailAllowed, isNotificationAllowed } = getClaimsFromJwt(accessToken);

        const expires: string | null = expiryDate;

        const localJwt: LocalJwt = {
            accessToken,
            expires
        }

        localStorage.setItem("upstorage_user", JSON.stringify(localJwt));

        const hubCn = new HubConnectionBuilder().withUrl("https://localhost:7172/Hubs/SeleniumLogHub").build()

        await hubCn.start();

        hubCn.on("NewSeleniumLogAdded", (message: MessageModel) => {
            dispatch(storeNotification({ ...message, isSeen: false }))
        })

        navigate('/')

        dispatch(storeUser({
            id: uid,
            email,
            firstName: given_name,
            lastName: family_name,
            expires,
            accessToken,
            isNotificationAllowed: isNotificationAllowed == 'true',
            isMailAllowed: isMailAllowed == 'true'
        }))
    }

    return (
        <></>
    )
}

export default SocialLogin;