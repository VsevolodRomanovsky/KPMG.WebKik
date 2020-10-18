import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';
import { Register9DataViewModel } from '../models/register9data.model';

export
    class Register9ViewModel {

    public Id: number;
    public Type: RegisterType;
    public Year: number;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  
	public Register9Data: Register9DataViewModel[];
	public Summary: number;
	public SummaryYear: number;
	public CurrentYearDividendSumAggr: number;
	public CurrentYearTransitionalDividendSumAggr: number;
	public LastYearDividendSumAggr: number;

	constructor() {
		this.OwnerProjectCompany = new ProjectCompanyViewModel();
		this.Register9Data = new Array<Register9DataViewModel>();
    }
}