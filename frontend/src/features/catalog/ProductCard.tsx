import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { Product } from "../../app/models/project";

interface Props {
    product: Product;
}

export default function ProductCard({ product }: Props) {
    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardMedia
                sx={{ height: 140 }}
                image={"https://dddstoragedemo.blob.core.windows.net/pictures-ddd-container/" + product.pictureName}
                title={product.name}
            />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    { product.name }
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    {product.name}
                </Typography>
            </CardContent>
            <CardActions>
                <Button size="small">Add to the order</Button>
                <Button size="small">Buy in one click</Button>
            </CardActions>
        </Card>
    )
}