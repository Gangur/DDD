import { LockOutlined } from "@mui/icons-material";
import {
  Avatar,
  Box,
  Container,
  Grid,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { LoadingButton } from "@mui/lab";
import { RegisterDto } from "../../app/api/http-client";
import agent from "../../app/api/agent";
import { toast } from "react-toastify";

export default function Register() {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    setError,
    formState: { isSubmitting, errors, isValid },
  } = useForm({
    mode: "onChange",
  });

  function handleApiErrors(errors: string[]) {
    errors.forEach((error) => {
      if (error.includes("Password")) {
        setError("password", { message: error });
      } else if (error.includes("Email")) {
        setError("email", { message: error });
      } else if (error.includes("DisplayName")) {
        setError("displayName", { message: error });
      }
    });
  }

  return (
    <Container
      component={Paper}
      maxWidth="sm"
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        p: 4,
      }}
    >
      <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
        <LockOutlined />
      </Avatar>
      <Typography component="h1" variant="h5">
        Register
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit((data) =>
          agent.auth
            .register(data as RegisterDto)
            .then(() => {
              toast.success("Registration successful - you can login now!");
              navigate("/login");
            })
            .catch((error) => handleApiErrors(error))
        )}
        noValidate
        sx={{ mt: 1 }}
      >
        <TextField
          margin="normal"
          required
          fullWidth
          label="Email"
          autoComplete="email"
          autoFocus
          {...register("email", {
            required: "Email is required!",
            pattern: {
              value:
                /^([A-Za-z]+)([0-9]+)?([A-Za-z0-9._]+)?@(([A-Za-z]+)([0-9]+)?([A-Za-z0-9._]+)?)((\.)([a-zA-Z]+))$/,
              message: "Not valid email address!",
            },
          })}
          error={!!errors.email}
          helperText={errors?.email?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Display Name"
          autoComplete="displayName"
          {...register("displayName", {
            required: "Display Name is required!",
          })}
          error={!!errors.displayName}
          helperText={errors?.displayName?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Password"
          type="password"
          autoComplete="password"
          {...register("password", {
            required: "Password is required!",
            pattern: {
              value:
                /^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\d\W])|(?=.*\W)(?=.*\d))|(?=.*\W)(?=.*[A-Z])(?=.*\d)).{8,}$/,
              message: "Password does not meet complexity requirements",
            },
          })}
          error={!!errors.password}
          helperText={errors?.password?.message as string}
        />
        <LoadingButton
          disabled={!isValid}
          loading={isSubmitting}
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Register
        </LoadingButton>
        <Grid container>
          <Grid item>
            <Link to="/login">Already have an account? Sign In</Link>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
}
