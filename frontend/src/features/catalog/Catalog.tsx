import { Product } from "../../app/models/project";
import ProductList from "./ProductList";
import { useEffect, useState } from "react";

export default function Ctatalog() {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetch('https://localhost:44370/product/v1/list')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    setProducts(data.value)
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