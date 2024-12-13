import { TextField } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setProductParams } from "./catalogSlice";

export default function ProductSearch()
{
    const  {productParams} = useAppSelector(state => state.catalog);
    const dispatch = useAppDispatch();

    return <TextField
            label='Search products'
            variant='outlined'
            fullWidth 
            value={productParams.searchTerm || ''}
            onChange={event => dispatch(setProductParams({searchTerm: event.target.value}))}/>
}