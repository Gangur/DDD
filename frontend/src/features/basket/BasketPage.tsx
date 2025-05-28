import { Box, Button, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import DisplayPrice from "../../tools/price-factory";
import { Add, Delete, Remove } from "@mui/icons-material";
import PictureUrl from "../../tools/pictures-url-factory";
import { LoadingButton } from "@mui/lab";
import BasketSummary from "./BasketSummary";
import { Link } from "react-router-dom";
import { useAddBasketItemMutation, useFetchBasketQuery, useRemoveBasketItemMutation } from "./basketApi";
import { getCookieCustomerId } from "../../tools/cookies";

export default function BasketPage() {
    const {data: basket, isLoading} = useFetchBasketQuery(getCookieCustomerId());

    const [addBasketItem, { isLoading: isLoadingAddBasketItem}] = useAddBasketItemMutation();
    const [removeBasketItem, { isLoading: isLoadingRemoveBasketItem}] = useRemoveBasketItemMutation();

    if (isLoading) 
        return  <Typography variant='h3'>Loading basket...</Typography>

    if (!basket || basket.lineItems?.length == 0) 
        return <Typography variant='h3'>Your basket is empty</Typography>

    const total = basket.lineItems!.reduce((sum, item) => {
        let local = item.priceAmount! * item.quantity!;
        if (item.priceCurrency == 'EUR')
            local *= 1.1;

        return sum + local;
    }, 0);

    return (
        <>
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
                                    <LoadingButton
                                        loading={isLoadingRemoveBasketItem}
                                        onClick={() => removeBasketItem({
                                            orderId: basket.id!,
                                            productId: item.productId!,
                                            quantity: 1
                                        })}
                                        color='error'>
                                        <Remove />
                                    </LoadingButton>
                                    {item.quantity}
                                    <LoadingButton
                                        loading={isLoadingAddBasketItem}
                                        onClick={() => addBasketItem({
                                            orderId: basket.id!,
                                            productId: item.productId!,
                                            quantity: 1
                                        })}
                                        color='secondary'>
                                        <Add />
                                    </LoadingButton>
                                </TableCell>
                                <TableCell align="right">{DisplayPrice(item.priceAmount! * item.quantity!, item.priceCurrency)}</TableCell>
                                <TableCell align="right">
                                    <LoadingButton
                                        loading={isLoadingRemoveBasketItem} 
                                        onClick={() => removeBasketItem({
                                            orderId: basket.id!,
                                            productId: item.productId!,
                                            quantity: item.quantity!
                                        })} 
                                        color='error'>
                                        <Delete />
                                    </LoadingButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Grid container>
                <Grid item xs={6} />
                <Grid item xs={6} >
                    <BasketSummary subtotal={total} />
                    <Button
                        component={Link}
                        to='/checkout'
                        variant='contained'
                        size='large'
                        fullWidth
                    >
                        Checkout
                    </Button>
                </Grid>
            </Grid>
        </>
    );
}