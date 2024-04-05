using App.ApplicationLayer.Contexts;

namespace App.ApplicationLayer.Services
{
    public interface IReadService
    {
        IEnumerable<string> GetNames();
        IEnumerable<string> GetDescs();
    }

    public class ReadService : IReadService
    {
        private readonly RidelTestContext _db;

        public ReadService(
          RidelTestContext ridelContext

          )
        {
            _db = ridelContext;

        }

        public IEnumerable<string> GetDescs()
        {
            return _db.Table2s.Select(p => p.Desc);
        }

        public IEnumerable<string> GetNames()
        {
           return _db.Table1s.Select(p => p.Name);
        }
    }
}
