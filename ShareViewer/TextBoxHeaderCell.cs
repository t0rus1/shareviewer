using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    public class TextBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        public readonly TextBox _gotoRowText;

        public TextBoxHeaderCell(EventHandler valueChanged)
        {
            _gotoRowText = new TextBox();

            _gotoRowText.Size = new Size(60, 50);
            _gotoRowText.Font = Control.DefaultFont;
            _gotoRowText.BackColor = Color.LightGray;

            _gotoRowText.MouseClick += new MouseEventHandler(MouseClick);
            _gotoRowText.TextChanged += valueChanged;
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            Helper.Log("Debug",$"x:{e.X}, y:{e.Y}, button:{e.Button}");
            OnClick(new DataGridViewCellEventArgs(ColumnIndex, RowIndex));
        }

        public string RowSearchValue
        {
            get { return _gotoRowText.Text; }
            set { _gotoRowText.Text = value; }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            _gotoRowText.Location = new Point(cellBounds.Left+ 36, cellBounds.Top + 5);
        }

    }

    public class ColumnWithNumericHeader : DataGridViewTextBoxColumn
    {
        TextBoxHeaderCell headerCell;

        public ColumnWithNumericHeader(EventHandler valueChanged)
        {
            //this.CellTemplate = new DataGridViewTextBoxCell();
            headerCell = new TextBoxHeaderCell(valueChanged);
            this.HeaderCell = headerCell;
            //this.Width = 150;
        }

        protected override void OnDataGridViewChanged()
        {
            if (DataGridView != null)
            {
                this.DataGridView.Controls.Add(headerCell._gotoRowText);
            }
        }


    }

}
