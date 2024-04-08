import { Button } from "@mui/material";
import { Product } from "../../app/models/project";
import ProductList from "./ProductList";

interface Props {
    products: Product[];
    addProduct: () => void;
}

export default function Ctatalog({products, addProduct}: Props) {
    return (
        (
            <>
                <ProductList products={products} />
                <Button variant='contained' onClick={addProduct}>Add product</Button>
            </>
        )
    );
}