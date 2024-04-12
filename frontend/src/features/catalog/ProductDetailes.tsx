import { Divider, Grid, Skeleton, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ProductDto } from "../../tools/HttpClient";
import HttpClient from "../../tools/HttpClientFactory";
import PictureUrl from "../../tools/PicturesUrlFactory";
import DisplayPrice from "../../tools/PriceFactory";

export default function ProductDetailes() {
    const { id } = useParams<{ id: string }>();
    const [product, setProduct] = useState<ProductDto | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        HttpClient()
            .productV1Get(id)
            .then(data => {
                if (data.success) {
                    setProduct(data.value!)
                }
                else {
                    throw Error(data.errorMessage);
                }
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }, [id]);

    if (loading)
    {
        return
        <>
            <Skeleton />
            <Skeleton animation="wave" />
            <Skeleton animation={false} />
        </>
    }

    if (!product) {
        return <h3>Product not found</h3>
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
                                <TableCell></TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Brand</TableCell>
                                <TableCell></TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Quantity in stock</TableCell>
                                <TableCell></TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>)
}