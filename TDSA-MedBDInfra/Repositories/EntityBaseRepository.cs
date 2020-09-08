using System;
using Microsoft.EntityFrameworkCore;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDInfra.Repositories {
  public class EntityBaseRepository<TEntity>
  : IEntityBaseRepository<TEntity> where TEntity : class {
    protected readonly TDSAMedBDContext Db;
    protected readonly DbSet<TEntity> DbSet;

    public EntityBaseRepository(TDSAMedBDContext context) {
      Db = context;
      DbSet = Db.Set<TEntity>();
    }

    public virtual void Add(TEntity obj) {
      DbSet.Add(obj);
    }

    public virtual void Update(TEntity obj) {
      DbSet.Update(obj);
    }

    public virtual void Save() {
      Db.SaveChanges();
    }

    public void Dispose() {
      Db.Dispose();
      GC.SuppressFinalize(this);
    }
  }
}
