using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex
{
    public partial class RowOne : BaseForm
    {
        public string text;
        public bool b = false;
        public List<TextBox> variables = new List<TextBox>(6);

        public RowOne()
        {
            InitializeComponent();
        }

        private void RowOne_Load(object sender, EventArgs e)
        {
            variables.Add(res);
            variables.Add(x1);
            variables.Add(x2);
            variables.Add(x3);
            variables.Add(x4);
            variables.Add(x5);
            foreach(var vairy in variables)
            {
                vairy.Text = "0";
            }
            label1.Text = text;
            if (b)
            {
                res.ReadOnly = true;
            }
            for(int i = 0; i < SimplexInputCreator.variablesСount+1; i++)
            {
                variables[i].Visible = true;
                string s = 'l' + (i + 1).ToString();
               Label l = this.Controls.Find(s,true)[0] as Label;
                l.Visible = true;
                if (i == SimplexInputCreator.variablesСount) l.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] dmass = new double[SimplexInputCreator.variablesСount+1];
            for(int i = 0; i < dmass.Length; i++)
            {
                dmass[i] = double.Parse(variables[i].Text);
                if (b)
                {
                    dmass[i] = -dmass[i];
                }
            }
            SimplexInputCreator.rows.Add(new MyRow(SimplexInputCreator.variablesСount,dmass,comboBox1.Text));
            if (SimplexInputCreator.formsNeeded--==0)
            {
                FinalForm ff = new FinalForm();
                ShowNextForm(ff);
            }
            RowOne ro = new RowOne();
            ro.text = "Ограничение:";
            ShowNextForm(ro);
        }
    }
}
