import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { OrderDto } from "../../app/api/http-client";
import agent from "../../app/api/agent";

interface BasketState {
    basket: OrderDto | undefined
    status: string
}

function setDefaultStatus(state: BasketState) {
    state.status = 'idle';
}

const initialState: BasketState = {
    basket: undefined,
    status: 'idle'
}

export const addBasketItemAsync = createAsyncThunk<OrderDto | undefined,
    {
        orderId: string,
        productId: string,
        quantity: number
    }>(
        'basket/addBasketItemAsync',
        async ({ orderId, productId, quantity }, thunkApi) => {
            try {
                return await agent.orders.addLineItem(orderId, productId, quantity)
            }
            catch (error: any) {
                return thunkApi.rejectWithValue({ error: error });
            }
        }
    )

export const removeBasketItemAsync = createAsyncThunk<OrderDto | undefined,
    {
        orderId: string,
        productId: string,
        quantity: number
    }>(
        'basket/removeBasketItemAsync',
        async ({ orderId, productId, quantity }, thunkApi) => {
            try {
                return await agent.orders.removeLineItem(orderId, productId, quantity)
            }
            catch (error: any) {
                return thunkApi.rejectWithValue({ error: error });
            }
        }
    )

export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload
        }
    },
    extraReducers: (builder => {
        builder.addCase(addBasketItemAsync.pending, (state, action) => {
            console.log(action);
            state.status = 'pendingAddItem-' + action.meta.arg.productId;
        });
        builder.addCase(addBasketItemAsync.fulfilled, (state, action) => {
            state.basket = action.payload;
            setDefaultStatus(state);
        });
        builder.addCase(addBasketItemAsync.rejected, (state, action) => {
            console.log(action);
            setDefaultStatus(state);
        });

        builder.addCase(removeBasketItemAsync.pending, (state, action) => {
            console.log(action);
            state.status = 'pendingRemoveItem-' + action.meta.arg.productId;
        });
        builder.addCase(removeBasketItemAsync.fulfilled, (state, action) => {
            state.basket = action.payload;
            setDefaultStatus(state);
        });
        builder.addCase(removeBasketItemAsync.rejected, (state, action) => {
            console.log(action);
            setDefaultStatus(state);
        });
    })
});

export const { setBasket } = basketSlice.actions;
