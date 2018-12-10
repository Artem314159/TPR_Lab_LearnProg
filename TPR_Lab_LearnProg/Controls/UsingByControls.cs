using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace TPR_Lab_LearnProg.Controls
{
    public static class UsingByControls
    {
        public static void InitMatrix(this TableLayoutPanel tblLayPnl, string matrName, double[,] matrArray)
        {
            int m = matrArray.GetLength(0),
                n = matrArray.GetLength(1);

            Label matrNameLbl = new Label()
            {
                Text = matrName + ":",
                Font = new Font("Calibri", 18),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            tblLayPnl.Controls.Add(matrNameLbl, 0, 1);
            tblLayPnl.SetRowSpan(matrNameLbl, m);

            for (int i = 0; i < m + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    Label l = new Label()
                    {
                        //Text = matrA[i - 1, j - 2].ToString(),
                        Font = new Font("Calibri", 12),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    if (i == 0)
                    {
                        if (j == 1)
                            continue;
                        l.Text = "β" + (j - 1);
                    }
                    else
                    {
                        if (j == 1)
                        {
                            l.Text = "α" + i;
                        }
                        else
                        {
                            l.Text = matrArray[i - 1, j - 2].ToString();
                            l.Font = new Font("Calibri", 20);
                            l.BorderStyle = BorderStyle.FixedSingle;
                            l.Margin = new Padding(0);
                        }
                    }
                    tblLayPnl.Controls.Add(l, j, i);
                }
            }
        }

        public static void RTxtBoxLoad(this RichTextBox RTxtBox, string tabPageName, string xmlDocPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlDocPath);
            RTxtBox.Clear();
            XmlNodeList xmlLineNodes = xmlDoc.SelectSingleNode($"/main/{tabPageName}/{RTxtBox.Name}").SelectNodes("line");
            int count = xmlLineNodes.Count;
            string[] lines = new string[count];

            // not possible in one FOR because RichTextBox has improper property Lines
            for (int i = 0; i < count; i++)
            {
                lines[i] = xmlLineNodes[i].InnerText.Replace("\r\n       ", "");
            }
            RTxtBox.Lines = lines;
            for (int i = 0; i < count; i++)
            {
                RTxtBox.Select(RTxtBox.GetFirstCharIndexFromLine(i), xmlLineNodes[i].InnerText.Length);
                RTxtBox.SetStyleFromXml(xmlLineNodes[i].Attributes);
            }
        }

        public static void SetStyleFromXml(this RichTextBox RTxtBox, XmlAttributeCollection styles)
        {
            string fontName = styles["font_name"] != null ? styles["font_name"].Value : RTxtBox.SelectionFont.OriginalFontName;
            float fontSize = styles["font_size"] != null ? Convert.ToSingle(styles["font_size"].Value) : RTxtBox.SelectionFont.Size;
            if (styles["font_style"] == null || !Enum.TryParse(styles["font_style"].Value, out FontStyle fontStyle))
                fontStyle = RTxtBox.SelectionFont.Style;
            RTxtBox.SelectionFont = new Font(fontName, fontSize, fontStyle);

            if(styles["aligment"] != null && Enum.TryParse(styles["aligment"].Value, out HorizontalAlignment fontAlignment))
                RTxtBox.SelectionAlignment = fontAlignment;
        }
    }
}
