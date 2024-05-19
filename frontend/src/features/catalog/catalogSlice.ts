import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { Category, ProductDto, ProductDtoListResultDto } from "../../app/api/http-client";
import agent from "../../app/api/agent";
import { RootState } from "@reduxjs/toolkit/query";

const productsAdapter = createEntityAdapter<ProductDto>();

const _defaultState = 'idle';

export const fetchProductsAsync = createAsyncThunk<ProductDtoListResultDto>(
    'catalog/fetchProductsAsync',
     async (_, thunkApi) => {
         try {
             return await agent.v1ProductsList(undefined, undefined, undefined, undefined, false, 1, 10);
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

export const fetchFiltersAsync = createAsyncThunk(
    'catalog/fetchFilters',
    async (_, thunkAPI) => {
        try {
            return await agent.v1ProductsListBrands();
        }
        catch (error) {
            return thunkAPI.rejectWithValue({ error: error })
        }
    }
)

export const catalogSlice = createSlice({
    name: 'catalog',
    initialState: productsAdapter.getInitialState({
        productsLoaded: false,
        filtersLoaded: false,
        status: _defaultState,
        brands: [] as string[],
        categories: [] as string[]
    }),
    reducers: {},
    extraReducers: (builder => {
        builder.addCase(fetchProductsAsync.pending, (state) => {
            state.status = 'pendingFetchProducts'
        });
        builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
            productsAdapter.setAll(state, action.payload.values!);
            state.status = _defaultState;
            state.productsLoaded = true;
        });
        builder.addCase(fetchProductsAsync.rejected, (state, action) => {
            console.log(action);
            state.status = _defaultState;
        });

        builder.addCase(fetchProductAsync.pending, (state) => {
            state.status = 'pendingFetchProduct'
        });
        builder.addCase(fetchProductAsync.fulfilled, (state,     action) => {
            productsAdapter.upsertOne(state, action.payload);
            state.status = _defaultState;
        });
        builder.addCase(fetchProductAsync.rejected, (state, action) => {
            console.log(action);
            state.status = _defaultState;
        });
        builder.addCase(fetchFiltersAsync.pending, (state) => {
            state.status = 'pendingFetchFilters';
        });
        builder.addCase(fetchFiltersAsync.fulfilled, (state, action) => {
            state.brands = action.payload;
            state.categories = [
                Category.Phones,
                Category.Books,
                Category.Tablets
            ];
            state.filtersLoaded = true;
            state.status = _defaultState;
        });
        builder.addCase(fetchFiltersAsync.rejected, (state, action) => {
            console.log(action);
            state.status = _defaultState;
        });
    })
});

export const productSelectors = productsAdapter.getSelectors((state: RootState) => state.catalog)