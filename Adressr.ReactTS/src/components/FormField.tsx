import "./FormField.css";

interface FormFieldProps {
    id: string;
    label: string;
    value: string;
    onChange: (value: string) => void;
    onBlur: () => void;
    type?: "text" | "email" | "password";
    autoComplete?: string;
    maxLength?: number;
    required?: boolean;
    error?: string;
}

export function FormField({ id, label, value, onChange, onBlur, type="text", autoComplete, maxLength, required, error }: FormFieldProps){
    const errorId = `${id}-error`;

    return (
        <div className="field">
            <label className="field__label" htmlFor="{id}">{label}</label>
            <input className="field__input"
                id={id}
                name={id}
                type={type}
                value={value}
                onChange={(e) => onChange(e.target.value)}
                onBlur={onBlur}
                autoComplete={autoComplete}
                maxLength={maxLength}
                required={required}
                aria-invalid={error ? true : undefined}
                aria-describedby={error ? errorId : undefined}
            />
            {error && <p className="field__error" id={errorId} role="alert">{error}</p>}
        </div>
    );
}