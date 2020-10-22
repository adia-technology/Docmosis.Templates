using System;

namespace Docmosis.Templates
{
    public class DocmosisTemplatesError : Exception
    {
        public DocmosisTemplatesError(string receivedInvalidResponseFromDocmosis) : base(receivedInvalidResponseFromDocmosis)
        {
        }
    }
}