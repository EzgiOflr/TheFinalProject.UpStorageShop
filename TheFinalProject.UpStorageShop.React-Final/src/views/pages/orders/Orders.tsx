import { CButton, CCard, CCardBody, CCardHeader, CModal, CModalBody, CModalHeader, CModalTitle, CTable, CTableBody, CTableDataCell, CTableHead, CTableHeaderCell, CTableRow } from "@coreui/react";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../utils/axiosInstance";
import { GetOrdersResponse } from "../../../types/GetOrders-Response";
import CIcon from "@coreui/icons-react";
import { cilCloudDownload, cilList, cilTrash } from "@coreui/icons";
import { GetOrderEventsByOrderId } from "../../../types/GetOrderEventsByOrderId-Response";
import { GetProductsByOrderIdResponse } from "../../../types/GetProductsByOrderId-Response";
import { DeleteOrderResponse } from "../../../types/DeleteOrder-Response";
import { OrdersExportResponse } from "../../../types/OrdersExport-Response";

const Orders = () => {
    const [data, setData] = useState<GetOrdersResponse[]>([]);
    const [orderEvents, setOrderEvents] = useState<GetOrderEventsByOrderId[] | undefined>(undefined)
    const [orderProducts, setOrderProducts] = useState<GetProductsByOrderIdResponse[] | undefined>(undefined)

    const tableColors = ["primary", "secondary", "success", "danger", "warning", "info", "light", "dark"];

    useEffect(() => {
        getData();
    }, [])

    const deleteOrder = async (_orderId: string) => {
        try {
            var response = await api.post('/Order/DeleteOrder', {
                orderId: _orderId
            });

            var responseData: DeleteOrderResponse = response.data;

            toast(responseData.message)

            if (responseData.data)
                getData();
        }
        catch (err) {
            toast("An error occured.")
        }
    }

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
            setOrderEvents(responseData);
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    const getOrderProducts = async (_orderId: string) => {
        try {
            var response = await api.post('/Product/GetProductsByOrderId', {
                orderId: _orderId
            });

            var responseData: GetProductsByOrderIdResponse[] = response.data;
            setOrderProducts(responseData);
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    const excelExport = async () => {
        try {
            var response = await api.post('/Export/OrdersExport', {

            });

            var responseData: OrdersExportResponse = response.data;

            fetch("https://localhost:7172/Export/" + responseData.message)
                .then(response => response.blob())
                .then(blob => {
                    // Dosyayı indirecek bir link oluşturun
                    const downloadLink = URL.createObjectURL(blob);

                    // Sanal bir <a> elemanı oluşturun ve indirme işlemini tetikleyin
                    const anchorElement = document.createElement('a');
                    anchorElement.href = downloadLink;
                    anchorElement.download = "Orders_" + responseData.message;
                    anchorElement.click();

                    // Artık gerekli olmadığı için oluşturulan linki temizleyin
                    URL.revokeObjectURL(downloadLink);
                })
                .catch(error => {
                    console.error('Excel dosyası indirme sırasında bir hata oluştu:', error);
                });
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    return (
        <React.Fragment>
            <CCard>
                <CCardHeader>
                    <div style={{ display: 'flex', flexDirection: 'row' }}>
                        <div style={{ display: 'flex', flexGrow: 1, alignItems: 'center' }}>
                            Orders
                        </div>
                        <div style={{ display: 'flex', }}>
                            <CButton onClick={excelExport}>
                                <CIcon style={{ marginRight: 10 }} onClick={() => { }} icon={cilCloudDownload} />
                                Excel Export
                            </CButton>
                        </div>
                    </div>
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
                                <CTableHeaderCell scope="col"></CTableHeaderCell>
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
                                        <CTableDataCell><CIcon onClick={() => getOrderProducts(item.orderId)} icon={cilList} /></CTableDataCell>
                                        <CTableDataCell><CIcon onClick={() => deleteOrder(item.orderId)} icon={cilTrash} /></CTableDataCell>
                                    </CTableRow>
                                )
                            }
                        </CTableBody>
                    </CTable>
                </CCardBody>
            </CCard>

            <CModal size="lg" visible={orderEvents !== undefined} onClose={() => setOrderEvents(undefined)}>
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
                                orderEvents?.map((item, index) =>
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

            <CModal size="lg" visible={orderProducts !== undefined} onClose={() => setOrderProducts(undefined)}>
                <CModalHeader>
                    <CModalTitle>Order Products</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CTable>
                        <CTableHead>
                            <CTableRow>
                                <CTableHeaderCell scope="col">Name</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Picture</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Is On Sale</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Price</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Sale Price</CTableHeaderCell>
                                <CTableHeaderCell scope="col">Date</CTableHeaderCell>
                            </CTableRow>
                        </CTableHead>
                        <CTableBody>
                            {
                                orderProducts?.map((item, index) =>
                                    <CTableRow color={tableColors[index % tableColors.length]} key={index}>
                                        <CTableDataCell>{item.name}</CTableDataCell>
                                        <CTableDataCell>
                                            <img style={{ width: '75px' }} className="img img-thumbnail" src={item.picture} />
                                        </CTableDataCell>
                                        <CTableDataCell>{item.isOnSale}</CTableDataCell>
                                        <CTableDataCell>{item.price}</CTableDataCell>
                                        <CTableDataCell>{item.salePrice == 0 ? "" : item.salePrice}</CTableDataCell>
                                        <CTableDataCell>{item.date}</CTableDataCell>
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