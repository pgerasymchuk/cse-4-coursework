import {RouterProvider, createBrowserRouter, Navigate} from "react-router-dom";
import {useAuth} from "../provider/authProvider";
import {ProtectedRoute} from "./ProtectedRoute";
import SignIn from "../pages/sign-in/SignIn";
import SignUp from "../pages/sign-up/SignUp";
import Catalog from "../pages/catalog/Catalog";
import BikesPage from "../pages/BikesPage";
import BikeDetailsPage from "../pages/BikeDetailsPage";
// import BikeEditorPage from "../pages/BikeEditorPage";
import BikeFormPage from "../pages/bike-form/BikeFormPage";
import Home from "../pages/home/Home";
import Advertisement from "../pages/advertisement/Advertisement";
import MyBikes from "../pages/my-bikes/MyBikes";
import ResetPassword from "../pages/sign-in/ResetPassword";

const Routes = () => {
    const {token} = useAuth();

    // Define public routes accessible to all users
    const routesForPublic = [
        {
            path: "/about-us",
            element: <div>About Us</div>,
        },
    ];

    // Define routes accessible only to authenticated users
    const routesForAuthenticatedOnly = [
        {
            path: "/",
            element: <ProtectedRoute/>,
            children: [{
                path: "",
                element: <Home/>,

                children: [
                    {
                        path: "",
                        element: <Catalog/>
                    },
                    {
                        path: "/ads/:id",
                        element: <Advertisement />
                    },
                    {
                        path: "/profile",
                        element: <div>User Profile</div>
                    },
                    {
                        path: "/bikes",
                        element: <MyBikes />
                    },
                    {
                        path: "/bikes/:bikeId",
                        element: <BikeDetailsPage/>
                    },
                    /*{
                        path: "/bikes/:bikeId/edit",
                        element: <BikeEditorPage/>
                    }*/
                    {
                        path: "/bikes/:bikeId/edit",
                        element: <BikeFormPage />
                    },
                    {
                        path: "/bikes/create",
                        element: <BikeFormPage />
                    }
                ],
            }]
        },
    ];

    // Define routes accessible only to non-authenticated users
    const routesForNotAuthenticatedOnly = [
        {
            path: "/",
            element: <Navigate to={'/sign-in'} replace={true}/>,
        },
        {
            path: "/sign-in",
            element: <SignIn/>,
        },
        {
            path: "/sign-up",
            element: <SignUp/>,
        },
        {
            path: "/reset-password",
            element: <ResetPassword />,
        },
    ];

    const router = createBrowserRouter([
        ...routesForPublic,
        ...(!token ? routesForNotAuthenticatedOnly : []),
        ...routesForAuthenticatedOnly,
    ]);

    return <RouterProvider router={router}/>;
};

export default Routes;
