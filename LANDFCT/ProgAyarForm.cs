// Decompiled with JetBrains decompiler
// Type: EsdTurnikesi.AyarForm
// Assembly: EsdTurnikesi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8099926-BBEB-495E-ADF6-36B4F5F75BE8
// Assembly location: C:\Users\serkan.baki\Desktop\esd-rar\ESD\Release\EsdTurnikesi.exe

using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Text;

namespace LANDFCT
{
    public class ProgAyarForm : Form
    {
        public Form MainFrm;
        private IContainer components;

        private Button btnKaydet;
        private TextBox companyNo;
        private Label label23;
        private TextBox SAPNo;
        private Label label28;
        private Button btnINIsec;
        private TextBox txtINIdosya;
        private Label label220;
        private Button btnOkuIni;
        private Button btnKaydetIni;
        public GroupBox iniSettings;
        public GroupBox barkodSettings;
        private TextBox softwareRev;
        private Label label31;
        private Label label29;
        private TextBox FCTRev;
        private TextBox softwareVer;
        private Label label30;
        private Label label26;
        private TextBox BOMVer;
        private TextBox ICTRev;
        private Label label27;
        private Label label24;
        private TextBox cardNo;
        private TextBox gerberVer;
        private Label label1;
        private TextBox indexNo;
        private TextBox productCode;
        private Label label2;
        private TextBox secondRubberMin;
        private Label label15;
        private TextBox firstRubberMin;
        private TextBox secondRubberMax;
        private Label label13;
        private Label label16;
        private Label label14;
        private TextBox firstRubberMax;
        private TextBox fourthRubberMin;
        private Label label21;
        private TextBox thirdRubberMin;
        private TextBox fourthRubberMax;
        private Label label22;
        private Label label33;
        private Label label34;
        private TextBox thirdRubberMax;
        public GroupBox rubberSettings;
        public GroupBox operatorSettings;
        public GroupBox cardSettings;
        private CheckBox card4;
        private CheckBox card3;
        private CheckBox card2;
        private CheckBox card1;
        private PictureBox infoPicture1;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private ToolTip toolTip3;
        private ToolTip toolTip4;
        private PictureBox infoPicture9;
        private PictureBox infoPicture8;
        private PictureBox infoPicture7;
        private PictureBox infoPicture6;
        private PictureBox infoPicture5;
        private PictureBox infoPicture4;
        private PictureBox infoPicture3;
        private PictureBox infoPicture2;
        private PictureBox infoPicture11;
        private PictureBox infoPicture10;
        private ToolTip toolTip5;
        private ToolTip toolTip6;
        private ToolTip toolTip7;
        private ToolTip toolTip8;
        private ToolTip toolTip9;
        private ToolTip toolTip10;
        private ToolTip toolTip11;
        private Label label25;

        public ProgAyarForm()
        {
            this.InitializeComponent();
        }

        public class INIKaydet
        {
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            public INIKaydet(string dosyaYolu)
            {
                DOSYAYOLU = dosyaYolu;
            }
            private string DOSYAYOLU = String.Empty;
            public string Varsayilan { get; set; }
            public string Oku(string bolum, string ayaradi)
            {
                Varsayilan = Varsayilan ?? string.Empty;
                StringBuilder StrBuild = new StringBuilder(256);
                GetPrivateProfileString(bolum, ayaradi, Varsayilan, StrBuild, 255, DOSYAYOLU);
                return StrBuild.ToString();
            }
            public long Yaz(string bolum, string ayaradi, string deger)
            {
                return WritePrivateProfileString(bolum, ayaradi, deger, DOSYAYOLU);
            }
        }

        private void AyarForm_Load(object sender, EventArgs e)
        {
            toolTip_Load();

            this.companyNo.Text = Prog_Ayarlar.Default.companyNo;
            this.SAPNo.Text = Prog_Ayarlar.Default.SAPNo;
            this.indexNo.Text = Prog_Ayarlar.Default.indexNo;
            this.productCode.Text = Prog_Ayarlar.Default.productCode;
            this.cardNo.Text = Prog_Ayarlar.Default.cardNo;
            this.gerberVer.Text = Prog_Ayarlar.Default.gerberVer;
            this.BOMVer.Text = Prog_Ayarlar.Default.BOMVer;
            this.ICTRev.Text = Prog_Ayarlar.Default.ICTRev;
            this.FCTRev.Text = Prog_Ayarlar.Default.FCTRev;
            this.softwareVer.Text = Prog_Ayarlar.Default.softwareVer;
            this.softwareRev.Text = Prog_Ayarlar.Default.softwareRev;

            this.txtINIdosya.Text = Prog_Ayarlar.Default.iniDosyaYolu;
        
            this.firstRubberMin.Text = Prog_Ayarlar.Default.firstRubberMin;
            this.firstRubberMax.Text = Prog_Ayarlar.Default.firstRubberMax;

            this.secondRubberMin.Text = Prog_Ayarlar.Default.secondRubberMin;
            this.secondRubberMax.Text = Prog_Ayarlar.Default.secondRubberMax;

            this.thirdRubberMin.Text = Prog_Ayarlar.Default.thirdRubberMin;
            this.thirdRubberMax.Text = Prog_Ayarlar.Default.thirdRubberMax;

            this.fourthRubberMin.Text = Prog_Ayarlar.Default.fourthRubberMin;
            this.fourthRubberMax.Text = Prog_Ayarlar.Default.fourthRubberMax;

            this.card1.Checked = Prog_Ayarlar.Default.card1;
            this.card2.Checked = Prog_Ayarlar.Default.card2;
            this.card3.Checked = Prog_Ayarlar.Default.card3;
            this.card4.Checked = Prog_Ayarlar.Default.card4;
        }

        private void toolTip_Load()
        {
            string[] toolTipTitle = new string[12];
            string[] toolTipTool = new string[12];
            toolTipTitle[1] = "Lütfen 2 Haneli Company No Giriniz";
            toolTipTitle[2] = "Lütfen 10 Haneli SAP No Giriniz";
            toolTipTitle[3] = "Lütfen 2 Haneli Card No Giriniz";
            toolTipTitle[4] = "Lütfen 2 Haneli Gerber Ver Giriniz";
            toolTipTitle[5] = "Lütfen 2 Haneli BOM Ver Giriniz";
            toolTipTitle[6] = "Lütfen 2 Haneli ICT Rev Giriniz";
            toolTipTitle[7] = "Lütfen 2 Haneli FCT Rev Giriniz";
            toolTipTitle[8] = "Lütfen 2 Haneli Software Rev Giriniz";
            toolTipTitle[9] = "Lütfen 2 Haneli Software Ver Giriniz";
            toolTipTitle[10] = "Lütfen 6 Haneli Index No Giriniz";
            toolTipTitle[11] = "Lütfen 14 Haneli Product Code Giriniz";

            toolTipTool[1] = "Örnek : 91";
            toolTipTool[2] = "Örnek : 3585310100";
            toolTipTool[3] = "Örnek : 05";
            toolTipTool[4] = "Örnek : 06";
            toolTipTool[5] = "Örnek : 03";
            toolTipTool[6] = "Örnek : 01";
            toolTipTool[7] = "Örnek : 01";
            toolTipTool[8] = "Örnek : 01";
            toolTipTool[9] = "Örnek : 04";
            toolTipTool[10] = "Örnek : 000156";
            toolTipTool[11] = "Örnek : 00000000123456";

            toolTip1.Active = true;
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 5000;
            toolTip1.IsBalloon = true;
            toolTip1.UseAnimation = true;
            toolTip1.UseFading = true;
            toolTip1.ShowAlways = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.ToolTipTitle = toolTipTitle[1];
            toolTip1.SetToolTip(infoPicture1, toolTipTool[1]);

            toolTip2.Active = true;
            toolTip2.AutoPopDelay = 5000;
            toolTip2.InitialDelay = 1000;
            toolTip2.ReshowDelay = 5000;
            toolTip2.IsBalloon = true;
            toolTip2.UseAnimation = true;
            toolTip2.UseFading = true;
            toolTip2.ShowAlways = true;
            toolTip2.ToolTipIcon = ToolTipIcon.Info;
            toolTip2.ToolTipTitle = toolTipTitle[2];
            toolTip2.SetToolTip(infoPicture2, toolTipTool[2]);

            toolTip3.Active = true;
            toolTip3.AutoPopDelay = 5000;
            toolTip3.InitialDelay = 1000;
            toolTip3.ReshowDelay = 5000;
            toolTip3.IsBalloon = true;
            toolTip3.UseAnimation = true;
            toolTip3.UseFading = true;
            toolTip3.ShowAlways = true;
            toolTip3.ToolTipIcon = ToolTipIcon.Info;
            toolTip3.ToolTipTitle = toolTipTitle[3];
            toolTip3.SetToolTip(infoPicture3, toolTipTool[3]);

            toolTip4.Active = true;
            toolTip4.AutoPopDelay = 5000;
            toolTip4.InitialDelay = 1000;
            toolTip4.ReshowDelay = 5000;
            toolTip4.IsBalloon = true;
            toolTip4.UseAnimation = true;
            toolTip4.UseFading = true;
            toolTip4.ShowAlways = true;
            toolTip4.ToolTipIcon = ToolTipIcon.Info;
            toolTip4.ToolTipTitle = toolTipTitle[4];
            toolTip4.SetToolTip(infoPicture4, toolTipTool[4]);

            toolTip5.Active = true;
            toolTip5.AutoPopDelay = 5000;
            toolTip5.InitialDelay = 1000;
            toolTip5.ReshowDelay = 5000;
            toolTip5.IsBalloon = true;
            toolTip5.UseAnimation = true;
            toolTip5.UseFading = true;
            toolTip5.ShowAlways = true;
            toolTip5.ToolTipIcon = ToolTipIcon.Info;
            toolTip5.ToolTipTitle = toolTipTitle[5];
            toolTip5.SetToolTip(infoPicture5, toolTipTool[5]);

            toolTip6.Active = true;
            toolTip6.AutoPopDelay = 5000;
            toolTip6.InitialDelay = 1000;
            toolTip6.ReshowDelay = 5000;
            toolTip6.IsBalloon = true;
            toolTip6.UseAnimation = true;
            toolTip6.UseFading = true;
            toolTip6.ShowAlways = true;
            toolTip6.ToolTipIcon = ToolTipIcon.Info;
            toolTip6.ToolTipTitle = toolTipTitle[6];
            toolTip6.SetToolTip(infoPicture6, toolTipTool[6]);

            toolTip7.Active = true;
            toolTip7.AutoPopDelay = 5000;
            toolTip7.InitialDelay = 1000;
            toolTip7.ReshowDelay = 5000;
            toolTip7.IsBalloon = true;
            toolTip7.UseAnimation = true;
            toolTip7.UseFading = true;
            toolTip7.ShowAlways = true;
            toolTip7.ToolTipIcon = ToolTipIcon.Info;
            toolTip7.ToolTipTitle = toolTipTitle[7];
            toolTip7.SetToolTip(infoPicture7, toolTipTool[7]);

            toolTip8.Active = true;
            toolTip8.AutoPopDelay = 5000;
            toolTip8.InitialDelay = 1000;
            toolTip8.ReshowDelay = 5000;
            toolTip8.IsBalloon = true;
            toolTip8.UseAnimation = true;
            toolTip8.UseFading = true;
            toolTip8.ShowAlways = true;
            toolTip8.ToolTipIcon = ToolTipIcon.Info;
            toolTip8.ToolTipTitle = toolTipTitle[8];
            toolTip8.SetToolTip(infoPicture8, toolTipTool[8]);

            toolTip9.Active = true;
            toolTip9.AutoPopDelay = 5000;
            toolTip9.InitialDelay = 1000;
            toolTip9.ReshowDelay = 5000;
            toolTip9.IsBalloon = true;
            toolTip9.UseAnimation = true;
            toolTip9.UseFading = true;
            toolTip9.ShowAlways = true;
            toolTip9.ToolTipIcon = ToolTipIcon.Info;
            toolTip9.ToolTipTitle = toolTipTitle[9];
            toolTip9.SetToolTip(infoPicture9, toolTipTool[9]);

            toolTip10.Active = true;
            toolTip10.AutoPopDelay = 5000;
            toolTip10.InitialDelay = 1000;
            toolTip10.ReshowDelay = 5000;
            toolTip10.IsBalloon = true;
            toolTip10.UseAnimation = true;
            toolTip10.UseFading = true;
            toolTip10.ShowAlways = true;
            toolTip10.ToolTipIcon = ToolTipIcon.Info;
            toolTip10.ToolTipTitle = toolTipTitle[10];
            toolTip10.SetToolTip(infoPicture10, toolTipTool[10]);

            toolTip11.Active = true;
            toolTip11.AutoPopDelay = 5000;
            toolTip11.InitialDelay = 1000;
            toolTip11.ReshowDelay = 5000;
            toolTip11.IsBalloon = true;
            toolTip11.UseAnimation = true;
            toolTip11.UseFading = true;
            toolTip11.ShowAlways = true;
            toolTip11.ToolTipIcon = ToolTipIcon.Info;
            toolTip11.ToolTipTitle = toolTipTitle[11];
            toolTip11.SetToolTip(infoPicture11, toolTipTool[11]);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedBarkod())
                {
                    Prog_Ayarlar.Default.companyNo = this.companyNo.Text;
                    Prog_Ayarlar.Default.SAPNo = this.SAPNo.Text;
                    Prog_Ayarlar.Default.indexNo = this.indexNo.Text;
                    Prog_Ayarlar.Default.productCode = this.productCode.Text;
                    Prog_Ayarlar.Default.cardNo = this.cardNo.Text;
                    Prog_Ayarlar.Default.gerberVer = this.gerberVer.Text;
                    Prog_Ayarlar.Default.BOMVer = this.BOMVer.Text;
                    Prog_Ayarlar.Default.ICTRev = this.ICTRev.Text;
                    Prog_Ayarlar.Default.FCTRev = this.FCTRev.Text;
                    Prog_Ayarlar.Default.softwareVer = this.softwareVer.Text;
                    Prog_Ayarlar.Default.softwareRev = this.softwareRev.Text;

                    Prog_Ayarlar.Default.iniDosyaYolu = this.txtINIdosya.Text;

                    Prog_Ayarlar.Default.firstRubberMin = this.firstRubberMin.Text;
                    Prog_Ayarlar.Default.firstRubberMax = this.firstRubberMax.Text;

                    Prog_Ayarlar.Default.secondRubberMin = this.secondRubberMin.Text;
                    Prog_Ayarlar.Default.secondRubberMax = this.secondRubberMax.Text;

                    Prog_Ayarlar.Default.thirdRubberMin = this.thirdRubberMin.Text;
                    Prog_Ayarlar.Default.thirdRubberMax = this.thirdRubberMax.Text;

                    Prog_Ayarlar.Default.fourthRubberMin = this.fourthRubberMin.Text;
                    Prog_Ayarlar.Default.fourthRubberMax = this.fourthRubberMax.Text;

                    Prog_Ayarlar.Default.card1 = this.card1.Checked;
                    Prog_Ayarlar.Default.card2 = this.card2.Checked;
                    Prog_Ayarlar.Default.card3 = this.card3.Checked;
                    Prog_Ayarlar.Default.card4 = this.card4.Checked;

                    Prog_Ayarlar.Default.Save();

                    CustomMessageBox.ShowMessage("Bütün Ayarlar Başarıyla Kaydedildi.", Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Information, Color.Yellow);
                    this.Close();

                    Application.Restart();
                }
                else
                {
                    CustomMessageBox.ShowMessage("Barkod Ayarları Hatalı ", Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Error, Color.Red);
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("Ayarlar Kayıt Hatası: " + ex.ToString(), Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Error, Color.Red);
            }
        }   

        private void btnKaydetIni_Click(object sender, EventArgs e)
        {
            if (txtINIdosya.Text != "")
            {
                INIKaydet ini = new INIKaydet(txtINIdosya.Text);  // @"\Programlama.ini"
               
                ini.Yaz("companyNo", "Metin Kutusu", Convert.ToString(companyNo.Text));
                ini.Yaz("SAPNo", "Metin Kutusu", Convert.ToString(SAPNo.Text));
                ini.Yaz("indexNo", "Metin Kutusu", Convert.ToString(indexNo.Text));
                ini.Yaz("productCode", "Metin Kutusu", Convert.ToString(productCode.Text));
                ini.Yaz("gerberVer", "Metin Kutusu", Convert.ToString(gerberVer.Text));
                ini.Yaz("BOMVer", "Metin Kutusu", Convert.ToString(BOMVer.Text));
                ini.Yaz("ICTRev", "Metin Kutusu", Convert.ToString(ICTRev.Text));
                ini.Yaz("FCTRev", "Metin Kutusu", Convert.ToString(FCTRev.Text));
                ini.Yaz("softwareVer", "Metin Kutusu", Convert.ToString(softwareVer.Text));
                ini.Yaz("softwareRev", "Metin Kutusu", Convert.ToString(softwareRev.Text));

                ini.Yaz("firstRubberMin", "Metin Kutusu", Convert.ToString(firstRubberMin.Text));
                ini.Yaz("firstRubberMax", "Metin Kutusu", Convert.ToString(firstRubberMax.Text));
                ini.Yaz("secondRubberMin", "Metin Kutusu", Convert.ToString(secondRubberMin.Text));
                ini.Yaz("secondRubberMax", "Metin Kutusu", Convert.ToString(secondRubberMax.Text));
                ini.Yaz("thirdRubberMin", "Metin Kutusu", Convert.ToString(thirdRubberMin.Text));
                ini.Yaz("thirdRubberMax", "Metin Kutusu", Convert.ToString(thirdRubberMax.Text));
                ini.Yaz("fourthRubberMin", "Metin Kutusu", Convert.ToString(fourthRubberMin.Text));
                ini.Yaz("fourthRubberMax", "Metin Kutusu", Convert.ToString(fourthRubberMax.Text));

                ini.Yaz("card1", "Metin Kutusu", Convert.ToString(card1.Checked));
                ini.Yaz("card2", "Metin Kutusu", Convert.ToString(card2.Checked));
                ini.Yaz("card3", "Metin Kutusu", Convert.ToString(card3.Checked));
                ini.Yaz("card4", "Metin Kutusu", Convert.ToString(card4.Checked));

                CustomMessageBox.ShowMessage("Bütün Ayarlar Dosyaya Başarıyla Kaydedildi.", Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Information, Color.Yellow);
            }
            else
            {
                CustomMessageBox.ShowMessage("Dosya Yolu Boş Kalamaz", Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Error, Color.Red);
            }
        }

        private void btnOkuIni_Click(object sender, EventArgs e)
        {
            if (txtINIdosya.Text != "")
            {
                try
                {
                    if (File.Exists(txtINIdosya.Text))
                    {
                        INIKaydet ini = new INIKaydet(txtINIdosya.Text);

                        companyNo.Text = ini.Oku("companyNo", "Metin Kutusu");
                        SAPNo.Text = ini.Oku("SAPNo", "Metin Kutusu");
                        indexNo.Text = ini.Oku("indexNo", "Metin Kutusu");
                        productCode.Text = ini.Oku("productCode", "Metin Kutusu");
                        cardNo.Text = ini.Oku("cardNo", "Metin Kutusu");
                        gerberVer.Text = ini.Oku("gerberVer", "Metin Kutusu");
                        BOMVer.Text = ini.Oku("BOMVer", "Metin Kutusu");
                        ICTRev.Text = ini.Oku("ICTRev", "Metin Kutusu");
                        FCTRev.Text = ini.Oku("FCTRev", "Metin Kutusu");
                        softwareVer.Text = ini.Oku("softwareVer", "Metin Kutusu");
                        softwareRev.Text = ini.Oku("softwareRev", "Metin Kutusu");

                        firstRubberMin.Text = ini.Oku("firstRubberMin", "Metin Kutusu");
                        firstRubberMax.Text = ini.Oku("firstRubberMax", "Metin Kutusu");
                        secondRubberMin.Text = ini.Oku("secondRubberMin", "Metin Kutusu");
                        secondRubberMax.Text = ini.Oku("secondRubberMax", "Metin Kutusu");
                        thirdRubberMin.Text = ini.Oku("thirdRubberMin", "Metin Kutusu");
                        thirdRubberMax.Text = ini.Oku("thirdRubberMax", "Metin Kutusu");
                        fourthRubberMin.Text = ini.Oku("fourthRubberMin", "Metin Kutusu");
                        fourthRubberMax.Text = ini.Oku("fourthRubberMax", "Metin Kutusu");

                        if (ini.Oku("card1", "Metin Kutusu") == "True")
                            card1.Checked = true;
                        else if (ini.Oku("card1", "Metin Kutusu") == "False")
                            card1.Checked = false;

                        if (ini.Oku("card2", "Metin Kutusu") == "True")
                            card2.Checked = true;
                        else if (ini.Oku("card2", "Metin Kutusu") == "False")
                            card2.Checked = false;

                        if (ini.Oku("card3", "Metin Kutusu") == "True")
                            card3.Checked = true;
                        else if (ini.Oku("card3", "Metin Kutusu") == "False")
                            card3.Checked = false;

                        if (ini.Oku("card4", "Metin Kutusu") == "True")
                            card4.Checked = true;
                        else if (ini.Oku("card4", "Metin Kutusu") == "False")
                            card4.Checked = false;

                        CustomMessageBox.ShowMessage("Bütün Ayarlar Dosyadan Başarıyla Okundu.", Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Information, Color.Yellow);
                    }
                }
                catch (Exception hata)
                {
                    CustomMessageBox.ShowMessage("ini Dosyası Hasarlı" + hata.Message, Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Error, Color.Red);
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("Dosya Yolu Boş Kalamaz", Ayarlar.Default.projectName, MessageBoxButtons.OK, CustomMessageBoxIcon.Error, Color.Red);
            }
        }

        private void btnIDsec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.ini";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.txtINIdosya.Text = openFileDialog.FileName;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgAyarForm));
            this.btnKaydet = new System.Windows.Forms.Button();
            this.companyNo = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.SAPNo = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnINIsec = new System.Windows.Forms.Button();
            this.txtINIdosya = new System.Windows.Forms.TextBox();
            this.label220 = new System.Windows.Forms.Label();
            this.btnOkuIni = new System.Windows.Forms.Button();
            this.btnKaydetIni = new System.Windows.Forms.Button();
            this.iniSettings = new System.Windows.Forms.GroupBox();
            this.barkodSettings = new System.Windows.Forms.GroupBox();
            this.softwareRev = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.FCTRev = new System.Windows.Forms.TextBox();
            this.softwareVer = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.BOMVer = new System.Windows.Forms.TextBox();
            this.ICTRev = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cardNo = new System.Windows.Forms.TextBox();
            this.gerberVer = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.indexNo = new System.Windows.Forms.TextBox();
            this.productCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secondRubberMin = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.firstRubberMin = new System.Windows.Forms.TextBox();
            this.secondRubberMax = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.firstRubberMax = new System.Windows.Forms.TextBox();
            this.fourthRubberMin = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.thirdRubberMin = new System.Windows.Forms.TextBox();
            this.fourthRubberMax = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.thirdRubberMax = new System.Windows.Forms.TextBox();
            this.rubberSettings = new System.Windows.Forms.GroupBox();
            this.operatorSettings = new System.Windows.Forms.GroupBox();
            this.cardSettings = new System.Windows.Forms.GroupBox();
            this.card4 = new System.Windows.Forms.CheckBox();
            this.card3 = new System.Windows.Forms.CheckBox();
            this.card2 = new System.Windows.Forms.CheckBox();
            this.card1 = new System.Windows.Forms.CheckBox();
            this.infoPicture1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.infoPicture2 = new System.Windows.Forms.PictureBox();
            this.infoPicture4 = new System.Windows.Forms.PictureBox();
            this.infoPicture3 = new System.Windows.Forms.PictureBox();
            this.infoPicture6 = new System.Windows.Forms.PictureBox();
            this.infoPicture5 = new System.Windows.Forms.PictureBox();
            this.infoPicture8 = new System.Windows.Forms.PictureBox();
            this.infoPicture7 = new System.Windows.Forms.PictureBox();
            this.infoPicture9 = new System.Windows.Forms.PictureBox();
            this.infoPicture11 = new System.Windows.Forms.PictureBox();
            this.infoPicture10 = new System.Windows.Forms.PictureBox();
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip7 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip8 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip9 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip10 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip11 = new System.Windows.Forms.ToolTip(this.components);
            this.iniSettings.SuspendLayout();
            this.barkodSettings.SuspendLayout();
            this.rubberSettings.SuspendLayout();
            this.operatorSettings.SuspendLayout();
            this.cardSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture10)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.Aqua;
            this.btnKaydet.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.Black;
            this.btnKaydet.Location = new System.Drawing.Point(12, 379);
            this.btnKaydet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(412, 50);
            this.btnKaydet.TabIndex = 6;
            this.btnKaydet.Text = "Ayarları Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // companyNo
            // 
            this.companyNo.Location = new System.Drawing.Point(130, 22);
            this.companyNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.companyNo.Name = "companyNo";
            this.companyNo.Size = new System.Drawing.Size(172, 24);
            this.companyNo.TabIndex = 58;
            this.companyNo.TextChanged += new System.EventHandler(this.companyNo_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(35, 25);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(83, 17);
            this.label23.TabIndex = 57;
            this.label23.Text = "Company No:";
            // 
            // SAPNo
            // 
            this.SAPNo.Location = new System.Drawing.Point(130, 60);
            this.SAPNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SAPNo.Name = "SAPNo";
            this.SAPNo.Size = new System.Drawing.Size(172, 24);
            this.SAPNo.TabIndex = 60;
            this.SAPNo.TextChanged += new System.EventHandler(this.SAPNo_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(63, 63);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(52, 17);
            this.label28.TabIndex = 59;
            this.label28.Text = "SAP No:";
            // 
            // btnINIsec
            // 
            this.btnINIsec.BackColor = System.Drawing.Color.Aqua;
            this.btnINIsec.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnINIsec.Location = new System.Drawing.Point(306, 23);
            this.btnINIsec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnINIsec.Name = "btnINIsec";
            this.btnINIsec.Size = new System.Drawing.Size(65, 24);
            this.btnINIsec.TabIndex = 587;
            this.btnINIsec.Text = "Seç";
            this.btnINIsec.UseVisualStyleBackColor = false;
            this.btnINIsec.Click += new System.EventHandler(this.btnIDsec_Click);
            // 
            // txtINIdosya
            // 
            this.txtINIdosya.Location = new System.Drawing.Point(131, 22);
            this.txtINIdosya.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtINIdosya.Name = "txtINIdosya";
            this.txtINIdosya.Size = new System.Drawing.Size(167, 24);
            this.txtINIdosya.TabIndex = 586;
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Location = new System.Drawing.Point(9, 22);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(116, 17);
            this.label220.TabIndex = 585;
            this.label220.Text = "Ayarlar Dosya Yolu:";
            // 
            // btnOkuIni
            // 
            this.btnOkuIni.BackColor = System.Drawing.Color.Aqua;
            this.btnOkuIni.Location = new System.Drawing.Point(217, 55);
            this.btnOkuIni.Name = "btnOkuIni";
            this.btnOkuIni.Size = new System.Drawing.Size(80, 30);
            this.btnOkuIni.TabIndex = 584;
            this.btnOkuIni.Text = "Oku";
            this.btnOkuIni.UseVisualStyleBackColor = false;
            this.btnOkuIni.Click += new System.EventHandler(this.btnOkuIni_Click);
            // 
            // btnKaydetIni
            // 
            this.btnKaydetIni.BackColor = System.Drawing.Color.Aqua;
            this.btnKaydetIni.Location = new System.Drawing.Point(131, 55);
            this.btnKaydetIni.Name = "btnKaydetIni";
            this.btnKaydetIni.Size = new System.Drawing.Size(80, 30);
            this.btnKaydetIni.TabIndex = 583;
            this.btnKaydetIni.Text = "Kaydet";
            this.btnKaydetIni.UseVisualStyleBackColor = false;
            this.btnKaydetIni.Click += new System.EventHandler(this.btnKaydetIni_Click);
            // 
            // iniSettings
            // 
            this.iniSettings.Controls.Add(this.label220);
            this.iniSettings.Controls.Add(this.btnOkuIni);
            this.iniSettings.Controls.Add(this.btnINIsec);
            this.iniSettings.Controls.Add(this.btnKaydetIni);
            this.iniSettings.Controls.Add(this.txtINIdosya);
            this.iniSettings.Location = new System.Drawing.Point(12, 110);
            this.iniSettings.Name = "iniSettings";
            this.iniSettings.Size = new System.Drawing.Size(412, 92);
            this.iniSettings.TabIndex = 588;
            this.iniSettings.TabStop = false;
            this.iniSettings.Text = "Ini Dosyası Ayarları:";
            // 
            // barkodSettings
            // 
            this.barkodSettings.Controls.Add(this.infoPicture9);
            this.barkodSettings.Controls.Add(this.infoPicture8);
            this.barkodSettings.Controls.Add(this.infoPicture7);
            this.barkodSettings.Controls.Add(this.infoPicture6);
            this.barkodSettings.Controls.Add(this.infoPicture5);
            this.barkodSettings.Controls.Add(this.infoPicture4);
            this.barkodSettings.Controls.Add(this.infoPicture3);
            this.barkodSettings.Controls.Add(this.infoPicture2);
            this.barkodSettings.Controls.Add(this.infoPicture1);
            this.barkodSettings.Controls.Add(this.softwareRev);
            this.barkodSettings.Controls.Add(this.label31);
            this.barkodSettings.Controls.Add(this.label29);
            this.barkodSettings.Controls.Add(this.FCTRev);
            this.barkodSettings.Controls.Add(this.softwareVer);
            this.barkodSettings.Controls.Add(this.label30);
            this.barkodSettings.Controls.Add(this.label26);
            this.barkodSettings.Controls.Add(this.BOMVer);
            this.barkodSettings.Controls.Add(this.ICTRev);
            this.barkodSettings.Controls.Add(this.label27);
            this.barkodSettings.Controls.Add(this.label24);
            this.barkodSettings.Controls.Add(this.cardNo);
            this.barkodSettings.Controls.Add(this.gerberVer);
            this.barkodSettings.Controls.Add(this.label25);
            this.barkodSettings.Controls.Add(this.label23);
            this.barkodSettings.Controls.Add(this.companyNo);
            this.barkodSettings.Controls.Add(this.SAPNo);
            this.barkodSettings.Controls.Add(this.label28);
            this.barkodSettings.Location = new System.Drawing.Point(449, 12);
            this.barkodSettings.Name = "barkodSettings";
            this.barkodSettings.Size = new System.Drawing.Size(379, 351);
            this.barkodSettings.TabIndex = 589;
            this.barkodSettings.TabStop = false;
            this.barkodSettings.Text = "Barkod Ayarları:";
            // 
            // softwareRev
            // 
            this.softwareRev.Location = new System.Drawing.Point(131, 314);
            this.softwareRev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.softwareRev.Name = "softwareRev";
            this.softwareRev.Size = new System.Drawing.Size(172, 24);
            this.softwareRev.TabIndex = 74;
            this.softwareRev.TextChanged += new System.EventHandler(this.softwareRev_TextChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(20, 317);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(87, 17);
            this.label31.TabIndex = 73;
            this.label31.Text = "Software Rev:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(52, 245);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(56, 17);
            this.label29.TabIndex = 69;
            this.label29.Text = "FCT Rev:";
            // 
            // FCTRev
            // 
            this.FCTRev.Location = new System.Drawing.Point(131, 242);
            this.FCTRev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FCTRev.Name = "FCTRev";
            this.FCTRev.Size = new System.Drawing.Size(172, 24);
            this.FCTRev.TabIndex = 70;
            this.FCTRev.TextChanged += new System.EventHandler(this.FCTRev_TextChanged);
            // 
            // softwareVer
            // 
            this.softwareVer.Location = new System.Drawing.Point(131, 278);
            this.softwareVer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.softwareVer.Name = "softwareVer";
            this.softwareVer.Size = new System.Drawing.Size(172, 24);
            this.softwareVer.TabIndex = 72;
            this.softwareVer.TextChanged += new System.EventHandler(this.softwareVer_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(23, 281);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(85, 17);
            this.label30.TabIndex = 71;
            this.label30.Text = "Software Ver:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(49, 172);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(63, 17);
            this.label26.TabIndex = 65;
            this.label26.Text = "BOM Ver:";
            // 
            // BOMVer
            // 
            this.BOMVer.Location = new System.Drawing.Point(131, 169);
            this.BOMVer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BOMVer.Name = "BOMVer";
            this.BOMVer.Size = new System.Drawing.Size(172, 24);
            this.BOMVer.TabIndex = 66;
            this.BOMVer.TextChanged += new System.EventHandler(this.BOMVer_TextChanged);
            // 
            // ICTRev
            // 
            this.ICTRev.Location = new System.Drawing.Point(131, 205);
            this.ICTRev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ICTRev.Name = "ICTRev";
            this.ICTRev.Size = new System.Drawing.Size(172, 24);
            this.ICTRev.TabIndex = 68;
            this.ICTRev.TextChanged += new System.EventHandler(this.ICTRev_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(57, 208);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(54, 17);
            this.label27.TabIndex = 67;
            this.label27.Text = "ICT Rev:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(57, 100);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(57, 17);
            this.label24.TabIndex = 61;
            this.label24.Text = "Card No:";
            // 
            // cardNo
            // 
            this.cardNo.Location = new System.Drawing.Point(131, 97);
            this.cardNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cardNo.Name = "cardNo";
            this.cardNo.Size = new System.Drawing.Size(172, 24);
            this.cardNo.TabIndex = 62;
            this.cardNo.TextChanged += new System.EventHandler(this.cardNo_TextChanged);
            // 
            // gerberVer
            // 
            this.gerberVer.Location = new System.Drawing.Point(131, 133);
            this.gerberVer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gerberVer.Name = "gerberVer";
            this.gerberVer.Size = new System.Drawing.Size(172, 24);
            this.gerberVer.TabIndex = 64;
            this.gerberVer.TextChanged += new System.EventHandler(this.gerberVer_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(41, 136);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(74, 17);
            this.label25.TabIndex = 63;
            this.label25.Text = "Gerber Ver:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 75;
            this.label1.Text = "İndex No:";
            // 
            // indexNo
            // 
            this.indexNo.Location = new System.Drawing.Point(125, 16);
            this.indexNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.indexNo.Name = "indexNo";
            this.indexNo.Size = new System.Drawing.Size(172, 24);
            this.indexNo.TabIndex = 76;
            this.indexNo.TextChanged += new System.EventHandler(this.indexNo_TextChanged);
            // 
            // productCode
            // 
            this.productCode.Location = new System.Drawing.Point(125, 52);
            this.productCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.productCode.Name = "productCode";
            this.productCode.Size = new System.Drawing.Size(172, 24);
            this.productCode.TabIndex = 78;
            this.productCode.TextChanged += new System.EventHandler(this.productCode_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(14, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 77;
            this.label2.Text = "Product Code:";
            // 
            // secondRubberMin
            // 
            this.secondRubberMin.Location = new System.Drawing.Point(102, 59);
            this.secondRubberMin.Name = "secondRubberMin";
            this.secondRubberMin.Size = new System.Drawing.Size(100, 24);
            this.secondRubberMin.TabIndex = 675;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(209, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 17);
            this.label15.TabIndex = 680;
            this.label15.Text = "RubberMax2:";
            // 
            // firstRubberMin
            // 
            this.firstRubberMin.Location = new System.Drawing.Point(102, 24);
            this.firstRubberMin.Name = "firstRubberMin";
            this.firstRubberMin.Size = new System.Drawing.Size(100, 24);
            this.firstRubberMin.TabIndex = 673;
            // 
            // secondRubberMax
            // 
            this.secondRubberMax.Location = new System.Drawing.Point(297, 59);
            this.secondRubberMax.Name = "secondRubberMax";
            this.secondRubberMax.Size = new System.Drawing.Size(100, 24);
            this.secondRubberMax.TabIndex = 679;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 17);
            this.label13.TabIndex = 674;
            this.label13.Text = "RubberMin1:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(209, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 17);
            this.label16.TabIndex = 678;
            this.label16.Text = "RubberMax1:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 17);
            this.label14.TabIndex = 676;
            this.label14.Text = "RubberMin2:";
            // 
            // firstRubberMax
            // 
            this.firstRubberMax.Location = new System.Drawing.Point(297, 24);
            this.firstRubberMax.Name = "firstRubberMax";
            this.firstRubberMax.Size = new System.Drawing.Size(100, 24);
            this.firstRubberMax.TabIndex = 677;
            // 
            // fourthRubberMin
            // 
            this.fourthRubberMin.Location = new System.Drawing.Point(102, 130);
            this.fourthRubberMin.Name = "fourthRubberMin";
            this.fourthRubberMin.Size = new System.Drawing.Size(100, 24);
            this.fourthRubberMin.TabIndex = 675;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(209, 133);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(85, 17);
            this.label21.TabIndex = 680;
            this.label21.Text = "RubberMax4:";
            // 
            // thirdRubberMin
            // 
            this.thirdRubberMin.Location = new System.Drawing.Point(102, 95);
            this.thirdRubberMin.Name = "thirdRubberMin";
            this.thirdRubberMin.Size = new System.Drawing.Size(100, 24);
            this.thirdRubberMin.TabIndex = 673;
            // 
            // fourthRubberMax
            // 
            this.fourthRubberMax.Location = new System.Drawing.Point(297, 130);
            this.fourthRubberMax.Name = "fourthRubberMax";
            this.fourthRubberMax.Size = new System.Drawing.Size(100, 24);
            this.fourthRubberMax.TabIndex = 679;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(14, 98);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 17);
            this.label22.TabIndex = 674;
            this.label22.Text = "RubberMin3:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(209, 98);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(85, 17);
            this.label33.TabIndex = 678;
            this.label33.Text = "RubberMax3:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(14, 133);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(82, 17);
            this.label34.TabIndex = 676;
            this.label34.Text = "RubberMin4:";
            // 
            // thirdRubberMax
            // 
            this.thirdRubberMax.Location = new System.Drawing.Point(297, 95);
            this.thirdRubberMax.Name = "thirdRubberMax";
            this.thirdRubberMax.Size = new System.Drawing.Size(100, 24);
            this.thirdRubberMax.TabIndex = 677;
            // 
            // rubberSettings
            // 
            this.rubberSettings.Controls.Add(this.secondRubberMin);
            this.rubberSettings.Controls.Add(this.fourthRubberMin);
            this.rubberSettings.Controls.Add(this.label15);
            this.rubberSettings.Controls.Add(this.firstRubberMin);
            this.rubberSettings.Controls.Add(this.label21);
            this.rubberSettings.Controls.Add(this.secondRubberMax);
            this.rubberSettings.Controls.Add(this.label13);
            this.rubberSettings.Controls.Add(this.label16);
            this.rubberSettings.Controls.Add(this.thirdRubberMin);
            this.rubberSettings.Controls.Add(this.label14);
            this.rubberSettings.Controls.Add(this.firstRubberMax);
            this.rubberSettings.Controls.Add(this.fourthRubberMax);
            this.rubberSettings.Controls.Add(this.label22);
            this.rubberSettings.Controls.Add(this.label33);
            this.rubberSettings.Controls.Add(this.label34);
            this.rubberSettings.Controls.Add(this.thirdRubberMax);
            this.rubberSettings.Location = new System.Drawing.Point(12, 209);
            this.rubberSettings.Name = "rubberSettings";
            this.rubberSettings.Size = new System.Drawing.Size(412, 165);
            this.rubberSettings.TabIndex = 699;
            this.rubberSettings.TabStop = false;
            this.rubberSettings.Text = "Rubber Değerleri";
            // 
            // operatorSettings
            // 
            this.operatorSettings.Controls.Add(this.infoPicture11);
            this.operatorSettings.Controls.Add(this.productCode);
            this.operatorSettings.Controls.Add(this.infoPicture10);
            this.operatorSettings.Controls.Add(this.label1);
            this.operatorSettings.Controls.Add(this.label2);
            this.operatorSettings.Controls.Add(this.indexNo);
            this.operatorSettings.Location = new System.Drawing.Point(12, 12);
            this.operatorSettings.Name = "operatorSettings";
            this.operatorSettings.Size = new System.Drawing.Size(412, 88);
            this.operatorSettings.TabIndex = 700;
            this.operatorSettings.TabStop = false;
            this.operatorSettings.Text = "Operatör Ayarları";
            // 
            // cardSettings
            // 
            this.cardSettings.Controls.Add(this.card4);
            this.cardSettings.Controls.Add(this.card3);
            this.cardSettings.Controls.Add(this.card2);
            this.cardSettings.Controls.Add(this.card1);
            this.cardSettings.Location = new System.Drawing.Point(449, 369);
            this.cardSettings.Name = "cardSettings";
            this.cardSettings.Size = new System.Drawing.Size(379, 60);
            this.cardSettings.TabIndex = 701;
            this.cardSettings.TabStop = false;
            this.cardSettings.Text = "Kart-Aktif-Pasif";
            // 
            // card4
            // 
            this.card4.AutoSize = true;
            this.card4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.card4.Location = new System.Drawing.Point(299, 17);
            this.card4.Name = "card4";
            this.card4.Size = new System.Drawing.Size(77, 30);
            this.card4.TabIndex = 3;
            this.card4.Text = "Kart4";
            this.card4.UseVisualStyleBackColor = true;
            // 
            // card3
            // 
            this.card3.AutoSize = true;
            this.card3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.card3.Location = new System.Drawing.Point(204, 17);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(77, 30);
            this.card3.TabIndex = 2;
            this.card3.Text = "Kart3";
            this.card3.UseVisualStyleBackColor = true;
            // 
            // card2
            // 
            this.card2.AutoSize = true;
            this.card2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.card2.Location = new System.Drawing.Point(108, 17);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(77, 30);
            this.card2.TabIndex = 1;
            this.card2.Text = "Kart2";
            this.card2.UseVisualStyleBackColor = true;
            // 
            // card1
            // 
            this.card1.AutoSize = true;
            this.card1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.card1.Location = new System.Drawing.Point(12, 17);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(77, 30);
            this.card1.TabIndex = 0;
            this.card1.Text = "Kart1";
            this.card1.UseVisualStyleBackColor = true;
            // 
            // infoPicture1
            // 
            this.infoPicture1.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture1.Image")));
            this.infoPicture1.Location = new System.Drawing.Point(320, 26);
            this.infoPicture1.Name = "infoPicture1";
            this.infoPicture1.Size = new System.Drawing.Size(20, 20);
            this.infoPicture1.TabIndex = 631;
            this.infoPicture1.TabStop = false;
            // 
            // infoPicture2
            // 
            this.infoPicture2.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture2.Image")));
            this.infoPicture2.Location = new System.Drawing.Point(320, 64);
            this.infoPicture2.Name = "infoPicture2";
            this.infoPicture2.Size = new System.Drawing.Size(20, 20);
            this.infoPicture2.TabIndex = 632;
            this.infoPicture2.TabStop = false;
            // 
            // infoPicture4
            // 
            this.infoPicture4.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture4.Image")));
            this.infoPicture4.Location = new System.Drawing.Point(320, 137);
            this.infoPicture4.Name = "infoPicture4";
            this.infoPicture4.Size = new System.Drawing.Size(20, 20);
            this.infoPicture4.TabIndex = 634;
            this.infoPicture4.TabStop = false;
            // 
            // infoPicture3
            // 
            this.infoPicture3.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture3.Image")));
            this.infoPicture3.Location = new System.Drawing.Point(320, 101);
            this.infoPicture3.Name = "infoPicture3";
            this.infoPicture3.Size = new System.Drawing.Size(20, 20);
            this.infoPicture3.TabIndex = 633;
            this.infoPicture3.TabStop = false;
            // 
            // infoPicture6
            // 
            this.infoPicture6.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture6.Image")));
            this.infoPicture6.Location = new System.Drawing.Point(320, 209);
            this.infoPicture6.Name = "infoPicture6";
            this.infoPicture6.Size = new System.Drawing.Size(20, 20);
            this.infoPicture6.TabIndex = 636;
            this.infoPicture6.TabStop = false;
            // 
            // infoPicture5
            // 
            this.infoPicture5.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture5.Image")));
            this.infoPicture5.Location = new System.Drawing.Point(320, 173);
            this.infoPicture5.Name = "infoPicture5";
            this.infoPicture5.Size = new System.Drawing.Size(20, 20);
            this.infoPicture5.TabIndex = 635;
            this.infoPicture5.TabStop = false;
            // 
            // infoPicture8
            // 
            this.infoPicture8.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture8.Image")));
            this.infoPicture8.Location = new System.Drawing.Point(320, 282);
            this.infoPicture8.Name = "infoPicture8";
            this.infoPicture8.Size = new System.Drawing.Size(20, 20);
            this.infoPicture8.TabIndex = 638;
            this.infoPicture8.TabStop = false;
            // 
            // infoPicture7
            // 
            this.infoPicture7.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture7.Image")));
            this.infoPicture7.Location = new System.Drawing.Point(320, 246);
            this.infoPicture7.Name = "infoPicture7";
            this.infoPicture7.Size = new System.Drawing.Size(20, 20);
            this.infoPicture7.TabIndex = 637;
            this.infoPicture7.TabStop = false;
            // 
            // infoPicture9
            // 
            this.infoPicture9.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture9.Image")));
            this.infoPicture9.Location = new System.Drawing.Point(320, 318);
            this.infoPicture9.Name = "infoPicture9";
            this.infoPicture9.Size = new System.Drawing.Size(20, 20);
            this.infoPicture9.TabIndex = 639;
            this.infoPicture9.TabStop = false;
            // 
            // infoPicture11
            // 
            this.infoPicture11.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture11.Image")));
            this.infoPicture11.Location = new System.Drawing.Point(316, 56);
            this.infoPicture11.Name = "infoPicture11";
            this.infoPicture11.Size = new System.Drawing.Size(20, 20);
            this.infoPicture11.TabIndex = 641;
            this.infoPicture11.TabStop = false;
            // 
            // infoPicture10
            // 
            this.infoPicture10.Image = ((System.Drawing.Image)(resources.GetObject("infoPicture10.Image")));
            this.infoPicture10.Location = new System.Drawing.Point(316, 20);
            this.infoPicture10.Name = "infoPicture10";
            this.infoPicture10.Size = new System.Drawing.Size(20, 20);
            this.infoPicture10.TabIndex = 640;
            this.infoPicture10.TabStop = false;
            // 
            // ProgAyarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(831, 430);
            this.Controls.Add(this.cardSettings);
            this.Controls.Add(this.operatorSettings);
            this.Controls.Add(this.iniSettings);
            this.Controls.Add(this.rubberSettings);
            this.Controls.Add(this.barkodSettings);
            this.Controls.Add(this.btnKaydet);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProgAyarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.AyarForm_Load);
            this.iniSettings.ResumeLayout(false);
            this.iniSettings.PerformLayout();
            this.barkodSettings.ResumeLayout(false);
            this.barkodSettings.PerformLayout();
            this.rubberSettings.ResumeLayout(false);
            this.rubberSettings.PerformLayout();
            this.operatorSettings.ResumeLayout(false);
            this.operatorSettings.PerformLayout();
            this.cardSettings.ResumeLayout(false);
            this.cardSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPicture10)).EndInit();
            this.ResumeLayout(false);

        }

        private bool checkedBarkod()
        {
            if (companyNo.Text.Length == 2)
            {
                companyNo.BackColor = Color.Green;
            }
            else
            {
                companyNo.BackColor = Color.Red;
                return false;
            }

            if (SAPNo.Text.Length == 10)
            {
                SAPNo.BackColor = Color.Green;
            }
            else
            {
                SAPNo.BackColor = Color.Red;
                return false;
            }

            if (indexNo.Text.Length == 6)
            {
                indexNo.BackColor = Color.Green;
            }
            else
            {
                indexNo.BackColor = Color.Red;
                return false;
            }

            if (productCode.Text.Length == 14)
            {
                productCode.BackColor = Color.Green;
            }
            else
            {
                productCode.BackColor = Color.Red;
                return false;
            }

            if (cardNo.Text.Length == 2)
            {
                cardNo.BackColor = Color.Green;
            }
            else
            {
                cardNo.BackColor = Color.Red;
                return false;
            }

            if (gerberVer.Text.Length == 2)
            {
                gerberVer.BackColor = Color.Green;
            }
            else
            {
                gerberVer.BackColor = Color.Red;
                return false;
            }

            if (BOMVer.Text.Length == 2)
            {
                BOMVer.BackColor = Color.Green;
            }
            else
            {
                BOMVer.BackColor = Color.Red;
                return false;
            }

            if (ICTRev.Text.Length == 2)
            {
                ICTRev.BackColor = Color.Green;
            }
            else
            {
                ICTRev.BackColor = Color.Red;
                return false;
            }

            if (FCTRev.Text.Length == 2)
            {
                FCTRev.BackColor = Color.Green;
            }
            else
            {
                FCTRev.BackColor = Color.Red;
                return false;
            }

            if (softwareVer.Text.Length == 2)
            {
                softwareVer.BackColor = Color.Green;
            }
            else
            {
                softwareVer.BackColor = Color.Red;
                return false;
            }

            if (softwareRev.Text.Length == 2)
            {
                softwareRev.BackColor = Color.Green;
            }
            else
            {
                softwareRev.BackColor = Color.Red;
                return false;
            }
            return true;
        }

        private void companyNo_TextChanged(object sender, EventArgs e)
        {
            if(companyNo.Text.Length == 2)
            {
                companyNo.BackColor = Color.Green;
            }
            else
            {
                companyNo.BackColor = Color.Red;
            }
        }

        private void SAPNo_TextChanged(object sender, EventArgs e)
        {
            if (SAPNo.Text.Length == 10)
            {
                SAPNo.BackColor = Color.Green;
            }
            else
            {
                SAPNo.BackColor = Color.Red;
            }
        }

        private void indexNo_TextChanged(object sender, EventArgs e)
        {
            if (indexNo.Text.Length == 6)
            {
                indexNo.BackColor = Color.Green;
            }
            else
            {
                indexNo.BackColor = Color.Red;
            }
        }

        private void productCode_TextChanged(object sender, EventArgs e)
        {
            if (productCode.Text.Length == 14)
            {
                productCode.BackColor = Color.Green;
            }
            else
            {
                productCode.BackColor = Color.Red;
            }
        }

        private void cardNo_TextChanged(object sender, EventArgs e)
        {
            if (cardNo.Text.Length == 2)
            {
                cardNo.BackColor = Color.Green;
            }
            else
            {
                cardNo.BackColor = Color.Red;
            }
        }

        private void gerberVer_TextChanged(object sender, EventArgs e)
        {
            if (gerberVer.Text.Length == 2)
            {
                gerberVer.BackColor = Color.Green;
            }
            else
            {
                gerberVer.BackColor = Color.Red;
            }
        }

        private void BOMVer_TextChanged(object sender, EventArgs e)
        {
            if (BOMVer.Text.Length == 2)
            {
                BOMVer.BackColor = Color.Green;
            }
            else
            {
                BOMVer.BackColor = Color.Red;
            }
        }

        private void ICTRev_TextChanged(object sender, EventArgs e)
        {
            if (ICTRev.Text.Length == 2)
            {
                ICTRev.BackColor = Color.Green;
            }
            else
            {
                ICTRev.BackColor = Color.Red;
            }
        }

        private void FCTRev_TextChanged(object sender, EventArgs e)
        {
            if (FCTRev.Text.Length == 2)
            {
                FCTRev.BackColor = Color.Green;
            }
            else
            {
                FCTRev.BackColor = Color.Red;
            }
        }

        private void softwareVer_TextChanged(object sender, EventArgs e)
        {
            if (softwareVer.Text.Length == 2)
            {
                softwareVer.BackColor = Color.Green;
            }
            else
            {
                softwareVer.BackColor = Color.Red;
            }
        }

        private void softwareRev_TextChanged(object sender, EventArgs e)
        {
            if (softwareRev.Text.Length == 2)
            {
                softwareRev.BackColor = Color.Green;
            }
            else
            {
                softwareRev.BackColor = Color.Red;
            }
        }

    }
}
