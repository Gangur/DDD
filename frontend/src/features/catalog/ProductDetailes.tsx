import { Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, TextField, Typography } from "@mui/material";
import { ChangeEvent, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import PictureUrl from "../../tools/pictures-url-factory";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import DisplayPrice from "../../tools/price-factory";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { LoadingButton } from "@mui/lab";
import { addBasketItemAsync, removeBasketItemAsync } from "../basket/basketSlice";
import { fetchProductAsync } from "./catalogSlice";
import { useFetchProductDetailsQuery } from "./catalogApi";

export default function ProductDetailes() {
    const { id } = useParams<{ id: string }>();
    const dispatch = useAppDispatch();
    const { basket, status } = useAppSelector(state => state.basket);
    const { data: product, isLoading} = useFetchProductDetailsQuery(id!);

    const [quantity, setQuantity] = useState(0);
    const item = basket?.lineItems?.find(li => li.productId == product?.id)

    useEffect(() => {
        if (item) setQuantity(item.quantity!);
        if (!product && id) dispatch(fetchProductAsync(id))
        
    }, [id, item, dispatch, product]);

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const value = parseInt(event.currentTarget.value);
        if (value > 0) {
            setQuantity(value);
        }
    }

    function handleUpdateCart() {
        if (!item || quantity > item.quantity!) {
            const quantityDif = item ? quantity - item.quantity! : quantity;

            dispatch(addBasketItemAsync({
                orderId: basket!.id!,
                productId: product!.id!,
                quantity: quantityDif
            }));
        }
        else {
            const quantityDif = item.quantity! - quantity;

            dispatch(removeBasketItemAsync({
                orderId: basket!.id!,
                productId: product!.id!,
                quantity: quantityDif
            }));
        }
    }

    if (isLoading)
    {
        return (<LoadingComponent />)
    }

    if (!product) {
        return <NotFound />
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
                            loading={status.includes('pending')}
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