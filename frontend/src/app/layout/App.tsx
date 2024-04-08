import { useEffect, useState } from 'react'
import { Product } from '../models/project'
import Catalog from '../../features/catalog/Catalog';
import { Container, CssBaseline } from '@mui/material';
import Header from './Header';

function App() {
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

    function addProduct() {
        setProducts(prevState => [...prevState,
            {
                id: "",
                name: "",
                priceCurrency: "",
                priceAmount: 0,
                sku: ""
            }]);
    }

  return (
      <div>
          <CssBaseline />
          <Header />
          <Container>
              <Catalog products={products} addProduct={addProduct} />
          </Container>
    </div>
  )
}

export default App
