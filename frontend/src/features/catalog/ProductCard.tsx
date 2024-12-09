import { Avatar, Button, Card, CardActions, CardContent, CardHeader, CardMedia, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import PictureUrl from "../../tools/pictures-url-factory";
import { ProductDto } from "../../app/api/http-client";
import DisplayPrice from "../../tools/price-factory";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync } from "../basket/basketSlice";

interface Props {
    product: ProductDto;
}

export default function ProductCard({ product }: Props) {
    const dispatch = useAppDispatch();
    const { basket, status } = useAppSelector(state => state.basket);

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
                <LoadingButton
                    loading={status === 'pendingAddItem-' + product.id}
                    onClick={() => dispatch(addBasketItemAsync({ orderId: basket!.id!, productId: product.id!, quantity: 1 }))}
                    size="small">Add to the order</LoadingButton>
                <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
            </CardActions>
        </Card>
    )
}