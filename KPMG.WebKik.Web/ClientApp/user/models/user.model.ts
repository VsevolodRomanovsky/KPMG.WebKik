import { RoleViewModel } from './role.model';

export
    class UserViewModel {
    public Id: number;
    public UserLogin: string;
    public DisplayName: string;
    public RoleId: number;
    public Role: RoleViewModel;
    public IsDisabled: boolean;
}
