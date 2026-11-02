import { useState } from "react";
import { SampleExperience } from "../../components/SampleExperience";

export function TaskDemo() {
    // const [done, setDone] = useState<boolean[]>([true, true, false]);
    const [published, setPublished] = useState(false);

    // const toggleTask = (index: number) => 
    //     setDone((current) => current.map((value, i) => (i === index ? !value : value)));

        return(
            <div>
                <div className="entry__bar">
                    <span className={published ? "entry__state entry__state--public" : "entry__state"} aria-live="polite">
                        {published ? "On your profile" : "Private"}
                    </span>
                </div>

                <SampleExperience />

                <button type="button" className="entry__publish" onClick={() => setPublished((value) => !value)}>
                    {published ? "Make private" : "Show on profile"}
                </button>
            </div>
            // <div className="entry">
            //     <div className="entry__head">
            //         <span>
            //             Job Title Here
            //         </span>
            //         <span className={published ? "entry__state entry__state--public" : "entry__state"} aria-live="polite">
            //             {published ? "On your profile" : "Private"}
            //         </span>
            //     </div>

            //     <ul className="entry__tasks">
            //         {TASKS.map((task, index) => (
            //             <li key={task}>
            //                 <label className="entry__task">
            //                     <input type="checkbox" checked={done[index]} onChange={() => toggleTask(index)} />
            //                     <span>{task}</span>
            //                 </label>
            //             </li>
            //         ))}
            //     </ul>

            //     <button type="button" className="entry__publish" onClick={() => setPublished((value) => !value)}>
            //         {published ? "Make private" : "Show on profile"}
            //     </button>
            // </div>
        );
}