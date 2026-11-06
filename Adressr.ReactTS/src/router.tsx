import { createBrowserRouter } from "react-router";
import { Login } from "./views/auth/Login";
import { Register } from "./views/auth/Register";
import { HomePage } from "./views/home/HomePage";
import { AppLayout } from "./AppLayout";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <AppLayout />,
        //errorElement: Fill error handling stuff here.
        children: [
            { index: true, element: <HomePage />}, //Temp landing page.
            { path: "login", element: <Login />},
            { path: "register", element: <Register />}
        ]
    }
]);