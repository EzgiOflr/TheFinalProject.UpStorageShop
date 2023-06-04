import React, { useEffect, useState } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import './Terminal.css'
import axios from "axios";
import { GetOrdersResponse } from "./models/getOrders-response";
import { GetOrderEventsByOrderIdResponse } from "./models/getOrderEventsByOrderId-response";
import { GetProductsByOrderIdResponse } from "./models/getProductsByOrderId-response";


const App = () => {
    const [text, setText] = useState<string>("");
    const [msgList, setMsgList] = useState<MessageModel[]>([])
    const [hubConnection, setHubConnection] = useState<HubConnection>()
    const [orders, setOrders] = useState<GetOrdersResponse[]>([])
    const [orderEvents, setOrderEvents] = useState<GetOrderEventsByOrderIdResponse[]>([])
    const [selectedOrderId, setSelectedOrderId] = useState('');
    const [products, setProducts] = useState<GetProductsByOrderIdResponse[]>([])

    interface MessageModel {
        message: string,
        sentOn: string
    }

    useEffect(() => {
        createHubConnection();
        getOrders();
    }, [])

    // const sendMsg = () => {
    //     if (hubConnection) {
    //         hubConnection.invoke("SendMessage", text).then((res) => { })
    //     }
    // }

    useEffect(() => {
        if (hubConnection)
            hubConnection.on("NewSeleniumLogAdded", (mesaj: MessageModel) => {
                setMsgList((prevState) => {
                    return prevState.concat(mesaj)
                })
            })
    }, [hubConnection])

    const getOrders = () => {

        axios.post('https://localhost:7172/api/Order/GetOrders', {})
            .then((response) => {
                setOrders(response.data);

            })

    }
    const getOrderEventsByOrderId = (OrderId: string) => {
        setSelectedOrderId(OrderId);

        axios.post('https://localhost:7172/api/OrderEvent/GetOrderEventsByOrderId', {
            OrderId: OrderId
        })
            .then((response) => {
                setOrderEvents(response.data);
            })

    }
    const getProductsByOrderId = (OrderId: string) => {
        setSelectedOrderId(OrderId);

        axios.post('https://localhost:7172/api/Product/GetProductsByOrderId', {
            OrderId: OrderId
        })
            .then((response) => {
                setProducts(response.data);
            })

    }


    const sendOrderMail = () => {
        axios.post('https://localhost:7172/api/Export/OrdersExport', {})
            .then((response) => {
                alert(response.data.message);
            })

    }

    const sendOrderEventMail = () => {
        axios.post('https://localhost:7172/api/Export/GetOrderEventsByOrderIdExport', {
            OrderId: selectedOrderId
        })
            .then((response) => {
                alert(response.data.message);
            })

    }
    const sendProductMail = () => {
        axios.post('https://localhost:7172/api/Export/GetProductsByOrderIdExport', {
            OrderId: selectedOrderId
        })
            .then((response) => {
                alert(response.data.message);
            })

    }
    const createHubConnection = async () => {
        const hubCn = new HubConnectionBuilder().withUrl("https://localhost:7172/Hubs/SeleniumLogHub").build()
        try {
            await hubCn.start();
            console.log(hubCn.connectionId)
            setHubConnection(hubCn)
        } catch (e) {
            console.log("e", e)
        }
    }

    return (
        <div className="container-fluid">

            <div className="row">
                <div className="col-md-6">
                    {
                        orderEvents.length == 0 && products.length == 0 && (
                            <React.Fragment>
                                <div className="row">
                                    <div className="col-md-6">
                                        <button onClick={getOrders} className="btn btn-primary">Refresh</button>
                                    </div>
                                    <div className="col-md-6 text-right clearfix">
                                        <button onClick={sendOrderMail} className="btn btn-primary float-right">Send Order Mail</button>
                                    </div>
                                </div>

                                <table className="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Order Id</th>
                                            <th>Requested Count</th>
                                            <th>Total Found Amount</th>
                                            <th>Crawle Type</th>
                                            <th>Date</th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            orders.map((item, index) =>
                                                <tr>
                                                    <td>{item.orderId}</td>
                                                    <td>{item.requestedCount}</td>
                                                    <td>{item.totalFoundAmount}</td>
                                                    <td>{item.crawledType}</td>
                                                    <td>{item.date}</td>
                                                    <td><a href="javascript:;" onClick={() => getOrderEventsByOrderId(item.orderId)}>Events</a></td>
                                                    {/* a elementini sayfayı yukarı atmadan tıklanabilir yapmanın tek yolu javasciprt:; vermektir. */}
                                                    <td><a href="javascript:;" onClick={() => getProductsByOrderId(item.orderId)}>Products</a></td>
                                                </tr>
                                            )
                                        }
                                    </tbody>
                                </table>
                            </React.Fragment>
                        )
                    }

                    {
                        orderEvents.length > 0 && (
                            <React.Fragment>
                                <div className="row">
                                    <div className="col-md-6">
                                        <button onClick={() => setOrderEvents([])} className="btn btn-primary">Back</button>
                                    </div>
                                    <div className="col-md-6 text-right clearfix">
                                        <button onClick={sendOrderEventMail} className="btn btn-primary float-right">Send Order Event Mail</button>
                                    </div>
                                </div>

                                <table className="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Order Id</th>
                                            <th>Status</th>
                                            <th>Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            orderEvents.map((item, index) =>
                                                <tr>
                                                    <td>{item.orderId}</td>
                                                    <td>{item.status}</td>
                                                    <td>{item.date}</td>
                                                </tr>
                                            )
                                        }
                                    </tbody>
                                </table>
                            </React.Fragment>
                        )
                    }

                    {
                        products.length > 0 && (
                            <React.Fragment>
                                <div className="row">
                                    <div className="col-md-6">
                                        <button onClick={() => setProducts([])} className="btn btn-primary">Back</button>
                                    </div>
                                    <div className="col-md-6 text-right clearfix">
                                        <button onClick={sendProductMail} className="btn btn-primary float-right">Send Product Mail</button>
                                    </div>
                                </div>

                                <table className="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Order Id</th>
                                            <th>Name</th>
                                            <th>Is On Sale</th>
                                            <th>Price</th>
                                            <th>Sale Price</th>
                                            <th>Picture</th>
                                            <th>Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            products.map((item, index) =>
                                                <tr>
                                                    <td>{item.orderId}</td>
                                                    <td>{item.name}</td>
                                                    <td>{item.isOnSale}</td>
                                                    <td>{item.price}</td>
                                                    <td>{item.salePrice}</td>
                                                    {/* <td>{item.picture}</td> */}
                                                    <td><img src={item.picture} style={{ width: 50 }}></img></td>
                                                    <td>{item.date}</td>

                                                </tr>
                                            )
                                        }
                                    </tbody>
                                </table>
                            </React.Fragment>
                        )
                    }
                </div>
                <div className="col-md-6">
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
                                msgList.length == 0 ? "..." : msgList.map(x => x.message).join('\n')
                            }
                        </pre>
                    </div>
                </div>
            </div>
        </div >
    )
}

export default App;