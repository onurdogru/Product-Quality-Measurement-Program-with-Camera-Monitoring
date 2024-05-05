using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANDFCT
{
    public partial class Error : Form
    {
        public Main MainFrm;
        public Error()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (this.txtSifre.Text == Ayarlar.Default.adminSifre)
            {
                this.MainFrm.yetki = 3;
                this.MainFrm.yetkidegistir();
                this.txtSifre.Clear();
                this.Close();
            }
            else
            {
                int num = (int)MessageBox.Show("Hatalı Giriş!");
                this.txtSifre.Clear();
            }
        }

        private void txtSifre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Return)
                return;
            this.btnGiris_Click(sender, (EventArgs)e);
        }
    }
}
