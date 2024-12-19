import * as React from 'react';
import AppTheme from '../../shared-theme/AppTheme';
import {GoogleIcon, FacebookIcon, SitemarkIcon} from './CustomIcons';
import ColorModeSelect from '../../shared-theme/ColorModeSelect';
import {Button, CssBaseline, Divider, FormControl, FormLabel, TextField, Typography} from "@mui/material";
import Link from "@mui/material/Link";
import Box from "@mui/material/Box";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import {useAuth} from "../../provider/authProvider";
import {useNavigate} from "react-router-dom";
import axiosInstance from "../../utils/axiosInstance";
import Container from "../../components/Container";
import Card from "../../components/Card";
import DirectionsBikeIcon from "@mui/icons-material/DirectionsBike";

export default function SignUp(props) {
    const [emailError, setEmailError] = React.useState(false);
    const [emailErrorMessage, setEmailErrorMessage] = React.useState('');
    const [passwordError, setPasswordError] = React.useState(false);
    const [passwordErrorMessage, setPasswordErrorMessage] = React.useState('');
    const [nameError, setNameError] = React.useState(false);
    const [nameErrorMessage, setNameErrorMessage] = React.useState('');
    const {setToken} = useAuth();
    const navigate = useNavigate();

    const validateInputs = () => {
        const email = document.getElementById('email');
        const password = document.getElementById('password')
        const name = document.getElementById('name');

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

        if (!name.value || name.value.length < 1) {
            setNameError(true);
            setNameErrorMessage('Name is required.');
            isValid = false;
        } else {
            setNameError(false);
            setNameErrorMessage('');
        }

        return isValid;
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (nameError || emailError || passwordError) {
            return;
        }
        const data = new FormData(event.currentTarget);
        try {
            const name = data.get('name').split(' ');
            const firstname = name[0] || '';
            const lastname = name[1] || '';
            const response = await axiosInstance.post('/api/user/', {
                firstName: firstname,
                lastName: lastname,
                email: data.get('email'),
                password: data.get('password'),
                gender: "m",
                login: data.get('email'),
                phoneNumber: "0123456789"
            });
            setToken(response.data.accessToken);
            navigate("/", {replace: true});
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <AppTheme {...props}>
            <CssBaseline enableColorScheme/>
            <ColorModeSelect sx={{position: 'fixed', top: '1rem', right: '1rem'}}/>
            <Container direction="column" justifyContent="space-between">
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
                        Sign up
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                        sx={{display: 'flex', flexDirection: 'column', gap: 2}}
                    >
                        <FormControl>
                            <FormLabel htmlFor="name">Full name</FormLabel>
                            <TextField
                                autoComplete="name"
                                name="name"
                                required
                                fullWidth
                                id="name"
                                placeholder="Jon Snow"
                                error={nameError}
                                helperText={nameErrorMessage}
                                color={nameError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="email">Email</FormLabel>
                            <TextField
                                required
                                fullWidth
                                id="email"
                                placeholder="your@email.com"
                                name="email"
                                autoComplete="email"
                                variant="outlined"
                                error={emailError}
                                helperText={emailErrorMessage}
                                color={passwordError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="password">Password</FormLabel>
                            <TextField
                                required
                                fullWidth
                                name="password"
                                placeholder="••••••"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                                variant="outlined"
                                error={passwordError}
                                helperText={passwordErrorMessage}
                                color={passwordError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <FormControlLabel
                            control={<Checkbox value="allowExtraEmails" color="primary"/>}
                            label="I want to receive updates via email."
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            onClick={validateInputs}
                        >
                            Sign up
                        </Button>
                    </Box>
                    <Divider>
                        <Typography sx={{color: 'text.secondary'}}>or</Typography>
                    </Divider>
                    <Box sx={{display: 'flex', flexDirection: 'column', gap: 2}}>
                        <Button
                            fullWidth
                            variant="outlined"
                            onClick={() => alert('Sign up with Google')}
                            startIcon={<GoogleIcon/>}
                        >
                            Sign up with Google
                        </Button>
                        <Button
                            fullWidth
                            variant="outlined"
                            onClick={() => alert('Sign up with Facebook')}
                            startIcon={<FacebookIcon/>}
                        >
                            Sign up with Facebook
                        </Button>
                        <Typography sx={{textAlign: 'center'}}>
                            Already have an account?{' '}
                            <Link
                                href="/sign-in/"
                                variant="body2"
                                sx={{alignSelf: 'center'}}
                            >
                                Sign in
                            </Link>
                        </Typography>
                    </Box>
                </Card>
            </Container>
        </AppTheme>
    );
}
