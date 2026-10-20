import { useState, type SubmitEvent } from "react";
import { Link, useNavigate } from "react-router";
import { ApiError } from "../../client";
import { loginUser, registerUser } from "../../auth/UserApi";
import type { ProblemDetails } from "../../types/Error/problem.type";
import { useAuth } from "../../auth/AuthContext";
import { FormField } from "../../components/FormField";

interface RegisterForm {
    username: string,
    email: string,
    name: string,
    education: string,
    password: string,
    confirmPassword: string
}

type FieldErrors = Partial<Record<keyof RegisterForm, string>>; //Technically this is <keyof RegisterForm, string | undefined> but I'm not gonna write that.

const emptyForm: RegisterForm = { 
    username: "",
    email: "",
    name: "",
    education: "",
    password: "",
    confirmPassword: ""
};

const LIMITS = { username: 30, email: 150, name: 50, education:70, password:50 };

const allTouched: Record<keyof RegisterForm, boolean> = {
    username: false,
    email: false,
    name: false,
    education: false,
    password: false,
    confirmPassword: false
};

function validate(form: RegisterForm): FieldErrors {
    const errors: FieldErrors = {};

    const username = form.username.trim();
    if(!username){
        errors.username = "Username is required.";
    }
    else if(username.length > LIMITS.username){
        errors.username = `Username must be ${LIMITS.username} characters of fewer.`;
    }

    const email = form.email.trim();
    const EMAIL_PATTERN = /^[^\s@]+@[^\s@.]+(?:\.[^\s@.]+)+$/;
    if(!EMAIL_PATTERN.test(email)){
        errors.email = "Enter a valid email address. I'm watching you buddy.";
    }
    else if(email.length > LIMITS.email) {
        errors.email = "Email address is too long.";
    }

    const fullName = form.name.trim();
    if(!fullName){
        errors.name = "Gimme your name.";
    }
    else if(fullName.length > LIMITS.name) {
        errors.name = `Your full name is too long. Can you make it ${LIMITS.name} characters long or less? Thanks.`;
    }

    if(form.education.trim().length > LIMITS.education){
        errors.education = `Can you make your educational history ${LIMITS.education} characters long or less? Thanks.`;
    }

    if(form.password.length < 8) {
        errors.password = "Password must be at least 8 characters long.";
    }
    else if(form.password.length > LIMITS.password) {
        errors.password = `Yo, I know you like them long passwords, but come on man. Shotern it down to ${LIMITS.password} or less.`;
    }

    if(form.confirmPassword !== form.password){
        errors.confirmPassword = "Passwords aren't matching go do it again.";
    }

    return errors;
}

function toFieldErrors(problem: ProblemDetails | null): FieldErrors {
    /*
        This function all it does is change the first letter of the ProblemDetail errors JSON key portions to lowercase. These errors come from the .NET API side.
        This is needed in order to match the RegisterForm fields that are lowercase.
        The reason this issue exists is due to casing conventions clashing between .NET and other languages. 
    */
    const result: FieldErrors = {};
    for(const [key, messages] of Object.entries(problem?.errors ?? {})) {
        const field = (key.charAt(0).toLowerCase() + key.slice(1)) as keyof RegisterForm;
        result[field] = messages[0];
    }
    return result;
}

export function Register() {
    const navigate = useNavigate(); //React hook. assigned. cant just use useNavigate, would break rules. Top level.
    const { signIn } = useAuth();

    const [form, setForm] = useState<RegisterForm>(emptyForm);
    const [touched, setTouched] = useState<Partial<Record<keyof RegisterForm, boolean>>>({});
    const [serverErrors, setServerErrors] = useState<FieldErrors>({});
    const [formError, setFormError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);

    const clientErrors = validate(form);

    const errorFor = (field: keyof RegisterForm): string | undefined => serverErrors[field] ?? (touched[field] ? clientErrors[field] : undefined);
    const markTouched = (field: keyof RegisterForm) => setTouched((current) => (current[field] ? current : {...current, [field]: true }));
    const update = (field: keyof RegisterForm) => (value: string) => setForm((current) => ({...current, [field]: value})); //Custom FormField already does (e) => onChange(e.target.value) on the onChange so we can just be a function that returns a function and chuck it into onChange in HTML.

    async function handleSubmit(event: SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        setFormError(null);
        setServerErrors({});
        setTouched(allTouched);
        if(Object.keys(clientErrors).length > 0) {
            return;
        }

        setIsSubmitting(true);

        try{
            await registerUser({
                username: form.username.trim(),
                email: form.email.trim(),
                password: form.password,
                name: form.name.trim(),
                education: form.education.trim() || undefined
            });
        }
        catch(err){
            if(err instanceof ApiError) {
                const mappedServerErrors = toFieldErrors(err.problem);
                setServerErrors(mappedServerErrors);
                if(Object.keys(mappedServerErrors).length === 0) {
                    setFormError(err.message);
                }
            }
            else{
                setFormError("Could not reach the server. Try again later.");
            }
            setIsSubmitting(false);
            return;
        }

        //Adding this in to just auto sign me in when I register an account. If this actually released, you'd remove this part of the code and let the user login by themselves after a redirect to the registration page with navigate("/login". {replace: true}). Replace to prevent going back and requesting again.
        try{
            const { token } = await loginUser({ identifier: form.username.trim(), password: form.password });
            signIn(token);
            navigate("/", {replace: true});
        }
        catch {
            navigate("/login", { replace: true });
        }
    }

    return(
        <main>
            <h1>Create an account</h1>
            <form onSubmit={handleSubmit} noValidate>
                {formError && <p role="alert">{formError}</p>}
                <FormField id="username" label="Username" value={form.username} onChange={update("username")} onBlur={() => markTouched("username")} autoComplete="username" maxLength={LIMITS.username} required error={errorFor("username")} />
                <FormField id="email" label="E-mail Address" type="email" value={form.email} onChange={update("email")} onBlur={() => markTouched("email")} autoComplete="email" required error={errorFor("email")} />
                <FormField id="name" label="Full Name" value={form.name} onChange={update("name")} onBlur={() => markTouched("name")} autoComplete="name" maxLength={LIMITS.name} required error={errorFor("name")} />
                <FormField id="education" label="Education" value={form.education} onChange={update("education")} onBlur={() => markTouched("education")} maxLength={LIMITS.education} error={errorFor("education")} />
                <FormField id="password" label="Password" type="password" value={form.password} onChange={update("password")} onBlur={() => markTouched("password")} autoComplete="new-password" required error={errorFor("password")} />
                <FormField id="confirmPassword" label="Confirm Password" type="password" value={form.confirmPassword} onChange={update("confirmPassword")} onBlur={() => markTouched("confirmPassword")} autoComplete="new-password" required error={errorFor("confirmPassword")} />
                <button type="submit" disabled={isSubmitting}>{isSubmitting ? "Creating account. Hold on..." : "Register"}</button>
            </form>
            <p>Already have an account? <Link to="/login">Login instead</Link></p>
        </main>
    );
}