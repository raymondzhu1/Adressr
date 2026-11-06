export interface SearchResult {
    searchResultType: "user" | "company";
    key: string;
    name: string;
    imageLocation: string;
}