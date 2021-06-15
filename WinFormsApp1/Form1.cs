using Newtonsoft.Json;
using SalesTaxes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        static string Json = File.ReadAllText(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\")) + @"Resources\Products.json");
        static List<Products> products = JsonConvert.DeserializeObject<List<Products>>(Json);
        static List<Products> Input = new List<Products>();
        static int BaseTax = 10;
        static int ImportedTax = 5;

        public Form1()
        {
            InitializeComponent();
            foreach (var elm in products)
                this.dataGridView1.Rows.Add(elm.sku, elm.Name, elm.Amount, elm.Imported, elm.NoBaseTax);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Elements SKU
                var Sku = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value;

                var element = products.Find(x => x.sku == (string)Sku);

                if (element != null)
                    Input.Add(element);

                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();

                foreach (var elm in Input)
                    this.dataGridView2.Rows.Add(elm.sku, elm.Name);
            }
            catch (Exception ex) { }

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            try
            {
                Input.RemoveAt(dataGridView2.CurrentCell.RowIndex);

                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();

                foreach (var elm in Input)
                    this.dataGridView2.Rows.Add(elm.sku, elm.Name);
            }
            catch (Exception ex) { }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            MessageBox.Show(ProcessData.ConcatData(Input, BaseTax, ImportedTax));

            Input = new List<Products>();

            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

        }
    }

        
}
