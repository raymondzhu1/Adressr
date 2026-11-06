import { useEffect, useId, useState } from "react";
import { Link } from "react-router";
import { assetUrl } from "../client";
import { useAuthorizedGet } from "../auth/useAuthorizedGet";
import type { SearchResult } from "../types/Search/search.types";
import "./search.css";

const MIN_LENGTH = 2;

function useDebounced(value: string, delayMs: number) {
    //Sets up the debouncing for our search so we aren't firing requests super often.
    const [debounced, setDebounced] = useState(value);

    useEffect(() => {
        const timer = setTimeout(() => setDebounced(value), delayMs)
        return () => clearTimeout(timer);
    }, [value, delayMs]);

    return debounced;
}

export function SearchBox() {
    const listId = useId();
    const [text, setText] = useState("");
    const [open, setOpen] = useState(false);

    const term = useDebounced(text.trim(), 250);
    const active = term.length >= MIN_LENGTH;
    const query = useAuthorizedGet<SearchResult[]>(active ? `/api/search?query=${encodeURIComponent(term)}` : null);

    const close = () => {
        setOpen(false);
        setText("");
    }

    function renderResults() {
        if(query.data === undefined) {
            return query.isError ? 
                <p className="search__status" role="status">Search is unavailable right now.</p> : <p className="search__status" role="status">Searching...</p>;
        }

        if(query.data.length === 0) {
            return <p className="search__status" role="status">No users or companies matched "{term}".</p>;
        }

        return(
            <ul className="search__list">
                {query.data.map((result) => (
                    <li key={`${result.searchResultType}-${result.key}`}>
                        <Link className="search__item" to={result.searchResultType === "user" ? `/profile/${result.key}` : `/company/${result.key}`} onClick={close}>
                            <span className="search__identity">
                                <img className={result.searchResultType === "user" ? "search__image search__image--round" : "search__image"} src={assetUrl(result.imageLocation)} alt="Profile image / Company logo" />
                                <span className="search__name">{result.name}</span>
                            </span>
                            <span className="search__type">{result.searchResultType === "user" ? "User" : "Company"}</span>
                        </Link>
                    </li>
                ))}
            </ul>
        );
    }

    const showResults = open && active;

    return(
        <div className="search" 
            onBlur={(event) => {
                if(!event.currentTarget.contains(event.relatedTarget)){
                    setOpen(false);
                }}}
            onKeyDown={(event) => {
                if(event.key === "Escape"){
                    setOpen(false);
                }
            }}
        >
            <input className="search__input" type="search" placeholder="Search users and companies" aria-label="Search users and companies" aria-controls={showResults ? listId : undefined} autoComplete="off" value={text} 
                onChange={(event) => {
                    setText(event.target.value);
                    setOpen(true);
                }}
                onFocus={() => setOpen(true)} 
            />
            {showResults && (
                <div id={listId} className="search__results" onMouseDown={(event) => event.preventDefault()}>
                    {renderResults()}
                </div>
            )}
        </div>
    );
}