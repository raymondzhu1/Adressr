import { createBrowserRouter } from "react-router";
import { Login } from "./views/auth/Login";
import { Register } from "./views/auth/Register";
import { HomePage } from "./views/home/HomePage";

export const router = createBrowserRouter([
    {
        path: "/",
        //errorElement: Fill error handling stuff here.
        children: [
            { index: true, element: <HomePage />}, //Temp landing page.
            { path: "login", element: <Login />},
            { path: "register", element: <Register />}
        ]
    }
]);