import { ProductDto } from "../../tools/HttpClient";
import HttpClient from "../../tools/HttpClientFactory";
import ProductList from "./ProductList";
import { useEffect, useState } from "react";

export default function Ctatalog() {
    const [products, setProducts] = useState<ProductDto[]>([]);

    useEffect(() => {
        HttpClient()
            .productV1List()
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