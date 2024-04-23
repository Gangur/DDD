import agent from "../../app/api/agent";
import { ProductDto } from "../../app/api/http-client";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ProductList from "./ProductList";
import { useEffect, useState } from "react";

export default function Ctatalog() {
    const [products, setProducts] = useState<ProductDto[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent
            .v1ProductsList()
            .then(data => setProducts(data))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <LoadingComponent />

    return (
        (
            <>
                <ProductList products={products} />
            </>
        )
    );
}