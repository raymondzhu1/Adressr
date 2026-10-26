import type { ReactNode } from "react";
import "./auth.css";

interface AuthLayoutProps {
    title: string;
    children: ReactNode;
}

export function AuthLayout({ title, children }: AuthLayoutProps) {
    return(
        <div className="auth">
            <aside className="auth__panel">
                <span className="auth__brand">Adressr</span>

                <div>
                    <p className="auth__pitch">Log your work and stuff. Do company tracking stuff. Resume management. WOOOO!</p>
                    <p className="auth__support">
                        Tasks can be used to help build resumes. Track your tasks today! Select and choose what experiences to show.
                    </p>
                </div>

                <div className="log" aria-hidden="true">
                    <div className="log__head">
                        <span>SAMPLE TEXT JOB TITLE</span>
                        <span className="log__chip">On your profile</span>
                    </div>
                    <ul className="log__tasks">
                        <li className="log__task log__task--done">TASK ONE</li>
                        <li className="log__task log__task--done">TASK TWO</li>
                        <li className="log__task">CURRENT TASK DOING</li>
                    </ul>
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