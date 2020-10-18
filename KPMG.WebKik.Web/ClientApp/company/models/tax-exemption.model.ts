import { RationalyType } from './rationaly-type.model';
import { ProjectCompanyShareViewModel } from './company-share.model';

export
    class TaxExemtionsViewModel {
    public Id: number;
    public Rationaly: Array<RationalyType>;
    public Year: number;
    public OwnerProjectCompanyId: number;
    public DependentProjectCompanyId: number;
    public Result: boolean;    
}