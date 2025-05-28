import { Container, CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import Header from './Header';
import { useCallback, useEffect } from 'react';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import { useAppDispatch, useAppSelector } from '../store/configureStore';
import { fetchCurrentUser } from '../../features/auth/accountSlice';
import { setDarkMode } from './uiSlice';

function App() {
    const dispatch = useAppDispatch();
    const {darkMode} = useAppSelector(state => state.ui);

    const initApp = useCallback(async () => {
        try {
            await dispatch(fetchCurrentUser());
        }
        catch (error) {
            console.log(error);
        }
    }, [dispatch])

    useEffect(() => {
        initApp();
    }, [initApp]);

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
        dispatch(setDarkMode(!darkMode));
    }

    //if (isLoading)
    //    return <LoadingComponent />

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
