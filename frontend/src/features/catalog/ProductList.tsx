import { Grid, List } from "@mui/material";
import { Product } from "../../app/models/project";
import ProductCard from "./ProductCard";

interface Props {
    products: Product[];
}

export default function ProductList({ products }: Props) {
    return (
        <List>
            <Grid container spacing={4}>
                {products.map(product =>
                (
                    <Grid item xs={3} key={product.id}>
                        <ProductCard product={product} />
                    </Grid>
                ))}
            </Grid>
        </List>
    )
}