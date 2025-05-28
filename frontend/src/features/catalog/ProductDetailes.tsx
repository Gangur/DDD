import { Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, TextField, Typography } from "@mui/material";
import { useParams } from "react-router-dom";
import PictureUrl from "../../tools/pictures-url-factory";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import DisplayPrice from "../../tools/price-factory";
import { LoadingButton } from "@mui/lab";
import { useFetchProductDetailsQuery } from "./catalogApi";
import { useAddBasketItemMutation, useFetchBasketQuery, useRemoveBasketItemMutation } from "../basket/basketApi";
import { getCookieCustomerId } from "../../tools/cookies";
import { ChangeEvent, useEffect, useState } from "react";

export default function ProductDetailes() {
    const { id } = useParams<{ id: string }>();
    const { data: product, isLoading } = useFetchProductDetailsQuery(id || '');
    const { data: basket } = useFetchBasketQuery(getCookieCustomerId());
    const item = basket?.lineItems?.find(i => i.productId === id);
    const [quantity, setQuantity] = useState(0);

    const [removeBasketItem, { isLoading: isLoadingRemoveBasketItem}] = useRemoveBasketItemMutation();
    const [addBasketItem, { isLoading: isLoadingAddBasketItem}] = useAddBasketItemMutation();

    useEffect(() => {
        if (item) 
            setQuantity(item.quantity);
    }, [item])

    if (isLoading)
    {
        return (<LoadingComponent />)
    }

    if (!product) {
        return <NotFound />
    }

    const handleUpdateCart = () => {
        const updatedQuantity = item ? Math.abs(quantity - item.quantity) : quantity;
        if (!item || quantity > item.quantity) {
            addBasketItem({
                orderId: basket!.id, 
                productId: product.id,
                quantity: updatedQuantity});
        }
        else {
            removeBasketItem({
                orderId: basket!.id,
                productId: product.id,
                quantity: updatedQuantity
            });
        }
    }

    const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        const value = +event.currentTarget.value;

        if (value >= 0) setQuantity(value); 
    }

    return (
        <Grid container spacing={6}>
            <Grid item xs={6}>
                <img src={PictureUrl(product.pictureName!)} style={{ width: '100%', marginTop: '42pt' }}></img>
            </Grid>
            <Grid item xs={6}>
                <Typography variant='h3'>{product.name}</Typography>
                <Divider sx={{ mb: 2 }} />
                <Typography variant='h4' color='secondary'>{DisplayPrice(product.priceAmount, product.priceCurrency)}</Typography>
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>{product.name}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Description</TableCell>
                                <TableCell></TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Type</TableCell>
                                <TableCell>{product.category}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Brand</TableCell>
                                <TableCell>{product.brand}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Quantity in stock</TableCell>
                                <TableCell>{product.sku}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
                <Grid mt={2} container spacing={2}>
                    <Grid item xs={6}>
                        <TextField
                            onChange={handleInputChange}
                            variant='outlined'
                            type='number'
                            label='Quantity in Cart'
                            fullWidth
                            value={quantity}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <LoadingButton
                            disabled={item?.quantity === quantity || !item && quantity === 0}
                            loading={isLoadingAddBasketItem || isLoadingRemoveBasketItem}
                            onClick={handleUpdateCart}
                            sx={{ height: '55px' }}
                            color='primary'
                            size='large'
                            variant='contained'
                            fullWidth
                        >
                            {item ? 'Update Quantity' : 'Add to Cart'}
                        </LoadingButton>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>)
}