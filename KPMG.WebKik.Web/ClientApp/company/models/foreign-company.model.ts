import { ProjectCompanyViewModel } from './project-company.model';
import { ContryCodeViewModel } from './country-code.model';

export
    class ForeignCompanyViewModel {
    public Id: number;
    public Number: string;
    public Name: string;
    public FullName: string;
    public CountryCodeId: number;
    public CountryCode: ContryCodeViewModel;
    public RegistrationNumber: string;
    public TaxPayerCodeId?: number;
    public Address: string;
    public Company: ProjectCompanyViewModel;
    public FoundDate: Date;
}