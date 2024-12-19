import * as React from 'react';
import ForgotPassword from './ForgotPassword';
import AppTheme from '../../shared-theme/AppTheme';
import ColorModeSelect from '../../shared-theme/ColorModeSelect';
import {
    Box, Button, Checkbox,
    CssBaseline,
    FormControl,
    FormControlLabel,
    FormLabel, Link,
    TextField,
    Typography
} from "@mui/material";
import {useAuth} from "../../provider/authProvider";
import {useNavigate} from "react-router-dom";
import axiosInstance from "../../utils/axiosInstance";
import DirectionsBikeIcon from '@mui/icons-material/DirectionsBike';
import Container from "../../components/Container";
import Card from "../../components/Card";

export default function SignIn(props) {
    const [emailError, setEmailError] = React.useState(false);
    const [emailErrorMessage, setEmailErrorMessage] = React.useState('');
    const [passwordError, setPasswordError] = React.useState(false);
    const [passwordErrorMessage, setPasswordErrorMessage] = React.useState('');
    const [open, setOpen] = React.useState(false);
    const {setToken} = useAuth();
    const navigate = useNavigate();

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (emailError || passwordError) {
            return;
        }
        const data = new FormData(event.currentTarget);
        try {
            const response = await axiosInstance.post('/api/auth/', {
                email: data.get('email'),
                password: data.get('password'),
            });
            setToken(response.data.accessToken);
            navigate("/", {replace: true});
        } catch (error) {
            console.log(error);
        }
    };

    const validateInputs = () => {
        const email = document.getElementById('email');
        const password = document.getElementById('password');

        let isValid = true;

        if (!email.value || !/\S+@\S+\.\S+/.test(email.value)) {
            setEmailError(true);
            setEmailErrorMessage('Please enter a valid email address.');
            isValid = false;
        } else {
            setEmailError(false);
            setEmailErrorMessage('');
        }

        if (!password.value || password.value.length < 6) {
            setPasswordError(true);
            setPasswordErrorMessage('Password must be at least 6 characters long.');
            isValid = false;
        } else {
            setPasswordError(false);
            setPasswordErrorMessage('');
        }

        return isValid;
    };

    return (
        <AppTheme {...props}>
            <CssBaseline enableColorScheme/>
            <Container direction="column" justifyContent="space-between">
                <ColorModeSelect sx={{position: 'fixed', top: '1rem', right: '1rem'}}/>
                <Card variant="outlined">
                    <Box component="div" sx={{display: 'inline-flex', flexAlign: 'center', gap: 2}}>
                        <DirectionsBikeIcon/>
                        <Typography
                            component="p"
                            variant="h4"
                            sx={{fontSize: '1rem'}}
                        >Cycle Share</Typography>
                    </Box>
                    <Typography
                        component="h1"
                        variant="h4"
                        sx={{width: '100%', fontSize: 'clamp(2rem, 10vw, 2.15rem)'}}
                    >
                        Sign in
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                        noValidate
                        sx={{
                            display: 'flex',
                            flexDirection: 'column',
                            width: '100%',
                            gap: 2,
                        }}
                    >
                        <FormControl>
                            <FormLabel htmlFor="email">Email</FormLabel>
                            <TextField
                                error={emailError}
                                helperText={emailErrorMessage}
                                id="email"
                                type="email"
                                name="email"
                                placeholder="your@email.com"
                                autoComplete="email"
                                autoFocus
                                required
                                fullWidth
                                variant="outlined"
                                color={emailError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="password">Password</FormLabel>
                            <TextField
                                error={passwordError}
                                helperText={passwordErrorMessage}
                                name="password"
                                placeholder="••••••"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                                autoFocus
                                required
                                fullWidth
                                variant="outlined"
                                color={passwordError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <FormControlLabel
                            control={<Checkbox value="remember" color="primary"/>}
                            label="Remember me"
                        />
                        <ForgotPassword open={open} handleClose={handleClose}/>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            onClick={validateInputs}
                        >
                            Sign in
                        </Button>
                        <Link
                            component="button"
                            type="button"
                            onClick={handleClickOpen}
                            variant="body2"
                            sx={{alignSelf: 'center'}}
                        >
                            Forgot your password?
                        </Link>
                    </Box>
                    {/*<Divider>or</Divider>*/}
                    {/*<Box sx={{display: 'flex', flexDirection: 'column', gap: 2}}>*/}
                    {/*    <Button*/}
                    {/*        fullWidth*/}
                    {/*        variant="outlined"*/}
                    {/*        onClick={() => alert('Sign in with Google')}*/}
                    {/*        startIcon={<GoogleIcon/>}*/}
                    {/*    >*/}
                    {/*        Sign in with Google*/}
                    {/*    </Button>*/}
                    {/*    <Button*/}
                    {/*        fullWidth*/}
                    {/*        variant="outlined"*/}
                    {/*        onClick={() => alert('Sign in with Facebook')}*/}
                    {/*        startIcon={<FacebookIcon/>}*/}
                    {/*    >*/}
                    {/*        Sign in with Facebook*/}
                    {/*    </Button>*/}
                    {/*    <Typography sx={{textAlign: 'center'}}>*/}
                    {/*        Don&apos;t have an account?{' '}*/}
                    {/*        <Link*/}
                    {/*            href="/sign-up/"*/}
                    {/*            variant="body2"*/}
                    {/*            sx={{alignSelf: 'center'}}*/}
                    {/*        >*/}
                    {/*            Sign up*/}
                    {/*        </Link>*/}
                    {/*    </Typography>*/}
                    {/*</Box>*/}
                </Card>
            </Container>
        </AppTheme>
    );
}
