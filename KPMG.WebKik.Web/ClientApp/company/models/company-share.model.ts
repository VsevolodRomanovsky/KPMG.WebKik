import { ProjectCompanyViewModel } from './project-company.model';
import { ShareType } from './share-type';
import { GroundsType } from './grounds-type.model';
import { TaxExemtionsViewModel } from './tax-exemption.model'
export 
    class ProjectCompanyShareViewModel{
    public Id: number;
    public CompanyStatus: string;
    public ShareType: ShareType;
    public ShareTypeName: string;
    public SharePart: number;
    public ShareStartDate: Date;
    public ShareFinishDate: Date;
    public ShareWithResidentsPart: number;
    public ShareWithFamilyPart: number;
    public IsFounder: boolean;
    public IsControlledBy: boolean;
    public FoundDate: Date;
    public IsOwnInterest: boolean;
    public IsChildInterest: boolean;
    public IsPartnerInterest: boolean;
    public ControlGrounds: string;
    public ControlEmergenceDate: Date;
    public ForeignLightCompanyGrounds: GroundsType;
    public ParticipantStatus: string;
    public IsIndependentRecognition: boolean;

    public IsKIKCompany: boolean;
    public IsExemptFromTaxes: boolean;

    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;
    public DependentProjectCompany: ProjectCompanyViewModel;
    public DependentProjectCompanyId: number;

    public TaxExemptions: Array<TaxExemtionsViewModel>;

    constructor() {
        this.Id = 0;
        this.ShareType = ShareType.Direct;
    }
}