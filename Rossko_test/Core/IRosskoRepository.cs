using Rossko_test.Model;

namespace Rossko_test.Core
{
    public interface IRosskoRepository
    {
        void Insert(OptionsArray optionsArray);
        OptionsArray GetByKeyword(string keyword);
        bool AnyByKeyword(string keyword);
    }
}
