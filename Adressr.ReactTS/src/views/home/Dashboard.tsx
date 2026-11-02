import { useAuth } from "../../auth/AuthContext";
import { CompaniesPanel } from "./DashboardSections/CompaniesPanel";
import { ExamsPanel } from "./DashboardSections/ExamsPanel";
import { TasksPanel } from "./DashboardSections/TasksPanel";
import "./dashboard.css";

export function Dashboard() {
    const { signOut } = useAuth();

    return(
        <div>
            {/* Temporary header location. This should be in a more global location throughout the app, rather than only sitting in the dashboard. */}
            <header className="headerbar">
                <span className="header__brand">Adressr</span>
                <button type="button" className="header__logout" onClick={signOut}>Logout</button>
            </header>

            <main className="dashboard__main">
                <h1 className="dashboard__title">Home</h1>
                {/* Need to add separators potentially between each of these components. */}
                <CompaniesPanel />
                <TasksPanel />
                <ExamsPanel />
            </main>
        </div>
    );
}