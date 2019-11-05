using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.SIMLIB
{
    public class PdfLib
    {
        public void AddTexto(PdfWriter writer,pdfLibText Texto)
        {

            Phrase Field = new Phrase(Texto.Text);
            Field.Font.SetFamily(Texto.FontName);
            Field.Font.Size = Texto.FontSize;
            Field.Font.Color = new BaseColor(Texto.FontColor);
            Field.Font.SetStyle(Texto.FontStyle);
            PdfContentByte Content = writer.DirectContent;
            ColumnText.ShowTextAligned(Content, Element.ALIGN_LEFT, Field, Texto.X, Texto.Y, 0);
        }

        public void addCell(PdfPTable table, pdfLibCell celula)
        {
            PdfPCell cell = new PdfPCell();
            if (celula.Picture==null)
            {
                //Font fFont = fFont = new Font(Font.GetFamilyIndex(celula.FontName), celula.FontSize, celula.FontStyle);
                Phrase phrase = new Phrase(" " + celula.Text);
                phrase.Font.SetFamily(celula.FontName);
                phrase.Font.Size = celula.FontSize;
                phrase.Font.Color = new BaseColor(celula.FontColor);
                phrase.Font.SetStyle(celula.FontStyle);
                cell = new PdfPCell(phrase);
            }
            else
            {
                cell = new PdfPCell(celula.Picture,true);
            }

            
            cell.Colspan = celula.colspan;
            cell.BorderWidthTop = BorderScale(celula.BorderTop);
            cell.BorderWidthBottom = BorderScale(celula.BorderBottom);
            cell.BorderWidthLeft = BorderScale(celula.BorderLeft);
            cell.BorderWidthRight = BorderScale(celula.BorderRight);
            cell.NoWrap = false;
            if (celula.Height > 0)
            {
                cell.FixedHeight = celula.Height;
            }
            else
            {
                cell.MinimumHeight = 15f;
            }
            
            cell.HorizontalAlignment = celula.Align;
            cell.VerticalAlignment = celula.VerticalAlign;
            cell.BackgroundColor = new BaseColor(celula.Background);
            cell.PaddingLeft = celula.PaddingLeft;
            cell.PaddingRight= celula.PaddingRight;
            cell.PaddingTop= celula.PaddingTop;
            cell.PaddingBottom= celula.PaddingBottom;


            table.AddCell(cell);
        }
        public void addBorder(PdfWriter ww, Document dd)
        {
            PdfContentByte Content = ww.DirectContent;
            Rectangle pageBorderRect = new Rectangle(dd.PageSize);
            pageBorderRect.Left += dd.LeftMargin;
            pageBorderRect.Right -= dd.RightMargin;
            pageBorderRect.Top -= dd.TopMargin;
            pageBorderRect.Bottom += dd.BottomMargin + 20;
            Content.SetColorStroke(BaseColor.BLACK);
            Content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
            Content.Stroke();
        }
        public void addLogo(Document dd, pdfLibLogo Lg)
        {
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Lg.Path);
            logo.SetAbsolutePosition(Lg.X, Lg.Y);
            logo.ScalePercent(Lg.Scale);
            dd.Add(logo);
        }
        public float BorderScale(float param)
        {
            return param / 8;
        }
        public PdfPTable CreateTable(float[] CellWidths)
        {
            PdfPTable tb = new PdfPTable(CellWidths.Length);
            float width = 0;
            for (int i = 0; i < CellWidths.Count(); i++)
            {
                width += CellWidths[i];
            }
            tb.TotalWidth = width;
            tb.SetWidths(CellWidths);
            return tb;
        }
        public void AddRectangle(PdfWriter ww,  float posX,float posY,float pWidth ,float pHeigth)
        {
            PdfContentByte Content = ww.DirectContent;
            Content.SetColorStroke(BaseColor.BLACK);
            Content.Rectangle(posX,posY,pWidth,pHeigth)   ;
            Content.Stroke();


        }
    }
    public class pdfLibCell
    {
        public String Text { get; set; }
        public int colspan { get; set; } = 0;
        public int FontStyle { get; set; } = iTextSharp.text.Font.NORMAL;
        public float BorderTop { get; set; } = 1;
        public float BorderBottom { get; set; } = 1;
        public float BorderLeft { get; set; } = 1;
        public float BorderRight { get; set; } = 1;
        public int Align { get; set; } = PdfPCell.ALIGN_CENTER;
        public int VerticalAlign { get; set; } = PdfPCell.ALIGN_MIDDLE;
        public String FontName { get; set; } = "verdana";
        public int FontSize { get; set; } = 9;
        public System.Drawing.Color Background { get; set; } = System.Drawing.Color.White;
        public System.Drawing.Color FontColor { get; set; } = System.Drawing.Color.Black;
        public Image Picture { get; set; }
        public float PaddingTop { get; set; } = 2f;
        public float PaddingBottom{ get; set; } = 2f;
        public float PaddingLeft{ get; set; } = 2f;
        public float PaddingRight{ get; set; } = 2f;

        public float Height { get; set; } 
    }
    public class pdfLibText
    {
        public float X { get; set; }
        public float Y { get; set; }
        public String Text { get; set; }
        public String FontName { get; set; } = "verdana";
        public float FontSize { get; set; } = 9;
        public int FontStyle { get; set; } = iTextSharp.text.Font.NORMAL;
        public System.Drawing.Color FontColor { get; set; } = System.Drawing.Color.Black;
    }
    public class pdfLibLogo
    {
        public float X { get; set; }
        public float Y { get; set; }
        public String Path{ get; set; }
        public float Scale { get; set; } = 100;
    }
    public class TableTemplate
    {
        public float Size { get; set; }
        public String Header { get; set; }
        public String Field { get; set; }
        public int Align { get; set; } = PdfPCell.ALIGN_CENTER;
    }
}