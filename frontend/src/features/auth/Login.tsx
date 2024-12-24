import { LockOutlined } from "@mui/icons-material";
import { Avatar, Box, Container, Grid, Paper, TextField, Typography } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { FieldValues, useForm } from "react-hook-form";
import { LoadingButton } from "@mui/lab";
import { UserDto } from "../../app/api/http-client";
import { useAppDispatch } from "../../app/store/configureStore";
import { signInUser } from "./accountSlice";

export default function Login() {
    const navigation = useNavigate();
    const dispatch = useAppDispatch()

    const {register, handleSubmit, formState: {isSubmitting, errors, isValid}} = useForm({
        mode: 'onSubmit'
    });

    async function submitForm(data: FieldValues) {
        await dispatch(signInUser(data as UserDto));
        navigation('/catalog');
    }

    return (
        <Container component={Paper} maxWidth="sm" sx={{display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4}}> 
            <Avatar sx={{m: 1, bgcolor: 'secondary.main'}}>
                <LockOutlined/>
            </Avatar>
            <Typography component="h1" variant="h5">
                Sign In
            </Typography>
            <Box component="form" onSubmit={handleSubmit(submitForm)} noValidate sx={{mt: 1}}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    label="Login"
                    autoComplete="email"
                    autoFocus
                    {...register('login', { required: 'Login is required!' })}
                    error={!!errors.login}
                    helperText={errors?.login?.message as string}
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    label="Password"
                    type="password"
                    autoComplete="current-password"
                    autoFocus
                    {...register('password', { required: 'Password is required!' })}
                    error={!!errors.password}
                    helperText={errors?.password?.message as string}
                />
                <LoadingButton 
                    disabled={!isValid}
                    loading={isSubmitting} 
                    type="submit" 
                    fullWidth 
                    variant="contained" sx={{ mt: 3, mb: 2 }}>
                    Sign In
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to="/register" >
                            Don't have an account? Sign Up
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    )
}