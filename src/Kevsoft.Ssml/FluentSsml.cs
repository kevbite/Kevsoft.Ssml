using System;
using System.Threading.Tasks;

namespace Kevsoft.Ssml
{
    public abstract class FluentSsml : ISsml
    {
        private readonly ISsml _ssmlImplementation;

        protected FluentSsml(ISsml ssmlImplementation)
        {
            _ssmlImplementation = ssmlImplementation;
        }

        IFluentSay ISsml.Say(string value)
        {
            return _ssmlImplementation.Say(value);
        }

        IFluentSayDate ISsml.Say(DateTime value)
        {
            return _ssmlImplementation.Say(value);
        }

        Task<string> ISsml.ToStringAsync()
        {
            return _ssmlImplementation.ToStringAsync();
        }

        IBreak ISsml.Break()
        {
            return _ssmlImplementation.Break();
        }
    }
}