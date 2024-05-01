import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { ProductDto } from "../../app/api/http-client";
import agent from "../../app/api/agent";
import { RootState } from "@reduxjs/toolkit/query";

const productsAdapter = createEntityAdapter<ProductDto>();

export const fetchProductsAsync = createAsyncThunk<ProductDto[]>(
    'catalog/fetchProductsAsync',
     async (_, thunkApi) => {
        try {
            return await agent.v1ProductsList();
        }
        catch (error: any) {
            return thunkApi.rejectWithValue({ error: error });
        }
    }
)

export const fetchProductAsync = createAsyncThunk<ProductDto, string>(
    'catalog/fetchProductAsync',
    async (productId, thunkApi) => {
        try {
            return await agent.v1Products(productId);
        }
        catch (error: any) {
            return thunkApi.rejectWithValue({ error: error });
        }
    }
)

export const catalogSlice = createSlice({
    name: 'catalog',
    initialState: productsAdapter.getInitialState({
        productsLoaded: false,
        status: 'idle'
    }),
    reducers: {},
    extraReducers: (builder => {
        builder.addCase(fetchProductsAsync.pending, (state) => {
            state.status = 'pendingFetchProducts'
        });
        builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
            productsAdapter.setAll(state, action.payload);
            state.status = 'idle';
            state.productsLoaded = true;
        });
        builder.addCase(fetchProductsAsync.rejected, (state, action) => {
            console.log(action);
            state.status = 'idle';
        });

        builder.addCase(fetchProductAsync.pending, (state) => {
            state.status = 'pendingFetchProduct'
        });
        builder.addCase(fetchProductAsync.fulfilled, (state,     action) => {
            productsAdapter.upsertOne(state, action.payload);
            state.status = 'idle';
        });
        builder.addCase(fetchProductAsync.rejected, (state, action) => {
            console.log(action);
            state.status = 'idle';
        });
    })
});

export const productSelectors = productsAdapter.getSelectors((state: RootState) => state.catalog)