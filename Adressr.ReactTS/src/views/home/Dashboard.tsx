import { CompaniesPanel } from "./DashboardSections/CompaniesPanel";
import { ExamsPanel } from "./DashboardSections/ExamsPanel";
import { TasksPanel } from "./DashboardSections/TasksPanel";
import "./dashboard.css";

export function Dashboard() {
    return(
        <div>
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