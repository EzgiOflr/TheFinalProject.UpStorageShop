import React, { useEffect, useState } from "react";
import { MessageModel } from "../../../types/MessageModel";
import './Terminal.css'
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";

const LiveLog = () => {
    const auth = useSelector((state: RootState) => state.auth)

    return (
        <div className="terminal space shadow">
            <div className="top">
                <div className="btns">
                    <span className="circle red"></span>
                    <span className="circle yellow"></span>
                    <span className="circle green"></span>
                </div>
                <div className="title">Ezgi Oflar UpStorage Shop</div>
            </div>
            <pre className="body">
                {
                    auth.notificationList.length == 0 ? "..." : auth.notificationList.map(x => x.message).join('\n')
                }
            </pre>
        </div>
    )
}

export default LiveLog;