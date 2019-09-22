using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TemplateEngine.Docx;

namespace CardsCreator.DomainModel
{
    public class CardsSheetDocx : DocxDocument
    {
        public CardsSheetDocx() : base(@"C:\DevelopmentProjects\CardsCreator\CardsCreator\CardsCreator\wwwroot\templates\CardsSheet.docx")
        {

        }

        public override MemoryStream GetContent() 
        {
            //var valuesToFill = new Content(
            //    new FieldContent("Report date", DateTime.Now.Date.ToString()),
            //    new TableContent("Team members")
            //    .AddRow(
            //        new FieldContent("SideOne:Text", "Семёнов Илья Васильевич"))
            //    .AddRow(
            //        new FieldContent("SideTwo:Text", "Артемьев Вячеслав Геннадьевич"))
            //);

            var valuesToFill = new Content(new FieldContent("SideOne:Text", "hello"));

            MemoryStream ms = new MemoryStream();

            using (var outputDocument = new TemplateProcessor(Path)
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.Document.Save(ms);
            }

            return ms;
        }
    }
}
