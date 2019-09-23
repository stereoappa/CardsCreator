using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TemplateEngine.Docx;

namespace CardsCreator.DomainModel
{
    public abstract class DocxTemplateBase : IDocument
    {
        private readonly string _docxPath;
        private readonly string _docxDirectory;

        protected DocxTemplateBase(string templatePath)
            : this(templatePath, Path.GetTempPath())
        {

        }

        protected DocxTemplateBase(string docxPath, string docxDirectory)
        {
            _docxPath = docxPath;
            _docxDirectory = docxDirectory;
        }

        protected abstract Content GetContent();

        public void SaveToTempFile(string filename)
        {
            var content = GetContent();

            File.Copy(_docxPath, filename, true);
            //filename.RemoveAttribute(FileAttributes.ReadOnly);

            using (var file = new TemplateProcessor(filename)
                .SetRemoveContentControls(true))
            {
                file.FillContent(content);
                file.SaveChanges();
            }
        }


        public byte[] GetCompleteDocument()
        {
            var tempFile = Path.Combine(_docxDirectory, Path.GetRandomFileName());
            SaveToTempFile(tempFile);
            var res = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);
            return res;
        }
    }
}
