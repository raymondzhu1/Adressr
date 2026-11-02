import { Link } from "react-router";
import type { DashboardExamResponse } from "../../../types/Exam/exam.types";
import { DataPanel } from "./DataPanel";

export function ExamsPanel() {
    return(
        <DataPanel<DashboardExamResponse[]>
            title="Private exam invites"
            section="Exams"
            path="/api/exam/dashboard"
            isEmpty={(exams) => exams.length === 0}
            empty={<p className="panel__empty">You're not invited to any private exams. Boohoo... If you were invited to any private exams, they'll be listed here.</p>}
        >
            {(exams) => (
                <ul className="exams">
                    {exams.map((exam) => (
                        <li key={exam.examID} className="exam"> {/* Need to call this dashboard exam rather than plain exam for class name */}
                            <div>
                                <p className="exam__title">{exam.title}</p>
                                <p className="exam__meta">Time Limit: {exam.timeLimitMinutes} minutes</p>
                            </div>
                            <Link className="button button--fit" to={`/exams/${exam.examID}`}>View Exam Page</Link>
                        </li>
                    ))}
                </ul>
            )}
        </DataPanel>
    );
}