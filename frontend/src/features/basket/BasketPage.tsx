import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Box, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import DisplayPrice from "../../tools/price-factory";
import { Add, Delete, Remove } from "@mui/icons-material";
import { getCustomerId } from "../../tools/cookies";
import PictureUrl from "../../tools/pictures-url-factory";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setBasket } from "./basketSlice";
import { LoadingButton } from "@mui/lab";

export default function BasketPage() {
    const { basket } = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);
    const [loadingButton, setLoadinggButton] = useState(false);

    function handleAddItem(productId: string) {
        setLoadinggButton(true);
        agent.v1OrdersAddLineItem(basket!.id!, productId)
            .then(order => dispatch(setBasket(order)))
            .catch(error => console.log(error))
            .finally(() => setLoadinggButton(false))
    }

    function handleRemoveItem(productId: string, quantity: number = 1) {
        setLoadinggButton(true);
        agent.v1OrdersRemoveLineItem(basket!.id!, productId, quantity)
            .then(order => dispatch(setBasket(order)))
            .catch(error => console.log(error))
            .finally(() => setLoadinggButton(false))
    }

    
    useEffect(() => {
        const customerId = getCustomerId();
        agent.v1OrdersByCustomer(customerId!)
            .then(order => setBasket(order))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <LoadingComponent />

    if (!basket) return <Typography variant='h3'>Your basket is empty</Typography>

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Product</TableCell>
                        <TableCell align="right">Price</TableCell>
                        <TableCell align="center">Quantity</TableCell>
                        <TableCell align="right">Subtotal</TableCell>
                        <TableCell align="right"></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {basket.lineItems!.map(item => (
                        <TableRow
                            key={item.productId}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                <Box display='flex' alignContent='center'>
                                    <img src={PictureUrl(item.pictureName!)} alt={item.productName} style={{ height: 50, marginRight: 20 }} />
                                    <span>{item.productName}</span>
                                </Box>
                            </TableCell>
                            <TableCell align="right">{DisplayPrice(item.priceAmount, item.priceCurrency)}</TableCell>
                            <TableCell align="right">
                                <LoadingButton loading={loadingButton} onClick={() => handleRemoveItem(item.productId!)} color='error'>
                                    <Remove/>
                                </LoadingButton>
                                {item.quantity}
                                <LoadingButton loading={loadingButton} onClick={() => handleAddItem(item.productId!)} color='secondary'>
                                    <Add/>
                                </LoadingButton>
                            </TableCell>
                            <TableCell align="right">{DisplayPrice(item.priceAmount! * item.quantity!, item.priceCurrency)}</TableCell>
                            <TableCell align="right">
                                <LoadingButton loading={loadingButton} onClick={() => handleRemoveItem(item.productId!, item.quantity)} color='error'>
                                    <Delete />
                                </LoadingButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}