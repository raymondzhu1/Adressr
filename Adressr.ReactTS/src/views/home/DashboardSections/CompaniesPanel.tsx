import { Link } from "react-router";
import { assetUrl } from "../../../client";
import type { DashboardCompanyResponse } from "../../../types/Company/company.types";
import { DataPanel } from "./DataPanel";

export function CompaniesPanel() {
    return(
        <DataPanel<DashboardCompanyResponse[]> 
            title="Owned Companies" 
            section="Companies" 
            path="/api/company/dashboard" 
            action={<Link className="panel__action" to="/company/create">Create new company</Link>} 
            isEmpty={(companies) => companies.length === 0}
            empty={<p className="panel__empty">No owned companies.</p>}
        >
            {(companies) => (
                <ul className="companies">
                    {companies.map((company) => (
                        <li key={company.companyID}>
                            <Link className="company" to={`/companies/${company.companyID}`}>
                                <img className="company__icon" src={assetUrl(company.companyIconImageLocation)} alt="company logo here"/>
                                <span className="company__name">{company.name}</span>
                            </Link>
                        </li>
                    ))}
                </ul>
            )}
        </DataPanel>
    );
}