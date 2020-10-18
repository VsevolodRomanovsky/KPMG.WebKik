import { ProjectCompanyViewModel } from './project-company.model';

export
    class DomesticCompanyViewModel {
    public Id: number;
    public Number: string;
    public FullName: string;
    public OGRN: number;
    public INN: number;
    public KPP: string;
    public IsPublic: boolean;
    public Company: ProjectCompanyViewModel;
}
