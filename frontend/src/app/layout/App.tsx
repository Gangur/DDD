import { Container, CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import Header from './Header';
import { useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import { useAppDispatch } from '../store/configureStore';
import agent from '../api/agent';
import { setBasket } from '../../features/basket/basketSlice';
import { getCustomerId } from '../../tools/cookies';
import LoadingComponent from './LoadingComponent';

function App() {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const customerId = getCustomerId();

        if (customerId) {
            agent.v1OrdersByCustomer(customerId)
                .then(order => {
                    dispatch(setBasket(order));
                })
                .catch(error => console.log(error))
                .finally(() => setLoading(false))
        } else {
            setLoading(false);
        }
    }, [dispatch]);

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
