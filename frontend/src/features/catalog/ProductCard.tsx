import { Avatar, Button, Card, CardActions, CardContent, CardHeader, CardMedia, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import PictureUrl from "../../tools/pictures-url-factory";
import DisplayPrice from "../../tools/PriceFactory";
import { ProductDto } from "../../app/api/http-client";


interface Props {
    product: ProductDto;
}

export default function ProductCard({ product }: Props) {
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
                <Button size="small">Add to the order</Button>
                <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
            </CardActions>
        </Card>
    )
}