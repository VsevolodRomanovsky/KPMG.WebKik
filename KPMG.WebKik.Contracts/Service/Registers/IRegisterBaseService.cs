namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface  IRegisterBaseService<TEntity>
    {
        TEntity CalculateRegisterFields(TEntity register);
    }
}
