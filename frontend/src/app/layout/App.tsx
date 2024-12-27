import { Container, CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import Header from './Header';
import { useCallback, useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import { useAppDispatch } from '../store/configureStore';
import { fetchBasketAsync } from '../../features/basket/basketSlice';
import LoadingComponent from './LoadingComponent';
import { fetchCurrentUser } from '../../features/auth/accountSlice';

function App() {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    const initApp = useCallback(async () => {
        try {
            await dispatch(fetchCurrentUser());
            await dispatch(fetchBasketAsync())
        }
        catch (error) {
            console.log(error);
        }
    }, [dispatch])

    useEffect(() => {
        initApp()
        .then(() => setLoading(false));
    }, [initApp]);

    const [darkMode, setDarkMode] = useState(true);
    const paletteType = darkMode ? 'dark' : 'light'
    const theme = createTheme({
        palette: {
            mode: paletteType,
            background: {
                default: darkMode ? '#121212' : '#eaeaea'
            }
        }
    });

    function handleThemeChange() {
        setDarkMode(!darkMode);
    }

    if (loading)
        return <LoadingComponent />

    return (
        <ThemeProvider theme={theme}>
            <ToastContainer position='bottom-right' hideProgressBar theme='colored' />
            <CssBaseline />
            <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
            <Container>
                <Outlet />
            </Container>
        </ThemeProvider>
  )
}

export default App
