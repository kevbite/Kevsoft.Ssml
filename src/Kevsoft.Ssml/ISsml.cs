using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kevsoft.Ssml
{
    public interface ISsml
    {
        ISay Say(string value);

        Task<string> ToStringAsync();

        IBreak Break();
    }
}