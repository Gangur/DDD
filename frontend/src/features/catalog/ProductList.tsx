import { Grid, List } from "@mui/material";
import ProductCard from "./ProductCard";
import { ProductDto } from "../../app/api/http-client";
import ProductCardSkeleton from "./ProductCardSkeleton";
import { useFetchProductsQuery } from "./catalogApi";

interface Props {
    products: ProductDto[];
}

export default function ProductList({ products }: Props) {
    const { isLoading } = useFetchProductsQuery();
    return (
        <List>
            <Grid container spacing={4}>
                {products.map(product =>
                (
                    <Grid item xs={4} key={product.id}>
                        {isLoading ? (
                            <ProductCardSkeleton />
                        ) : (<ProductCard product={product} key={product.id} />)}
                    </Grid>
                ))}
            </Grid>
        </List>
    )
}