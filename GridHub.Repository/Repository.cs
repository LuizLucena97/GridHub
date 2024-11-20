using GridHub.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using GridHub.Database;

namespace GridHub.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FIAPDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(FIAPDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Adicionar uma nova entidade
        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
            }

            _context.Add(entity);
            _context.SaveChanges();
        }

        // Remover uma entidade
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
            }

            _context.Remove(entity);
            _context.SaveChanges();
        }

        // Atualizar uma entidade existente
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Obter todas as entidades
        public IEnumerable<T> GetAll()
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet não está inicializado.");
            }

            return _dbSet.ToList();
        }

        // Obter uma entidade pelo ID
        public T GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "O ID não pode ser nulo.");
            }

            return _dbSet.Find(id);
        }

        T IRepository<T>.Add(T entity)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
