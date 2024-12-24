import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import { LoginDto, UserDto } from "../../app/api/http-client";
import { FieldValues } from "react-hook-form";
import agent from "../../app/api/agent";
import { router } from "../../app/router/Routes";

interface AccountState {
    user: UserDto | undefined;
}

const initialState: AccountState = {
    user: undefined
}

export const signInUser = createAsyncThunk<UserDto | undefined, FieldValues>(
    'account/signInUser',
    async (data, thunkAPI) => {
        try {
            const user = await agent.auth.login(data as unknown as LoginDto);
            localStorage.setItem('user', JSON.stringify(user))
            return user;
        }
        catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
);

export const fetchCurrentUser = createAsyncThunk<UserDto | undefined>(
    'account/signInUser',
    async (_, thunkAPI) => {
        try {
            const user = await agent.auth.getCurrentUser();
            localStorage.setItem('user', JSON.stringify(user))
            return user;
        }
        catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
);

export const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
         signOut: (state: AccountState) => {
            state.user = undefined;
            localStorage.removeItem('user');
            router.navigate('/');
         }
    },
    extraReducers: (builder => {
        builder.addMatcher(isAnyOf(signInUser.fulfilled, fetchCurrentUser.fulfilled), (state: any, action) => {
            state.user = action.payload;
        });
        builder.addMatcher(isAnyOf(signInUser.rejected, fetchCurrentUser.rejected), (_, action) => {
            console.log(action.payload);
        })
    })
});

export const { signOut } = accountSlice.actions;