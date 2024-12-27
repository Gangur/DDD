import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import ProductList from "./ProductList";
import { useEffect } from "react";
import { fetchFiltersAsync, setPageNumer, setProductParams } from "./catalogSlice";
import { Grid, Paper } from "@mui/material";
import ProductSearch from "./ProductSearch";
import RadiooButtonGroup from "../../app/components/RadioButtonGroup";
import CheckboxButtons from "../../app/components/CheckBoxButtons";
import AppPagination from "../../app/components/AppPagination";
import { useFetchProductsQuery } from "./catalogApi";
import LoadingComponent from "../../app/layout/LoadingComponent";

const sortOptions = [
    { name: 'Alphabetical', order: 'Name', descending: false },
    { name: 'Price: Hight to low', order: 'Price', descending: true },
    { name: 'Price: Low to hight', order: 'Price', descending: false },
]

export default function Ctatalog() {
    //const products = useAppSelector(productSelectors.selectAll);
    const { filtersLoaded, categories, brands, productParams, productsTotal } = useAppSelector(state => state.catalog)
    const dispatch = useAppDispatch();

    const { data, isLoading } = useFetchProductsQuery();

    //useEffect(() => {
    //    if (!productsLoaded) dispatch(fetchProductsAsync())
    //}, [productsLoaded, dispatch]);

    useEffect(() => {
        if (!filtersLoaded) dispatch(fetchFiltersAsync())
    }, [filtersLoaded, dispatch])

    if (isLoading) return <LoadingComponent />

    let pagesCount = productsTotal / productParams.pageSize;
    if (pagesCount % 1 > 0)
    {
        pagesCount++;
        pagesCount = Math.floor(pagesCount);
    }

    return (
            <Grid container sx={{ mb: 2 }} spacing={4}>
                <Grid item xs={3}>
                    <Paper sx={{ mb: 2 }}>
                        <ProductSearch />
                    </Paper>
                    <Paper sx={{mb: 2, p: 2}}>
                        <RadiooButtonGroup 
                            selectedValue={sortOptions.find(x => x.order === productParams.orderBy && x.descending === productParams.descending)?.name || ''}
                            options={sortOptions}
                            onChange={(event: any) => {
                                const selected = sortOptions.find(x => x.name == event.target.value);
                                dispatch(setProductParams(
                                    {
                                        orderBy: selected?.order, 
                                        descending: selected?.descending
                                    }))
                            }}
                        />
                    </Paper>
                    <Paper sx={{ mb: 2, p: 2 }}>
                        <CheckboxButtons
                            items={brands}
                            checked={productParams.brands}
                            onChange={(items: string[]) => dispatch(setProductParams({brands: items}))}
                        />
                    </Paper>
                    <Paper sx={{ mb: 2, p: 2 }}>
                        <CheckboxButtons
                            items={categories}
                            checked={productParams.categories}
                            onChange={(items: string[]) => dispatch(setProductParams({categories: items}))}
                        />
                    </Paper>
                </Grid>
                <Grid item xs={9}>
                    <ProductList products={data?.values || []} />
                </Grid>
                <Grid item xs={3} />
                <Grid item xs={9}>
                    <AppPagination 
                        productParams={productParams}
                        productsTotal={productsTotal}
                        pagesCount={pagesCount}
                        onPageChange={(page: number) => dispatch(setPageNumer({pageNumber: page}))}
                    />
                </Grid>
            </Grid>
    );
}