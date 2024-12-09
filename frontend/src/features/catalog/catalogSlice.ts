import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { Category, ProductDto, ProductDtoListResultDto } from "../../app/api/http-client";
import agent from "../../app/api/agent";
import { ProductParams } from "../../app/models/product-params";
import { RootState } from "../../app/store/configureStore";

const productsAdapter = createEntityAdapter({
    selectId: (entity: ProductDto) => entity.id!,
    sortComparer: (a, b) => a.id!.localeCompare(b.id!),
  });

interface CatalogState {
    productsLoaded: boolean; 
    filtersLoaded: boolean;
    status: string;
    brands: string[];
    categories: string[];
    productParams: ProductParams;
}

const _defaultState = 'idle';

export const fetchProductsAsync = createAsyncThunk<ProductDtoListResultDto, void, { state: RootState }>(
    'catalog/fetchProductsAsync',
    async (_, thunkApi) => {
        const params = thunkApi.getState().catalog.productParams;

        try {
            const result = await agent
                .products.list(params.category,
                    params.brand,
                    params.searchTerm,
                    params.orderBy,
                    params.descending, 
                    params.pageNumber, 
                    params.pageSize);

            return result;
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
            return await agent.products.get(productId);
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
            return await agent.products.listBrands();
        }
        catch (error) {
            return thunkAPI.rejectWithValue({ error: error })
        }
    }
)

function initParams() {
    return {
        pageNumber: 1,
        pageSize: 10,
        orderBy: 'Name',
        descending: false
    }
}

export const catalogSlice = createSlice({
    name: 'catalog',
    initialState: productsAdapter.getInitialState<CatalogState>({
        productsLoaded: false,
        filtersLoaded: false,
        status: _defaultState,
        brands: [],
        categories: [],
        productParams: initParams()
    }),
    reducers: {
        setProductParams: (state, action) => {
            state.productsLoaded = false;
            state.productParams = {
                ...state.productParams,
                ...action.payload
            }
        },
        resetProductParams: (state) => {
            state.productParams = initParams(); 
        }
    },
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

export const productSelectors = productsAdapter.getSelectors((state: RootState) => state.catalog);
export const { setProductParams, resetProductParams } = catalogSlice.actions;
