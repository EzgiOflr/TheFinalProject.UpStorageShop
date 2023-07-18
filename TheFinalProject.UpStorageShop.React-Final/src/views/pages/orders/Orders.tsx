import { CCard, CCardBody, CCardHeader, CModal, CModalBody, CModalHeader, CModalTitle, CTable, CTableBody, CTableDataCell, CTableHead, CTableHeaderCell, CTableRow } from "@coreui/react";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../utils/axiosInstance";
import { GetOrdersResponse } from "../../../types/GetOrders-Response";
import CIcon from "@coreui/icons-react";
import { cilList } from "@coreui/icons";
import { GetOrderEventsByOrderId } from "../../../types/GetOrderEventsByOrderId-Response";

const Orders = () => {
    const [data, setData] = useState<GetOrdersResponse[]>([]);
    const [modalData, setModalData] = useState<GetOrderEventsByOrderId[] | undefined>(undefined)

    const tableColors = ["primary", "secondary", "success", "danger", "warning", "info", "light", "dark"];

    useEffect(() => {
        getData();
    }, [])

    const getData = async () => {
        try {
            var response = await api.post('/Order/GetOrders', {});
            var responseData: GetOrdersResponse[] = response.data;
            setData(responseData);
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    const getOrderEvents = async (_orderId: string) => {
        try {
            var response = await api.post('/OrderEvent/GetOrderEventsByOrderId', {
                orderId: _orderId
            });

            var responseData: GetOrderEventsByOrderId[] = response.data;
            setModalData(responseData);
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    return (
        <React.Fragment>
            <CCard>
                <CCardHeader>
                    Orders
                </CCardHeader>
                <CCardBody>
                    <CTable>
                        <CTableHead>
                            <CTableRow>
                                <CTableHeaderCell scope="col">Order Id</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Requested Count</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Total Found Amount</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Crawled Type</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Date</CTableHeaderCell>
                                <CTableHeaderCell scope="col"></CTableHeaderCell>
                            </CTableRow>
                        </CTableHead>
                        <CTableBody>
                            {
                                data.map((item, index) =>
                                    <CTableRow color={tableColors[index % tableColors.length]} key={index}>
                                        <CTableDataCell>{item.orderId}</CTableDataCell>
                                        <CTableDataCell>{item.requestedCount}</CTableDataCell>
                                        <CTableDataCell>{item.totalFoundAmount}</CTableDataCell>
                                        <CTableDataCell>{item.crawledType}</CTableDataCell>
                                        <CTableDataCell>{item.date}</CTableDataCell>
                                        <CTableDataCell><CIcon onClick={() => getOrderEvents(item.orderId)} icon={cilList} /></CTableDataCell>
                                    </CTableRow>
                                )
                            }
                        </CTableBody>
                    </CTable>
                </CCardBody>
            </CCard>

            <CModal size="lg" visible={modalData !== undefined} onClose={() => setModalData(undefined)}>
                <CModalHeader>
                    <CModalTitle>Order Events</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CTable>
                        <CTableHead>
                            <CTableRow>
                                <CTableHeaderCell scope="col">Date</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Status</CTableHeaderCell>
                            </CTableRow>
                        </CTableHead>
                        <CTableBody>
                            {
                                modalData?.map((item, index) =>
                                    <CTableRow color={tableColors[index % tableColors.length]} key={index}>
                                        <CTableDataCell>{item.date}</CTableDataCell>
                                        <CTableDataCell>{item.status}</CTableDataCell>
                                    </CTableRow>
                                )
                            }
                        </CTableBody>
                    </CTable>
                </CModalBody>
            </CModal>
        </React.Fragment>
    )
}

export default Orders;