import { Backdrop, Grid, Skeleton } from "@mui/material";

export default function LoadingComponent() {
    return (
        <Backdrop open={true} invisible={true}>
            <Grid container spacing={5}>
                <Grid item xs={4}></Grid>
                <Grid item xs={4}>
                    <Skeleton />
                    <Skeleton animation="wave" />
                    <Skeleton animation={false} />
                </Grid>
            </Grid>
        </Backdrop>
    )
}