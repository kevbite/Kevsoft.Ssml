using System;
using System.Threading.Tasks;
using System.Xml;

namespace Kevsoft.Ssml
{
    public interface IFluentSay : ISsml
    {
        ISsml AsAlias(string alias);

        ISsml Emphasised();

        ISsml Emphasised(EmphasiseLevel level);

        ISsml AsTelephone();

        ISsml AsCharacters();
    }
}