import { CButton, CCard, CCardBody, CCardFooter, CCardHeader, CForm, CFormInput, CFormLabel, CFormSelect, CFormTextarea } from "@coreui/react";
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import { ToastContainer, toast } from 'react-toastify';


const CreateOrder = () => {
    const [howManyProducts, setHowManyProducts] = useState(0);
    const [productType, setProductType] = useState('');
    const [accountHubConnection, setAccountHubConnection] = useState<HubConnection | undefined>(undefined);
    const auth = useSelector((state: RootState) => state.auth)

    useEffect(() => {

        const startConnection = async () => {

            const connection = new HubConnectionBuilder()
                .withUrl(`https://localhost:7172/Hubs/TriggerHub`)
                .withAutomaticReconnect()
                .build();

            await connection.start();

            setAccountHubConnection(connection);
        }

        if (!accountHubConnection) {
            startConnection();
        }


    }, [])

    const createOrder = async () => {
        try {
            if (!howManyProducts || howManyProducts <= 0 || !productType) {
                toast("Please fill all fields.");
                return;
            }

            await accountHubConnection?.invoke<string>("CrawlerTriggerAsync", {
                productCount: howManyProducts,
                productType: productType,
                token: auth.user?.accessToken
            });

            toast("Order created.")
        }
        catch (err) {
            toast("An error occured.")
        }
    }

    return (
        <CCard>
            <CCardHeader>
                Create Order
            </CCardHeader>
            <CCardBody>
                <div className="mb-3">
                    <CFormLabel htmlFor="exampleFormControlInput1">How many products do you want to crawl?</CFormLabel>
                    <CFormInput onChange={(e) => setHowManyProducts(parseInt(e.target.value))} value={howManyProducts} type="number" />
                </div>

                <CFormLabel htmlFor="exampleFormControlInput1">What type of products you want to crawl?</CFormLabel>
                <CFormSelect onChange={(e) => setProductType(e.target.value)} aria-label="Default select example">
                    <option value="">Select</option>
                    <option value="A">All</option>
                    <option value="B">On Sale</option>
                    <option value="C">Not On Sale</option>
                </CFormSelect>
            </CCardBody>
            <CCardFooter>
                <CButton onClick={createOrder}>Create Order</CButton>
            </CCardFooter>
        </CCard>
    )
}

export default CreateOrder;