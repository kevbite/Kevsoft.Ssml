using System;
using System.Threading.Tasks;

namespace Kevsoft.Ssml
{
    public interface ISsml
    {
        IFluentSay Say(string value);

        IFluentSayDate Say(DateTime value);

        Task<string> ToStringAsync();

        IBreak Break();
    }
}