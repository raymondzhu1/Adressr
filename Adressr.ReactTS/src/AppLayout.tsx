import { Outlet } from "react-router";
import { AdressrHeader } from "./AdressrHeader";
import "./app.css";

export function AppLayout() {
    return(
        <div className="app">
            <AdressrHeader />
            <div className="app__content">
                <Outlet />
            </div>
        </div>
    );
}