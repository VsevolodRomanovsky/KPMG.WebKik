import { DomesticCompanyViewModel } from './domestic-company.model';
import { ForeignCompanyViewModel } from './foreign-company.model';
import { ForeignLightCompanyViewModel } from './foreign-light-company.model';
import { IndividualCompanyViewModel } from './individual-company.model';
import { ProjectCompanyShareViewModel } from './company-share.model';
import { SupportingDocumentsViewModel } from './supporting-documents.model';
import { State } from './state.model';

export
    class ProjectCompanyViewModel {
    public Id: number;
    public Name: string;
    public State: State;
    public IsResident: boolean;
    public DomesticCompany: DomesticCompanyViewModel;
    public ForeignCompany: ForeignCompanyViewModel;
    public ForeignLightCompany: ForeignLightCompanyViewModel;
    public IndividualCompany: IndividualCompanyViewModel;
    public ProjectId: number;
	public OwnerProjectCompanyShares: Array<ProjectCompanyShareViewModel>;
	public SupportingDocuments: Array<SupportingDocumentsViewModel>;
    public StateName: string;

    constructor() {
        this.Id = 0;
        this.Name = "";
        this.State = State.Domestic;
        this.IsResident = false;
    }
}