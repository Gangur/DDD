import agent from "../../app/api/agent";
import { ProductDto } from "../../app/api/http-client";
import ProductList from "./ProductList";
import { useEffect, useState } from "react";

export default function Ctatalog() {
    const [products, setProducts] = useState<ProductDto[]>([]);

    useEffect(() => {
        agent
            .v1ProductsList()
            .then(data => {
                if (data.success) {
                    setProducts(data.value!)
                }
                else {
                    throw Error(data.errorMessage);
                }
            });
    }, []);

    return (
        (
            <>
                <ProductList products={products} />
            </>
        )
    );
}