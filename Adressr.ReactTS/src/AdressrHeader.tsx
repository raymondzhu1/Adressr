import { Link, useNavigate } from "react-router";
import { useAuth } from "./auth/AuthContext";
import { SearchBox } from "./components/SearchBox";

export function AdressrHeader() {
    const { isAuthenticated, signOut } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        signOut();
        navigate("/", {replace: true});
    };

    const handleLogin = () => {
        navigate("/login");
    }

    return(
        <header className="site-header">
            <div className="site-header__inner">
                <Link to="/" className="site-header__brand">Adressr</Link>
                
                <div className="site-header__search">
                    <SearchBox />
                </div>

                {isAuthenticated ? (
                    <button type="button" className="site-header__action" onClick={handleLogout}>Logout</button>
                ) : (
                    <button type="button" className="site-header__action" onClick={handleLogin}>Login</button>
                )}
            </div>
        </header>
    );
}