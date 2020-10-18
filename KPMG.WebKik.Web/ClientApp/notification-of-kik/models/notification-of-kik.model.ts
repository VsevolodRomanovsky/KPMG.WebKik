import { DirectoryEntryViewModel } from '../../directory/models/directory-entry.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';

export
    class NotificationOfKIKViewModel {
    public Id: number;
    public CreatedDate: Date;
    public ProjectCompanyId: number;
    public ProjectCompany: ProjectCompanyViewModel;
    public Correction: number;
    public Year: number;
    public TaxAuthorityCode: number;
    

    constructor() {
        this.CreatedDate = new Date();
        this.Correction = 0;
    }
}