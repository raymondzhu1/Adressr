import { useState, type SubmitEvent } from "react";
import { Link, useNavigate } from "react-router";
import { ApiError } from "../../client";
import { useAuth } from "../../auth/AuthContext";
import { FormField } from "../../components/FormField";
import { loginUser } from "../../auth/UserApi";

export function Login() {
    const navigate = useNavigate();
    const { signIn } = useAuth();

    const [identifier, setIdentifier] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);

    async function handleSubmit(event: SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        setError(null);
        setIsSubmitting(true);

        try {
            const response = await loginUser({ identifier: identifier.trim(), password });
            signIn(response.token);
            navigate("/", { replace:true });
        }
        catch(err) {
            if(err instanceof ApiError && err.status === 401) {
                setError("Incorrect username/email or password.");
            }
            else if(err instanceof ApiError) {
                setError(err.message);
            }
            else{
                setError("Could not reach the server. Please try again later.");
            }
        }
        finally {
            setIsSubmitting(false);
        }
    }

    return (
        <main>
            <h1>Login</h1>
            <form onSubmit={handleSubmit}>
                {error && <p role="alert">{error}</p>}
                <FormField id="identifier" label="Username or Email" value={identifier} onChange={setIdentifier} autoComplete="username" required />
                <FormField id="password" label="Password" type="password" value={password} onChange={setPassword} autoComplete="current-password" required />
                <button type="submit" disabled={isSubmitting}>{isSubmitting ? "Logging in..." : "Login"}</button>
            </form>
            <p>No account? <Link to="/register">Register</Link></p>
        </main>
    );
}