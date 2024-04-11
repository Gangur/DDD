import { Avatar, Button, Card, CardActions, CardContent, CardHeader, CardMedia, Typography } from "@mui/material";
import { Product } from "../../app/models/project";

interface Props {
    product: Product;
}

export default function ProductCard({ product }: Props) {
    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardHeader
                avatar={
                    <Avatar>
                        {product.name.charAt(0).toUpperCase()}
                    </Avatar>
                }
                title={product.name}
                titleTypographyProps={{
                    sx: { fontWeight: 'bold', color: 'primary.main' }
                }}
            />
            <CardMedia
                sx={{ height: 140, backgroundSize: 'contain' }}
                image={"https://dddstoragedemo.blob.core.windows.net/pictures-ddd-container/" + product.pictureName}
                title={product.name}
            />
            <CardContent>
                <Typography gutterBottom color='secondary' variant="h5">
                    {(product.priceAmount / 100).toFixed(2) + ' ' + product.priceCurrency}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    {product.name}
                </Typography>
            </CardContent>
            <CardActions>
                <Button size="small">Add to the order</Button>
                <Button size="small">View</Button>
            </CardActions>
        </Card>
    )
}