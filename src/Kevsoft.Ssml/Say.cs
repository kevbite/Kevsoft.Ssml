using System.Threading.Tasks;
using System.Xml;

namespace Kevsoft.Ssml
{
    public class Say : ISay, ISsmlWriter
    {
        private ISsmlWriter _ssmlWriter;
        private readonly ISsml _ssml;

        public Say(string value, ISsml ssml)
        {
            _ssmlWriter = new PlainTextWriter(value);;
            _ssml = ssml;
        }

        public async Task WriteAsync(XmlWriter xml)
        {
            await _ssmlWriter.WriteAsync(xml)
                .ConfigureAwait(false);
        }

        public ISsml AsAlias(string alias)
        {
            _ssmlWriter = new SubWriter(_ssmlWriter, alias);

            return this;
        }

        public ISsml Emphasised()
        {
            return Emphasised(EmphasiseLevel.NotSet);
        }

        public ISsml Emphasised(EmphasiseLevel level)
        {
            _ssmlWriter = new EmphasiseWriter(_ssmlWriter, level);

            return this;
        }

        ISay ISsml.Say(string value)
        {
            return _ssml.Say(value);
        }

        Task<string> ISsml.ToStringAsync()
        {
            return _ssml.ToStringAsync();
        }
            
        IBreak ISsml.Break()
        {
            return _ssml.Break();
        }
    }
}