import { useEffect, useState } from 'react'
import { Product } from './project'

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
        setProducts(prevState => [...prevState, { id: "", name: "", priceCurrency: "", priceAmount: 0, sku: "" }]);
    }

  return (
    <div>
          <h1>DDD Practice</h1>
          <ul>{products.map((item, index) => (
              <li key={index}>{item.name} - {item.priceAmount} {item.priceCurrency} - {item.sku}</li>
          ))}</ul>
          <button onClick={addProduct}>Add product</button>
    </div>
  )
}

export default App
