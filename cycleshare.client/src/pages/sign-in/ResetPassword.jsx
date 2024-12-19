import * as React from 'react';
import AppTheme from '../../shared-theme/AppTheme';
import ColorModeSelect from '../../shared-theme/ColorModeSelect';
import {
    Box, Button,
    CssBaseline,
    FormControl,
    FormLabel, Link,
    TextField,
    Typography
} from "@mui/material";
import {useAuth} from "../../provider/authProvider";
import {useNavigate, useSearchParams} from "react-router-dom";
import axiosInstance from "../../utils/axiosInstance";
import DirectionsBikeIcon from '@mui/icons-material/DirectionsBike';
import Container from "../../components/Container";
import Card from "../../components/Card";

export default function ResetPassword(props) {
    const [passwordError, setPasswordError] = React.useState(false);
    const [passwordErrorMessage, setPasswordErrorMessage] = React.useState('');
    const [repeatPasswordError, setRepeatPasswordError] = React.useState(false);
    const [repeatPasswordErrorMessage, setRepeatPasswordErrorMessage] = React.useState('');
    const [searchParams, setSearchParams] = useSearchParams();
    const {setToken} = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (passwordError || repeatPasswordError) {
            return;
        }
        const data = new FormData(event.currentTarget);
        try {
            const response = await axiosInstance.post('/api/user/password/reset/', {
                password: data.get('password'),
                repeatPassword: data.get('repeatPassword'),
                token: searchParams.get('token'),
            });
            // setToken(response.data.accessToken);
            navigate("/", {replace: true});
        } catch (error) {
            console.log(error);
        }
    };

    const validateInputs = () => {
        const password = document.getElementById('password');
        const repeatPassword = document.getElementById('repeatPassword');

        let isValid = true;

        if (!password.value || password.value.length < 6) {
            setPasswordError(true);
            setPasswordErrorMessage('Password must be at least 6 characters long.');
            isValid = false;
        } else {
            setPasswordError(false);
            setPasswordErrorMessage('');
        }

        if (!repeatPassword.value || repeatPassword.value !== password.value ) {
            setRepeatPasswordError(true);
            setPasswordErrorMessage('Passwords must match.');
            isValid = false;
        } else {
            setRepeatPasswordError(false);
            setRepeatPasswordErrorMessage('');
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
                        <DirectionsBikeIcon />
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
                        Reset Password
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
                            <FormLabel htmlFor="password">Password</FormLabel>
                            <TextField
                                error={passwordError}
                                helperText={passwordErrorMessage}
                                name="password"
                                placeholder="••••••"
                                type="password"
                                id="password"
                                autoFocus
                                required
                                fullWidth
                                variant="outlined"
                                color={passwordError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="repeatPassword">Repeat Password</FormLabel>
                            <TextField
                                error={repeatPasswordError}
                                helperText={repeatPasswordErrorMessage}
                                name="repeatPassword"
                                placeholder="••••••"
                                type="password"
                                id="repeatPassword"
                                required
                                fullWidth
                                variant="outlined"
                                color={repeatPasswordError ? 'error' : 'primary'}
                            />
                        </FormControl>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            onClick={validateInputs}
                        >
                            Reset
                        </Button>
                    </Box>
                </Card>
            </Container>
        </AppTheme>
    );
}
