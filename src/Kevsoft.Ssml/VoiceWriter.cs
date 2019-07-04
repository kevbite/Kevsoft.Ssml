using System.Threading.Tasks;
using System.Xml;

namespace Kevsoft.Ssml
{
    public class VoiceWriter : ISsmlWriter
    {
        private readonly ISsmlWriter _innerWriter;
        private readonly string _name;

        public VoiceWriter(ISsmlWriter innerWriter, string name)
        {
            _innerWriter = innerWriter;
            _name = name;
        }

        public async Task WriteAsync(XmlWriter writer)
        {
            await writer.WriteStartElementAsync(null, "voice", null)
                .ConfigureAwait(false);

            await writer.WriteAttributeStringAsync(null, "name", null, _name)
                .ConfigureAwait(false);

            await _innerWriter.WriteAsync(writer)
                .ConfigureAwait(false);

            await writer.WriteEndElementAsync()
                .ConfigureAwait(false);
        }
    }
}
