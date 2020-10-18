export
    class SignatoryViewModel {
    public Id: number;
    public ProjectCompanyId: number;
    public FirstName: string;
    public MiddleName: string;
    public LastName: string;
    public SignatoryName: string;
    public SignatoryCodeId: number;
    public PhoneNumber: string;
    public Email: string;
    public ConfirmationDocumentId?: number;
    public Inn: string;
    public get Fio(): string {
        return this.LastName + ' ' + this.FirstName[0] + '.' + this.MiddleName[0] + '.';
    }

    constructor(serverObject?: any) {
        if (!serverObject) {
            return;
        }
        Object.assign(this, serverObject);
    }
}
