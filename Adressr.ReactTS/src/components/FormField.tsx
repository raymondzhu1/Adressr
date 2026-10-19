interface FormFieldProps {
    id: string;
    label: string;
    value: string;
    onChange: (value: string) => void;
    type?: "text" | "email" | "password";
    autoComplete?: string;
    maxLength?: number;
    required?: boolean;
    error?: string;
}

export function FormField({ id, label, value, onChange, type="text", autoComplete, maxLength, required, error }: FormFieldProps){
    const errorId = `${id}-error`;

    return (
        <div>
            <label htmlFor="{id}">{label}</label>
            <input
                id={id}
                name={id}
                type={type}
                value={value}
                onChange={(e) => onChange(e.target.value)}
                autoComplete={autoComplete}
                maxLength={maxLength}
                required={required}
                aria-invalid={error ? true : undefined}
                aria-describedby={error ? errorId : undefined}
            />
            {error && <p id={errorId} role="alert">{error}</p>}
        </div>
    );
}