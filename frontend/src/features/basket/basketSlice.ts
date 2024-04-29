import { createSlice } from "@reduxjs/toolkit";
import { OrderDto } from "../../app/api/http-client";

interface BasketState {
    basket: OrderDto | undefined
}

const initialState: BasketState = {
    basket: undefined
}

export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload
        },
        removeItem: (state, action) => {
            const { productId, quantity } = action.payload;
            const itemIndex = state.basket?.lineItems!.findIndex(i => i.productId === productId)
            if (itemIndex === -1 || itemIndex == undefined)
                return;

            state.basket!.lineItems![itemIndex].quantity! -= quantity;

            if (state.basket!.lineItems![itemIndex].quantity! <= 0)
                state.basket!.lineItems!.splice(itemIndex, 1);
                
        }
    }
});

export const { setBasket, removeItem } = basketSlice.actions;
