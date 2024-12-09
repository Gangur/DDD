import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import ProductList from "./ProductList";
import { useEffect } from "react";
import { fetchFiltersAsync, fetchProductsAsync, productSelectors } from "./catalogSlice";
import { Box, Checkbox, FormControl, FormControlLabel, FormGroup, Grid, Pagination, Paper, Radio, RadioGroup, TextField, Typography } from "@mui/material";

const sortOptions = [
    { name: 'Alphabetical', order: 'Name', descending: false },
    { name: 'Price: Hight to low', order: 'Price', descending: false },
    { name: 'Price: Low to hight', order: 'Price', descending: true },
]

export default function Ctatalog() {
    const products = useAppSelector(productSelectors.selectAll);
    const { productsLoaded, filtersLoaded, status, categories, brands } = useAppSelector(state => state.catalog)
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
            <Grid container sx={{ mb: 2 }} spacing={4}>
                <Grid item xs={3}>
                    <Paper sx={{ mb: 2 }}>
                        <TextField
                            label='Search products'
                            variant='outlined'
                            fullWidth
                        />
                    </Paper>
                    <Paper sx={{mb: 2, p: 2}}>
                        <FormControl>
                            <RadioGroup>
                                {sortOptions.map(({ name }) => (
                                    <FormControlLabel key={name} value={name} control={<Radio />} label={name} />
                                ))}
                            </RadioGroup>
                        </FormControl>
                    </Paper>
                    <Paper sx={{ mb: 2, p: 2 }}>
                        <FormGroup>
                            {brands.map(brand => (
                                <FormControlLabel key={brand} control={<Checkbox />} label={brand} />
                            ))}
                        </FormGroup>
                    </Paper>
                    <Paper sx={{ mb: 2, p: 2 }}>
                        <FormGroup>
                            {categories.map(category => (
                                <FormControlLabel key={category} control={<Checkbox />} label={category} />
                            ))}
                        </FormGroup>
                    </Paper>
                </Grid>
                <Grid item xs={9}>
                    <ProductList products={products} />
                </Grid>
                <Grid item xs={3} />
                <Grid item xs={9}>
                    <Box display='flex' justifyContent='space-between' alignItems='center'>
                        <Typography>
                            Displaying 1-6 of 20 items
                        </Typography>
                        <Pagination
                            color='secondary'
                            size='large'
                            count={10}
                            page={1}
                        />
                    </Box>
                </Grid>
            </Grid>
        )
    );
}