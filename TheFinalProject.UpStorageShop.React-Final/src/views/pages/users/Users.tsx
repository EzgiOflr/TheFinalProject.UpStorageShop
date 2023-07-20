import CIcon from "@coreui/icons-react";
import { CCard, CCardBody, CCardHeader, CTable, CTableBody, CTableDataCell, CTableHead, CTableHeaderCell, CTableRow } from "@coreui/react";
import React, { useEffect, useState } from "react";
import api from "../../../utils/axiosInstance";
import { toast } from "react-toastify";
import { GetAllUsersResponse } from "../../../types/GetAllUsers-Response";
import { cilCheck, cilX } from "@coreui/icons";

const Users = () => {
    const [data, setData] = useState<GetAllUsersResponse[]>([]);
    const tableColors = ["primary", "secondary", "success", "danger", "warning", "info", "light", "dark"];

    useEffect(() => {
        getData();
    }, [])

    const getData = async () => {
        try {
            var response = await api.post('/User/GetAllUsers', {});
            var responseData: GetAllUsersResponse[] = response.data;
            setData(responseData);
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    return (
        <CCard>
            <CCardHeader>
                Users
            </CCardHeader>
            <CCardBody>
                <CTable>
                    <CTableHead>
                        <CTableRow>
                            <CTableHeaderCell scope="col">User Id</CTableHeaderCell>
                            <CTableHeaderCell scope="col">Name Surname</CTableHeaderCell>
                            <CTableHeaderCell scope="col">E-Mail</CTableHeaderCell>
                            <CTableHeaderCell scope="col">Is Notification Allowed</CTableHeaderCell>
                            <CTableHeaderCell scope="col">Is Email Allowed</CTableHeaderCell>
                        </CTableRow>
                    </CTableHead>
                    <CTableBody>
                        {
                            data.map((item, index) =>
                                <CTableRow color={tableColors[index % tableColors.length]} key={index}>
                                    <CTableDataCell>{item.userId}</CTableDataCell>
                                    <CTableDataCell>{item.nameSurname}</CTableDataCell>
                                    <CTableDataCell>{item.email}</CTableDataCell>
                                    <CTableDataCell>
                                        <CIcon icon={item.isNotificationAllowed ? cilCheck : cilX} />
                                    </CTableDataCell>
                                    <CTableDataCell>
                                        <CIcon icon={item.isMailAllowed ? cilCheck : cilX} />
                                    </CTableDataCell>
                                </CTableRow>
                            )
                        }
                    </CTableBody>
                </CTable>
            </CCardBody>
        </CCard>
    )
}

export default Users;