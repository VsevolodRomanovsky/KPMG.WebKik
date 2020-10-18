import { ProjectCompanyViewModel } from './project-company.model';
import { ShareType } from './share-type';
import { GroundsType } from './grounds-type.model';
import { TaxExemtionsViewModel } from './tax-exemption.model'
export
    class ProjectCompanyFactShareViewModel {

    public DependentProjectCompany: ProjectCompanyViewModel;
    public ShareFactPart: number;
    public ShareFactDirectPart: number;
    public ShareFactIndirectPart: number;    
}