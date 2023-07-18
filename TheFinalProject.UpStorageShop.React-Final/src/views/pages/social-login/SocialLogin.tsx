import React, { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { getClaimsFromJwt } from "../../../utils/jwtHelper";
import { LocalJwt } from "../../../types/AuthTypes";
import { useDispatch } from "react-redux";
import { storeUser } from "../../../redux/reducers/authSlice";

const SocialLogin = () => {
    const [searchParams] = useSearchParams();
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {

        const accessToken = searchParams.get("access_token");
        const expiryDate = searchParams.get("expiry_date");

        const { uid, email, given_name, family_name } = getClaimsFromJwt(accessToken);

        const expires: string | null = expiryDate;

        // setAppUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken });

        const localJwt: LocalJwt = {
            accessToken,
            expires
        }

        localStorage.setItem("upstorage_user", JSON.stringify(localJwt));

        // navigate("/");

        console.log(email);

        console.log(given_name);

        console.log(family_name);

        navigate('/')
        dispatch(storeUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken }))

       
    }, []);

    return (
        <></>
    )
}

export default SocialLogin;