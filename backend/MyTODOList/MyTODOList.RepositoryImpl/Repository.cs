using MyTODOList.Data;
using MyTODOList.Repository;

namespace MyTODOList.RepositoryImpl
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add<T>(T entidade) where T : class
        {
            _dataContext.Add<T>(entidade);
        }

        public void Update<T>(T entidade) where T : class
        {
            _dataContext.Update<T>(entidade);
        }

        public void Delete<T>(T entidade) where T : class
        {
            _dataContext.Remove<T>(entidade);
        }

        public bool SaveChanges()
        {
            return (_dataContext.SaveChanges() > 0);
        }        
    }
}
