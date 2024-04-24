import { Divider, Grid, Skeleton, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ProductDto } from "../../app/api/http-client";
import PictureUrl from "../../tools/pictures-url-factory";
import DisplayPrice from "../../tools/PriceFactory";
import agent from "../../app/api/agent";
import NotFound from "../../app/errors/NotFound";
import sleep from "../../tools/sleep";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default function ProductDetailes() {
    const { id } = useParams<{ id: string }>();
    const [product, setProduct] = useState<ProductDto | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent
            .v1Products(id ?? "")
            .then(data => {
                //await sleep()
                setProduct(data)
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }, [id]);

    if (loading)
    {
        return (<LoadingComponent />)
    }

    if (!product) {
        return <NotFound />
    }

    return (
        <Grid container spacing={6}>
            <Grid item xs={6}>
                <img src={PictureUrl(product.pictureName!)} style={{ width: '100%', marginTop: '42pt' }}></img>
            </Grid>
            <Grid item xs={6}>
                <Typography variant='h3'>{product.name}</Typography>
                <Divider sx={{ mb: 2 }} />
                <Typography variant='h4' color='secondary'>{DisplayPrice(product.priceAmount, product.priceCurrency)}</Typography>
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>{product.name}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Description</TableCell>
                                <TableCell></TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Type</TableCell>
                                <TableCell>{product.category}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Brand</TableCell>
                                <TableCell>{product.brand}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Quantity in stock</TableCell>
                                <TableCell>{product.sku}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>)
}