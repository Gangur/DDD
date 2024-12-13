import { Box, Pagination, Typography } from "@mui/material";
import { ProductParams } from "../models/product-params";

interface Props {
    productParams: ProductParams,
    productsTotal: number;
    pagesCount: number;
    onPageChange: (page: number) => void;
}

export default function AppPagination({ productParams, productsTotal, pagesCount, onPageChange }: Props) {
    return <Box display='flex' justifyContent='space-between' alignItems='center'>
        <Typography>
            Displaying {(productParams.pageNumber - 1) * productParams.pageSize + 1}-
            {productParams.pageNumber * productParams.pageSize > productsTotal 
                ? productsTotal : productParams.pageNumber * productParams.pageSize } of {productsTotal} items
        </Typography>
        <Pagination
            color='secondary'
            size='large'
            count={pagesCount}
            page={productParams.pageNumber}
            onChange={(_, page) => onPageChange(page)}
        />
    </Box>
}