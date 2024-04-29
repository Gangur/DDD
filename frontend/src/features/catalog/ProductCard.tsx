import { Avatar, Button, Card, CardActions, CardContent, CardHeader, CardMedia, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import PictureUrl from "../../tools/pictures-url-factory";
import { ProductDto } from "../../app/api/http-client";
import DisplayPrice from "../../tools/price-factory";
import { useState } from "react";
import agent from "../../app/api/agent";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setBasket } from "../basket/basketSlice";


interface Props {
    product: ProductDto;
}

export default function ProductCard({ product }: Props) {
    const [loading, setLoading] = useState(false);
    const dispatch = useAppDispatch();
    const basket = useAppSelector(state => state.basket.basket);

    function handleAddItem(productId: string) {
        setLoading(true);

        agent.v1OrdersAddLineItem(basket!.id!, productId)
            .then(order => dispatch(setBasket(order)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }

    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardHeader
                avatar={
                    <Avatar>
                        {product.name!.charAt(0).toUpperCase()}
                    </Avatar>
                }
                title={product.name}
                titleTypographyProps={{
                    sx: { fontWeight: 'bold', color: 'primary.main' }
                }}
            />
            <CardMedia
                sx={{ height: 140, backgroundSize: 'contain' }}
                image={PictureUrl(product.pictureName!)}
                title={product.name}
            />
            <CardContent>
                <Typography gutterBottom color='secondary' variant="h5">
                    {DisplayPrice(product.priceAmount, product.priceCurrency)}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    {product.name}
                </Typography>
            </CardContent>
            <CardActions>
                <LoadingButton loading={loading} onClick={() => handleAddItem(product.id!)} size="small">Add to the order</LoadingButton>
                <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
            </CardActions>
        </Card>
    )
}