import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import Home from "../../features/home/HomePage";
import Catalog from "../../features/catalog/Catalog";
import ProductDetailes from "../../features/catalog/ProductDetailes";
import About from "../../features/about/AboutPage";
import Contact from "../../features/contact/ContactPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import BasketPage from "../../features/basket/BasketPage";
import CheckoutPage from "../../features/checkout/CheckoutPage";
import Register from "../../features/auth/Register";
import Login from "../../features/auth/Login";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <Home /> },
            { path: 'catalog', element: <Catalog /> },
            { path: 'catalog/:id', element: <ProductDetailes /> },
            { path: 'about', element: <About /> },
            { path: 'contact', element: <Contact /> },
            { path: 'server-error', element: <ServerError /> },
            { path: 'not-found', element: <NotFound /> },
            { path: 'basket', element: <BasketPage /> },
            { path: 'checkout', element: <CheckoutPage /> },
            { path: 'register', element: <Register /> },
            { path: 'login', element: <Login /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },
        ]
    }
])