import { Grid, List } from "@mui/material";
import ProductCard from "./ProductCard";
import { ProductDto } from "../../app/api/http-client";
import { useAppSelector } from "../../app/store/configureStore";
import ProductCardSkeleton from "./ProductCardSkeleton";

interface Props {
    products: ProductDto[];
}

export default function ProductList({ products }: Props) {
    const { productsLoaded } = useAppSelector(state => state.catalog) 
    return (
        <List>
            <Grid container spacing={4}>
                {products.map(product =>
                (
                    <Grid item xs={4} key={product.id}>
                        {!productsLoaded ? (
                            <ProductCardSkeleton />
                        ) : (<ProductCard product={product} key={product.id} />)}
                    </Grid>
                ))}
            </Grid>
        </List>
    )
}