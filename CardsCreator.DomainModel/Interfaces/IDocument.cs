using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardsCreator.DomainModel.Interfaces
{
    public interface IDocument
    {
        void SaveToTempFile(string filename);
        byte[] GetCompleteDocument();
    }
}
