import { DirectoryEntryViewModel } from '../../directory/models/directory-entry.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';

export
    class TaxReturnViewModel {
    public Id: number;
    public CreatedDate: Date;
    public ProjectCompanyId: number;
    public ProjectCompany: ProjectCompanyViewModel;
    public Correction: number;
    public Year: number;

    constructor() {
        this.CreatedDate = new Date();
        this.Correction = 0;
    }
}