import type { ReactNode } from "react";
import { useState } from "react";
import "./Experience.css";

interface ExperienceAreaProps {
    companyName: string;
    roleTitle: string;
    description?: string;
    startDate?: string;
    endDate?: string | null;
    tone?: "default" | "deep"; //Cool way to do configurable backgrounds. Now we can have active backgrounds that could play GIFs or something or cool status effects.
    children?: ReactNode;
}

export function ExperienceArea({ companyName, roleTitle, description, startDate, endDate, tone = "default", children }: ExperienceAreaProps) {
    const [published, setPublished] = useState(false);

    return(
        <article className={tone === "deep" ? "experience experience--deep" : "experience"}>
            <header>
                <div className="experience__header">
                    <h3 className="experience__company">{companyName}</h3>
                    <span className={published ? "entry__state entry__state--public" : "entry__state"} aria-live="polite"> {/* Need to remove when viewing this from a profile. */}
                        {published ? "Public" : "Private"}
                    </span>
                </div>
                <p className="experience__role">{roleTitle}</p>
                {startDate && <p className="experience__tenure">Tenure: {startDate} to {endDate ? endDate : "Now"}</p>}
            </header>
            {description && <p className="experience__description">{description}</p>}
            {children}
            <button type="button" className="entry__publish" onClick={() => setPublished((value) => !value)}> {/* Need to remove when viewing this from a profile. */}
                {published ? "Make private" : "Make public"}
            </button>
        </article>
    );
}