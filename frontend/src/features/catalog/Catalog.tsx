import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import ProductList from "./ProductList";
import { useEffect } from "react";
import { fetchFiltersAsync, fetchProductsAsync, productSelectors } from "./catalogSlice";

export default function Ctatalog() {
    const products = useAppSelector(productSelectors.selectAll);
    const { productsLoaded, filtersLoaded, status } = useAppSelector(state => state.catalog)
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!productsLoaded) dispatch(fetchProductsAsync())
    }, [productsLoaded, dispatch]);

    useEffect(() => {
        if (!filtersLoaded) dispatch(fetchFiltersAsync())
    }, [filtersLoaded, dispatch])

    if (status.includes('pending')) return <LoadingComponent />

    return (
        (
            <>
                <ProductList products={products} />
            </>
        )
    );
}