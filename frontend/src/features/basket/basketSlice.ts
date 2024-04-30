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
        }
    }
});

export const { setBasket } = basketSlice.actions;
