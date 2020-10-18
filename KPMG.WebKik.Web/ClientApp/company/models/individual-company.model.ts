import { ProjectCompanyViewModel } from './project-company.model';
import { DocumentInformationViewModel } from './document-information.model';
import { DocumentConfirmationViewModel } from './document-confirmation.model';

export
    class IndividualCompanyViewModel {
        public Id: number;
        public ProjectCompany: ProjectCompanyViewModel;

        public INN: number;
        public Surname: string; 
        public Name: string;
        public MiddleName: string;
        public BirthDate: Date;
        public GenderCodeId?: number;
        public BirthPlace: string;
        public CitizenshipCodeId?: number;

        public VerifedPersonalityDocInfoId: number;
        public VerifedPersonalityDocInfo: DocumentInformationViewModel;
        public ConfirmedPersonalityDocInfoId: number;
        public ConfirmedPersonalityDocInfo: DocumentConfirmationViewModel;

        public RussianLocationCodeId: number;
        public RegionCodeId : number;
        public PostIndex: string;
        public District :string;
        public City: string;
        public CityType: string; 
        public Street: string;
        public HouseNumber: string;
        public BuildingNumber: string; 
        public AppartamentNumber: string;
        public ForeignCountryCodeId?: number; 
        public ForeignAddress: string;

        constructor() {
            this.VerifedPersonalityDocInfo = new DocumentInformationViewModel();
            this.ConfirmedPersonalityDocInfo = new DocumentConfirmationViewModel();
        }
}