using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    internal static class SlowVolFigSVFbdUI
    {
        // CALCULATION HANDLING
        internal static PropertyGrid PropertyGridParams(SlowVolFigSVFbdParam param, int height)
        {
            var pg = new PropertyGrid();
            pg.ToolbarVisible = false;
            pg.PropertySort = PropertySort.NoSort;
            pg.Size = new Size(150, height);
            pg.Location = new Point(20, 12);
            pg.SelectedObject = param;
            pg.PropertyValueChanged += OnParamSettingChange;
            return pg;
        }

        // Check that new param setting remains within allowed bounds
        private static void OnParamSettingChange(object sender, EventArgs e)
        {
            //var pg = (PropertyGrid)sender;
            //var param = ((SlowPriceParam)pg.SelectedObject);
            //var newVal = param.Setting;
            //var lowerLimit = param.From;
            //var upperLimit = param.To;
            //if (newVal < lowerLimit)
            //{
            //    param.Setting = lowerLimit;
            //}
            //else if (newVal > upperLimit)
            //{
            //    param.Setting = upperLimit;
            //}
        }

        internal static Button[] CalcAndSaveBtns(string calculation,
            EventHandler handleCalculationClick, EventHandler handleParameterSaveClick)
        {
            var buttons = new Button[2];

            var btnCalc = new Button();
            btnCalc.Size = new Size(60, 100);
            btnCalc.Location = new Point(170 + 20, 16);
            btnCalc.Text = "Calculate";
            btnCalc.Tag = calculation;
            if (handleCalculationClick != null)
            {
                btnCalc.Click += handleCalculationClick;
            }
            else
            {
                btnCalc.Enabled = false;
                btnCalc.Visible = false;
            }
            buttons[0] = btnCalc;

            var btnSave = new Button();
            btnSave.Size = new Size(60, 50);
            btnSave.Location = new Point(170 + 20, 120);
            if (handleCalculationClick != null)
            {
                btnSave.Text = "Apply and Save";
            }
            else
            {
                btnSave.Text = "Update";
            }
            btnSave.Tag = calculation;
            btnSave.Click += handleParameterSaveClick;
            buttons[1] = btnSave;

            return buttons;
        }

    }

}
