using Billing.Database;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Billing.Api.Helpers.PDF
{
    public class InvoicePdf : BasicPdf
    {
        private Invoice invoice;

        public InvoicePdf(Invoice _invoice)
        {
            invoice = _invoice;
        }

        public override Document CreateDocument()
        {
            // Create a new MigraDoc document
            Document = new Document();
            Document.Info.Title = "Invoice " + invoice.InvoiceNo;
            Document.Info.Subject = "Invoice " + invoice.Date;
            Document.Info.Author = invoice.Agent.Name;

            DefineStyles();

            CreatePage();

            FillContent();

            return Document;

        }

        protected override void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = Document.AddSection();

            // Create header
            Table = section.AddTable();
            Table.Style = "Table";
            Table.Borders.Width = 0.25;
            Table.Borders.Left.Width = 0.5;
            Table.Borders.Right.Width = 0.5;
            Table.Rows.LeftIndent = 0;


            Column column = Table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = Table.AddColumn("8cm");
            column.LeftPadding = "3cm";
            column.RightPadding = "2cm";
            column.Borders.Visible = false;

            column = Table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            Row row = Table.AddRow();
            row.TopPadding = 10;
            row.BottomPadding = 10;
            row.Cells[0].AddParagraph("Customer");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].Format.Font.Size = 14;

            row.TopPadding = 10;
            row.BottomPadding = 5;
            row.Cells[2].AddParagraph("Invoice");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].Format.Font.Size = 14;

            row = Table.AddRow();
            row.TopPadding = 10;
            row.BottomPadding = 10;
            row.Cells[0].AddParagraph(invoice.Customer.Name);
            row.Cells[0].AddParagraph();
            row.Cells[0].AddParagraph(invoice.Customer.Address);
            row.Cells[0].AddParagraph(invoice.Customer.Town.Zip + " " + invoice.Customer.Town.Name);
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;



            row.TopPadding = 10;
            row.BottomPadding = 5;
            row.Height = "2cm";
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[2].AddParagraph(invoice.InvoiceNo);
            row.Cells[2].Format.Font.Bold = false;
            row.Cells[2].AddParagraph();
            row.Cells[2].AddParagraph(invoice.Date.ToShortDateString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;



            row = Table.AddRow();
            row.Borders.Visible = false;

            Table = section.AddTable();
            Table.Style = "Table";
            Table.Borders.Width = 0.25;
            Table.Borders.Left.Width = 0.5;
            Table.Borders.Right.Width = 0.5;
            Table.Rows.LeftIndent = 0;
            Table.TopPadding = 5;
            Table.BottomPadding = 5;


            // Before you can add a row, you must define the columns
            //1
            column = Table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //2    
            column = Table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //3
            column = Table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //4
            column = Table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //5
            column = Table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Create the header of the table
            row = Table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("Customer Id");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("Sales Person");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].AddParagraph("Ordered");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].AddParagraph("Shipped");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].AddParagraph("Shipped Via");

            DateTime OrderedDate = DateTime.Now;
            foreach (var item in invoice.History)
            {
                DateTime Date;
                if (item.Status == 0)
                {
                    Date = item.Date;
                    OrderedDate = Date;
                    break;
                }
            }
            row = Table.AddRow();
            row.Cells[0].AddParagraph(invoice.Customer.Id.ToString());
            row.Cells[1].AddParagraph(invoice.Agent.Name);
            row.Cells[2].AddParagraph(OrderedDate.ToShortDateString());
            if (invoice.ShippedOn != null)
                row.Cells[3].AddParagraph(invoice.ShippedOn.Value.ToShortDateString());
            else
                row.Cells[3].AddParagraph(DateTime.Now.ToShortDateString());
            if (invoice.Shipper != null)
                row.Cells[4].AddParagraph(invoice.Shipper.Name);



            row = Table.AddRow();
            row.Borders.Visible = false;

            // Create the item table
            Table = section.AddTable();
            Table.Style = "Table";
            Table.Borders.Width = 0.25;
            Table.Borders.Left.Width = 0.5;
            Table.Borders.Right.Width = 0.5;
            Table.Rows.LeftIndent = 0;
            Table.TopPadding = 5;
            Table.BottomPadding = 5;

            // Before you can add a row, you must define the columns
            //1
            column = Table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //2    
            column = Table.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //3
            column = Table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //4
            column = Table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //5
            column = Table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            //6
            column = Table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;


            // Create the header of the table
            row = Table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("Id");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("Product");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].AddParagraph("Unit");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].AddParagraph("Quantity");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].AddParagraph("Price");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].AddParagraph("SubTotal");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
        }

        protected override void FillContent()
        {
            Row row;
            int counter = 1;
            foreach (var item in invoice.Items)
            {
                row = Table.AddRow();
                row.TopPadding = 5;
                row.BottomPadding = 5;
                row.Cells[0].AddParagraph(counter.ToString());
                row.Cells[1].AddParagraph(item.Product.Name);
                row.Cells[2].AddParagraph(item.Product.Unit);
                row.Cells[3].AddParagraph(item.Quantity.ToString());
                row.Cells[4].AddParagraph(item.Price.ToString());
                row.Cells[5].AddParagraph(item.SubTotal.ToString());

                counter++;
            }
            row = Table.AddRow();
            row.TopPadding = 10;
            row.Borders.Visible = false;
            row.Cells[4].AddParagraph("SubTotal:");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].AddParagraph(invoice.SubTotal.ToString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;

            row = Table.AddRow();
            row.Borders.Visible = false;
            row.Cells[4].AddParagraph("Vat:");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].AddParagraph(invoice.VatAmount.ToString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;

            row = Table.AddRow();
            row.Borders.Visible = false;
            row.Cells[4].AddParagraph("Shipping:");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].AddParagraph(invoice.Shipping.ToString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;

            row = Table.AddRow();
            row.Borders.Visible = false;
            row.Format.Font.Bold = true;
            row.Cells[4].AddParagraph("Total:");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].AddParagraph(invoice.Total.ToString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;

        }
    }
}