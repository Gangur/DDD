import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWirhErrorHandling } from "../../app/api/baseApi";
import { OrderDto } from "../../app/api/http-client"; 
import { ensureIDsExistence } from "../../tools/cookies";

export const basketTag = 'BasketTag';

export const basketApi = createApi({
    reducerPath: 'basketApi',
    baseQuery: baseQueryWirhErrorHandling,
    tagTypes: [basketTag],
    endpoints: (builder) => ({
        fetchBasket: builder.query<OrderDto, string>({
            query: (customerId) => `/orders/by-customer/${customerId}`,
            providesTags: [basketTag],
            onQueryStarted: async (customerId, { queryFulfilled} ) => {
                if (!customerId) {
                    customerId = await ensureIDsExistence();
                }

                try {
                    await queryFulfilled;
                }
                catch  (error) {
                    console.error(error);
                }
            }
        }),
        addBasketItem: builder.mutation<OrderDto, { orderId: string, productId: string, quantity: number }>({
            query: ({ orderId, productId, quantity }) => ({
                url: `/orders/add-line-item/${orderId}/${productId}?quantity=${quantity}`,
                method: 'POST'
            }),
            onQueryStarted: async (_, {dispatch, queryFulfilled}) => {
                try {
                    await queryFulfilled;
                    dispatch(basketApi.util.invalidateTags([basketTag]));
                }
                catch (error) { 
                    console.error(error);
                }
            }
        }),
        removeBasketItem: builder.mutation<void,  { orderId: string, productId: string, quantity: number }>({
            query: ({ orderId, productId, quantity }) => ({
                url: `/orders/remove-line-item/${orderId}/${productId}?quantity=${quantity}`,
                method: 'DELETE'
            }),
            onQueryStarted: async (_, {dispatch, queryFulfilled}) => {
                try {
                    await queryFulfilled;
                    dispatch(basketApi.util.invalidateTags([basketTag]));
                }
                catch (error) { 
                    console.error(error);
                }
            }
        })
    })
});

export const {
    useFetchBasketQuery,
    useAddBasketItemMutation,
    useRemoveBasketItemMutation
} = basketApi;

