using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardsCreator.DomainModel
{
    public abstract class DocxDocument : IDocument
    {
        public string Path { get; set; }

        MemoryStream _ms = new MemoryStream();
        public DocxDocument(string path)
        {
            Path = path;
        }

        public abstract MemoryStream GetContent();

        public byte[] GetBytes()
        {
            return _ms.ToArray();
        }

        public MemoryStream GetStream()
        {
            using (var fs = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.CopyTo(_ms);
                return _ms;
            }
        }
    }
}
