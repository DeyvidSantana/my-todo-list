namespace MyTODOList.Repository
{
    public interface IRepository<T>
    {
        void Add<T>(T entidade) where T: class;
        void Update<T>(T entidade) where T : class;
        void Delete<T>(T entidade) where T : class;
        bool SaveChanges();        
    }
}
