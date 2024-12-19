import React from "react";
import Header from "./Header";
import {Outlet, useNavigate} from "react-router-dom";
import {Container} from "@mui/material";
import "./Home.css";
import {useAuth} from "../../provider/authProvider";

const Home = () => {
    const { setToken } = useAuth();
    const navigate = useNavigate();

    const handleNavigation = (path) => {
        navigate(path);
    };
    const handleLogout = () => {
        setToken();
        navigate("/", { replace: true });
    };

    return (
        <main className="App">
            <Container component="section" maxWidth={"lg"}>
                <Header onLogout={handleLogout} onNavigate={handleNavigation}/>
                <Outlet/>
            </Container>
        </main>
    );
};

export default Home;
