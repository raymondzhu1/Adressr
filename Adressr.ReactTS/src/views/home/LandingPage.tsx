import { Link } from "react-router";
//import { TaskDemo } from "./TaskDemo";
import { SampleExperience } from "../../components/SampleExperience";
import "./landing.css";

//Gotta really make a global header bar so we don't need to define it here again.
export function LandingPage() {
    return(
        <div>
            <header className="landing__bar">
                <span className="landing__brand">Adressr</span>
                <Link to="/login">Login</Link>
            </header>

            <main>
                <section className="simpleTask">
                    <div>
                        <h1 className="simpleTask__title">Log your work. Share only what you want.</h1>
                        <p className="simpleTask__lead">
                            Track tasks you do at your current occupation or general hobby. Publish that experience to your profile.
                        </p>
                        <div className="simpleTask__actions">
                            <Link className="button button--fit" to="/register">Create account</Link>
                            <Link to="/login">Login</Link>
                        </div>
                    </div>

                    <SampleExperience />
                    {/* <div className="simpleTask__demo">
                        <TaskDemo />
                    </div> */}
                </section>

                <section className="offers">
                    <div className="offer">
                        <h2 className="offer__title">Keep a log</h2>
                        <p className="offer__text">
                            Record your tasks and subtasks for each thing you do. Tick them as complete and choose which experience gets published.
                        </p>
                    </div>
                    <div className="offer">
                        <h2 className="offer__title">Digitalize your company tree</h2>
                        <p className="offer__text">
                            Add your company to our web application and fill in your company structure to our convenient company features. Add positions, and then add openings for those position to allow any person you accept for that opening to autofill into those positions.
                        </p>
                    </div>
                    <div className="offer">
                        <h2 className="offer__title">Create interview exams for recruitment</h2>
                        <p className="offer__text">
                            Invite people to private exams to judge their skills. Create public exams as well for anyone to try.
                        </p>
                    </div>
                </section>
            </main>
        </div>
    );
}