import { createApi } from "@reduxjs/toolkit/query/react";
import { ProductDto, ProductDtoListResultDto } from "../../app/api/http-client";
import { baseQueryWirhErrorHandling } from "../../app/api/baseApi";

export const catalogApi = createApi({
    reducerPath: 'catalogApi',
    baseQuery: baseQueryWirhErrorHandling,
    endpoints: (build) => ({
        fetchProducts: build.query<ProductDtoListResultDto, void>({
            query: () => ({url: 'products/list'})
        }),
        fetchProductDetails: build.query<ProductDto, string>({
            query: (productId) => `products/get/${productId}`
        })
    })
});

export const {
    useFetchProductDetailsQuery, 
    useFetchProductsQuery
} = catalogApi;