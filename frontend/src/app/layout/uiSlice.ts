import { createSlice } from "@reduxjs/toolkit";

export const uiSliece = createSlice({
    name: 'ui',
    initialState: {
        isLoading: false,
        darkMode: true
    },
    reducers: {
        startLoading: (state) => {
            state.isLoading = true;
        },
        stopLoading: (state) => {
            state.isLoading = false;
        },
        setDarkMode: (state, action) => {
            state.darkMode = action.payload;
        }
    }
});

export const {startLoading, stopLoading, setDarkMode} = uiSliece.actions;