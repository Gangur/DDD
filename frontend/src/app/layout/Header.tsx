import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, IconButton, LinearProgress, List, ListItem, Switch, Toolbar, Typography } from "@mui/material";
import { Link, NavLink } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import SignInMenu from "./SignInMenu";
import { useFetchBasketQuery } from "../../features/basket/basketApi";
import { getCookieCustomerId } from "../../tools/cookies";

interface Props {
    darkMode: boolean;
    handleThemeChange: () => void
}

const midLinks = [
    { title: 'catalog', path: '/catalog' },
    { title: 'about', path: '/about' },
    { title: 'contact', path: '/contact' },
]

const rightLinks = [
    { title: 'login', path: '/login' },
    { title: 'register', path: '/register' },
]

const navStyles = {
    color: 'inherit',
    textDecoration: 'none',
    typography: 'h6',
    '&:hover': {
        color: 'grey.300'
    },
    '&.active': {
        color: 'text.secondary'
    }
}

export default function Header({ darkMode, handleThemeChange }: Props) {
    const { data: basket } = useFetchBasketQuery(getCookieCustomerId());
    const { user } = useAppSelector(state => state.account);
    const itemCount = basket?.lineItems?.reduce((sum, item) => sum + item.quantity!, 0) || 0;
    const {isLoading} = useAppSelector(state => state.ui);

    return (
        <AppBar position='static' sx={{ mb: 4 }}>
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Box display='flex' alignItems='center'>
                    <Typography
                        variant='h6'
                        component={NavLink}
                        to='/'
                        sx={navStyles}
                    >
                        DDD Practice
                    </Typography>
                    <Switch checked={darkMode} onChange={handleThemeChange} />
                    <List sx={{ display: 'flex' }}>
                        {midLinks.map(item => (
                            <ListItem
                                component={NavLink}
                                to={item.path}
                                key={item.path}
                                sx={navStyles}
                            >
                                {item.title.toUpperCase()}
                            </ListItem>
                        ))}
                    </List>
                </Box>

                <Box display='flex' alignItems='center'>
                    <IconButton component={Link} to='/basket' size='large' edge='start' color='inherit' sx={{ mr: 2 }}>
                        <Badge badgeContent={itemCount} color='secondary'>
                            <ShoppingCart />
                        </Badge>
                    </IconButton>
                    {user ? (
                        <SignInMenu />
                    ) : (
                    <List sx={{ display: 'flex' }}>
                        {rightLinks.map(item => (
                            <ListItem
                                component={NavLink}
                                to={item.path}
                                key={item.path}
                                sx={navStyles}
                            >
                                {item.title.toUpperCase()}
                            </ListItem>
                        ))}
                    </List>)}
                </Box>
            </Toolbar>
            {isLoading && (
                <Box sx={{width: '100%'}}>
                    <LinearProgress color="secondary"/>
                </Box>
            )}
        </AppBar>
    )
}