import React, { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { storeUser } from "../../../redux/reducers/authSlice";
import { RootState } from "../../../redux/store";

const Logout = () => {
    const [searchParams] = useSearchParams();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const auth = useSelector((state: RootState) => state.auth)

    useEffect(() => {

        logout()


    }, []);

    const logout = () => {
        localStorage.removeItem("upstorage_user");

        dispatch(storeUser((undefined)));

        navigate("/");
    }

    return (
        <></>
    )
}

export default Logout;