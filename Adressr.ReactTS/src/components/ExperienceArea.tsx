import type { ReactNode } from "react";
import "./Experience.css";

interface ExperienceAreaProps {
    companyName: string;
    roleTitle: string;
    description?: string;
    tone?: "default" | "deep";
    children?: ReactNode;
}

export function ExperienceArea({ companyName, roleTitle, description, tone = "default", children }: ExperienceAreaProps) {
    return(
        <article className={tone === "deep" ? "experience experience--deep" : "experience"}>
            <header>
                <h3 className="experience__company">{companyName}</h3>
                <p className="experience__role">{roleTitle}</p>
            </header>
            {description && <p className="experience__description">{description}</p>}
            {children}
        </article>
    );
}