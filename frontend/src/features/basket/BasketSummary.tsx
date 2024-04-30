import { Paper, Table, TableCell, TableContainer, TableRow } from "@mui/material";
import DisplayPrice from "../../tools/price-factory";

interface Props {
    subtotal: number
}


export default function BasketSummary(props: Props) {
    const freeDeliverySum = 200 * 100;

    let deliveryFee = 20 * 100;
    if (props.subtotal >= freeDeliverySum)
        deliveryFee = 0;

    return (
        <>
            <TableContainer component={Paper} variant={'outlined'}>
                <Table>
                    <TableRow>
                        <TableCell colSpan={2}>Subtotal</TableCell>
                        <TableCell align="right">{DisplayPrice(props.subtotal, 'USD')}</TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell colSpan={2}>Delivery fee*</TableCell>
                        <TableCell align="right">{DisplayPrice(deliveryFee, 'USD')}</TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell colSpan={2}>Total</TableCell>
                        <TableCell align="right">{DisplayPrice(props.subtotal + deliveryFee, 'USD')}</TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell>
                            <span style={{ fontStyle: 'italic' }}>Order over {DisplayPrice(freeDeliverySum, 'USD')} for free delivery</span>
                        </TableCell>
                    </TableRow>
                </Table>
            </TableContainer>
        </>
        );
}