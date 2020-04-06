using System;

namespace Kevsoft.Ssml
{
    public interface IFluentSay : ISsml
    {
        IFluentSay AsAlias(string alias);
        IFluentSay AsVoice(string name);

        IFluentSay Emphasised();

        IFluentSay Emphasised(EmphasiseLevel level);

        IFluentSay AsTelephone();

        IFluentSayAsCharacters AsCharacters();
    }
}