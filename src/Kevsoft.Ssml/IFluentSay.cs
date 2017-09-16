using System;

namespace Kevsoft.Ssml
{
    public interface IFluentSay : ISsml
    {
        ISsml AsAlias(string alias);

        ISsml Emphasised();

        ISsml Emphasised(EmphasiseLevel level);

        ISsml AsTelephone();

        IFluentSayAsCharaters AsCharacters();
    }
}