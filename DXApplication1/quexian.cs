using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace DXApplication1
{
    public partial class quexian : DevExpress.XtraEditors.XtraForm
    {
        public quexian()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData() {
            Application.DoEvents();
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中国....");
            splashScreenManager1.SetWaitFormDescription("正在初始化");

            string uri = "http://localhost:19893/testdemo.svc?wsdl";
            object o = WcfChannelFactory.ExecuteMethod<Itestdemo>(uri, "GetTestString", Guid.NewGuid());
            string reciveStr=(string)o;
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = "明细";
            btn1.Text = "明细";
            btn1.Name = "detail";
            btn1.Width = 60;
            btn1.UseColumnTextForButtonValue = true;
            this.QxxxData.Columns.Add(btn1); 
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(reciveStr);
            QxxxData.DataSource = dt;
           
            splashScreenManager1.CloseWaitForm();
        }
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void QxxxData_Click(object sender, EventArgs e)
        {

        }

        private void QxxxData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (QxxxData.Columns[e.ColumnIndex].Name == "detail")
            {
                //可以在此打开新窗口，把参数传递过去 
                MessageBox.Show(this.QxxxData.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
        }
    }
}