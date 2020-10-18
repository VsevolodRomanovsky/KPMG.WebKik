import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';
import { Register10SectionViewModel } from '../models/register10section.model';


export
class Register10ViewModel {

    public Id: number;
    public Type: RegisterType;
    public Year: number;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;
    public Register10Data: Register10SectionViewModel[]; 

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
        this.Register10Data = new Array<Register10SectionViewModel>();
    }
}