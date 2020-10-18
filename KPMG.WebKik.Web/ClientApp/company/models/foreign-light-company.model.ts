import { ProjectCompanyViewModel } from './project-company.model';
import { ContryCodeViewModel } from './country-code.model';

export
    class ForeignLightCompanyViewModel {
    public Id: number;
    public Number: string;
    public ForeignOrganizationalFormCodeId: number;
    public RussianName: string;
    public EnglishName: string;
    public CountryCode: ContryCodeViewModel;
    public CountryCodeId: number;
    public RequisitesEng: string;
    public RequisitesRus: string;
    public FoundDate: Date;
    public RegNumber: string;
    public OtherInfo: string;
    public Company: ProjectCompanyViewModel;
}