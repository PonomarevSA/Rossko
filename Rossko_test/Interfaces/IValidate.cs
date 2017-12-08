using System;

namespace Rossko_test.Interfaces
{
    public interface IValidate
    {
        (bool, string) Validate(Char[] input);
    }
}
