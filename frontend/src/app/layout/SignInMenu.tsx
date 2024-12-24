import { Button, Fade, Menu, MenuItem } from "@mui/material";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useAppSelector } from "../store/configureStore";
import { signOut } from "../../features/auth/accountSlice";


export default function SignInMenu() {
    const dispatch = useDispatch();
    const {user} = useAppSelector(state => state.account);
    const [anchorEl, setAnchorEl] = useState(null);
    const open = Boolean(anchorEl);

    const handleClieck = (event: any) => {
        setAnchorEl(event.currentTarget)
    }
    const handleClose = () => {
        setAnchorEl(null);
    }

    return  (
    <>
        <Button 
            color="inherit"
            onClick={handleClieck}
            sx={{typography: 'h6'}}
        >
            {user?.email}
        </Button>
        <Menu
            anchorEl={anchorEl}
            open={open}
            onClose={handleClose}
            TransitionComponent={Fade}
        >
            <MenuItem onClick={handleClose}>Profile</MenuItem>
            <MenuItem onClick={handleClose}>My orders</MenuItem>
            <MenuItem onClick={() => dispatch(signOut())}>Logout</MenuItem>
        </Menu>
    </>)
}