using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDemo
{
    public partial class Form1 : Form
    {
        delegate IEnumerable<int> CalcPrimesCaller(int from, int to);
        PrimesCalculator _asdmc;

        public Form1()
        {
            InitializeComponent();
            _asdmc = new PrimesCalculator();
        }

        private async void  btnCalcPrimes_Click(object sender, EventArgs e)
        {
            lstResult.Items.Clear();
            try
            {
                var from = int.Parse(txtFrom.Text);
                var to = int.Parse(txtTo.Text);
                var calcPrimes = new CalcPrimesCaller(_asdmc.CalcPrimes);
                await Task.Run(()=>calcPrimes.BeginInvoke(from, to, new AsyncCallback(setResult), null));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void setResult(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            CalcPrimesCaller calcPrimes = (CalcPrimesCaller)result.AsyncDelegate;
            var returnValue = calcPrimes.EndInvoke(ar);
           returnValue.ToList().ForEach((x) => lstResult.BeginInvoke((Action)(() => lstResult.Items.Add(x))));
        }
    }
}
