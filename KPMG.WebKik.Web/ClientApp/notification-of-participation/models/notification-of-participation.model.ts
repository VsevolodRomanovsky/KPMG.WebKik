import { DirectoryEntryViewModel } from '../../directory/models/directory-entry.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';

export
    class NotificationOfParticipationViewModel {
    public Id: number;
    public CreatedDate: Date;
    public ProjectCompanyId: number;
    public ProjectCompany: ProjectCompanyViewModel;
    public Correction: number;
    public SignatoryId: number;
    public SubmissionGroundId: number;
    public SubmissionGround: DirectoryEntryViewModel;
    public SubmissionDate?: Date;

    constructor() {
        this.SubmissionGroundId = 1;
        this.CreatedDate = new Date();
        this.Correction = 0;
    }
}
