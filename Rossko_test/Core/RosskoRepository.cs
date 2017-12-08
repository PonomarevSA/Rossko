using System.Linq;
using Rossko_test.Model;

namespace Rossko_test.Core
{
    public class RosskoRepository : IRosskoRepository
    {
        private RosskoContext db;

        public RosskoRepository(RosskoContext context)
        {
            db = context;
        }

        public bool AnyByKeyword(string keyword)
        {
            return db.OptionsArrays.Any(p => p.Keyword == keyword);
        }

        public OptionsArray GetByKeyword(string keyword)
        {
            return db.OptionsArrays.FirstOrDefault(p => p.Keyword == keyword);
        }

        public void Insert(OptionsArray optionsArray)
        {
            db.OptionsArrays.Add(optionsArray);
            db.SaveChanges();
        }
    }
}
