using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app.repository.implementation
{
    internal class ManagementRepositoryImpl : IManagementRepository
    {
        SampleContextFactory dbContextFactory = new SampleContextFactory();

        public Management? DeleteById(int id)
        {
            using(var db = dbContextFactory.CreateDbContext(null))
            {
                var management = db.Managements.Find(id);
                if (management != null)
                {
                    db.Remove(management);
                    db.SaveChanges();
                }
                return management;
            }
        }

        public List<Management> FindAll()
        {
            using (var db = dbContextFactory.CreateDbContext(null))
            {
                var managements = db.Managements.ToList();
                return managements;
            }
        }

        public Management? FindById(int id)
        {
            using (var db = dbContextFactory.CreateDbContext(null))
            {
                var management = db.Managements.Find(id);
                return management;
            }
        }

        public List<Management> FindByName(string name)
        {
            using (var db = dbContextFactory.CreateDbContext(null))
            {
                var managements = db.Managements.Where(x => x.Name == name).ToList();
                return managements;
            }
        }

        public Management Save(Management entity)
        {
            using (var db = dbContextFactory.CreateDbContext(null))
            {
                db.Managements.Add(entity);
                db.SaveChanges();
                return entity;
            }
        }
    }
}
