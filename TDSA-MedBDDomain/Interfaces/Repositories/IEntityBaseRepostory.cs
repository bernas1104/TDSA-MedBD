using System;

namespace TDSA_MedBDDomain.Interfaces.Repositories {
  public interface IEntityBaseRepository<TEntity> : IDisposable
  where TEntity : class {
    void Add(TEntity obj);
    void Update(TEntity obj);
    void Save();
  }
}
