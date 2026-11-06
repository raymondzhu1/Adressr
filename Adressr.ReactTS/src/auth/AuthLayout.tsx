import type { ReactNode } from "react";
import { SampleExperience } from "../components/SampleExperience";
import "./auth.css";

interface AuthLayoutProps {
    title: string;
    children: ReactNode;
}

export function AuthLayout({ title, children }: AuthLayoutProps) {
    return(
        <div className="auth">
            <aside className="auth__panel">
                <div>
                    <p className="auth__pitch">Log your work and stuff. Do company tracking stuff. Resume management. WOOOO!</p>
                    <p className="auth__support">
                        Tasks can be used to help build resumes. Track your tasks today! Select and choose what experiences to show.
                    </p>
                </div>

                <div className="auth__sample" aria-hidden="true">
                    <SampleExperience compact />
                </div>
            </aside>

            <main className="auth__main">
                <div className="auth__content">
                    <h1 className="auth__title">{title}</h1>
                    {children}
                </div>
            </main>
        </div>
    );
}