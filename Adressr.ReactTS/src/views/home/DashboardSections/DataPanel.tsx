import { useId, type ReactNode } from "react";
import { useAuthorizedGet } from "../../../auth/useAuthorizedGet";

interface DataPanelProps<T> {
    title: string;
    path: string;
    section: string;
    action?: ReactNode;
    isEmpty: (data: T) => boolean;
    empty: ReactNode;
    children: (data: T) => ReactNode;
}

export function DataPanel<T>({ title, path, section, action, isEmpty, empty, children }: DataPanelProps<T>) {
    const titleId = useId();
    const query = useAuthorizedGet<T>(path);

    function renderBody() {
        if(query.data !== undefined) {
            return isEmpty(query.data) ? empty : children(query.data);
        }

        if(query.isError) {
            return(
                <div className="panel__status">
                    <p>Could not load your {section}. Check your connection, down times on specific web services, and yourself and try again in a little bit.</p>
                    <button type="button" className="button button--fit" onClick={() => query.refetch()}>
                        Refresh {section}
                    </button>
                </div>
            );
        }

        return <p className="panel__status" role="status">Loading your {section}...</p>;
    }

    return(
        <section className="panel" aria-labelledby={titleId}>
            <div className="panel__head">
                <h2 id={titleId} className="panel__title">{title}</h2>
                {action}
            </div>
            {renderBody()}
        </section>
    );
}