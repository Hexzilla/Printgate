using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;

namespace Printgate.Model
{
    public class Printers : BaseModel
    {
        private GateSettings settings;

        public Printers(GateSettings settings)
        {
            this.settings = settings;
        }

        public static List<string> GetPrinterList()
        {
            var localPrintServer = new PrintServer();
            var printQueues = localPrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            var printerList = (from printer in printQueues select printer).ToList();

            var resultItems = new List<string>();
            foreach (var printer in printerList)
            {
                resultItems.Add(printer.Name);
            }
            return resultItems;
        }

        public async Task<bool> Print(string printerName)
        {
            // Create a FlowDocument dynamically.  
            FlowDocument doc = CreateFlowDocument();
            doc.Name = "FlowDoc";

            // Create IDocumentPaginatorSource from FlowDocument  
            IDocumentPaginatorSource idpSource = doc;

            // Create a PrintDialog  
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintQueue = new PrintQueue(new PrintServer(), printerName);
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
            return true;
        }
        
        private FlowDocument CreateFlowDocument()
        {
            Paragraph myParagraph = new Paragraph();

            // Add some Bold text to the paragraph
            myParagraph.Inlines.Add(new Bold(new Run("Some bold text in the paragraph.")));

            // Add some plain text to the paragraph
            myParagraph.Inlines.Add(new Run(" Some text that is not bold."));

            // Create a List and populate with three list items.
            List myList = new List();

            // First create paragraphs to go into the list item.
            Paragraph paragraphListItem1 = new Paragraph(new Run("ListItem 1"));
            Paragraph paragraphListItem2 = new Paragraph(new Run("ListItem 2"));
            Paragraph paragraphListItem3 = new Paragraph(new Run("ListItem 3"));

            // Add ListItems with paragraphs in them.
            myList.ListItems.Add(new ListItem(paragraphListItem1));
            myList.ListItems.Add(new ListItem(paragraphListItem2));
            myList.ListItems.Add(new ListItem(paragraphListItem3));

            var document = new FlowDocument();
            document.Blocks.Add(myParagraph);
            document.Blocks.Add(myList);
            return document;
        }

        public bool PrintXmlFile(string fileName, object data, Type type)
        {
            try
            {
                var writer = new System.Xml.Serialization.XmlSerializer(type);

                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"//{fileName}";
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, data);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public async Task<bool> PrintTableReservationData(TableReservationPrintData data)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fffffff");
            string fileName = $"{datetime}-TableReservation.xml";
            PrintXmlFile(fileName, data, typeof(TableReservationPrintData));

            await Print(settings.TablePrinter);
            await Print(settings.BeautifulPrinter);

            return true;
        }

        public bool PrintTakeAwayReservationData(TakeAwayReservationPrintData data)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fffffff");
            string fileName = $"{datetime}-TakeAwayReservation.xml";
            PrintXmlFile(fileName, data, typeof(TakeAwayReservationPrintData));
            return true;
        }
    }
}
