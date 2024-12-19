import * as React from 'react';
import AuthProvider from "./provider/authProvider";
import Routes from "./routes";

export default function App() {
    return (
        <AuthProvider>
            <Routes/>
        </AuthProvider>
    );
}
