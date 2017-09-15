using System;
using System.Threading.Tasks;

namespace Kevsoft.Ssml
{
    public interface ISsml
    {
        IFluentSay Say(string value);

        IFluentSayDate Say(DateTime value);

        IFluentSayTime Say(TimeSpan value);

        Task<string> ToStringAsync();

        IBreak Break();
    }
}