using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using LANDFCT.Printer;
using System.Printing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;

namespace LANDFCT
{
    public partial class Main : Form
    {
        private Thread saniyeThread = null;
        public AyarForm AyarFrm;
        public Sifre SifreFrm;
        public Error ErrorFrm;
        public ProgAyarForm ProgAyarFrm;

        private IntPtr ShellHwnd;
        private DateTime lastDateTime = DateTime.Now;
        private ModbusClient modbusClientPLC = null;

        const int M0 = 2048;
        const int M1 = 2049;
        const int M2 = 2050;
        const int M3 = 2051;
        const int M4 = 2052;
        const int M5 = 2053;
        const int M6 = 2054;
        const int M7 = 2055;
        const int M8 = 2056;
        const int M9 = 2057;
        const int M10 = 2058;
        const int M11 = 2059;
        const int M12 = 2060;
        const int M13 = 2061;
        const int M14 = 2062;
        const int M15 = 2063;
        const int M16 = 2064;
        const int M17 = 2065;
        const int M18 = 2066;
        const int M19 = 2067;
        const int M20 = 2068;
        const int M21 = 2069;
        const int M22 = 2070;
        const int M23 = 2071;
        const int M24 = 2072;
        const int M25 = 2073;
        const int M26 = 2074;
        const int M27 = 2075;
        const int M28 = 2076;
        const int M29 = 2077;
        const int M30 = 2078;
        const int M31 = 2079;
        const int M32 = 2080;
        const int M33 = 2081;
        const int M34 = 2082;
        const int M35 = 2083;
        const int M36 = 2084;
        const int M37 = 2085;
        const int M38 = 2086;
        const int M39 = 2087;
        const int M40 = 2088;
        const int M41 = 2089;
        const int M42 = 2090;
        const int M43 = 2091;
        const int M44 = 2092;
        const int M45 = 2093;
        const int M46 = 2094;
        const int M47 = 2095;
        const int M48 = 2096;
        const int M49 = 2097;
        const int M50 = 2098;
        const int M51 = 2099;
        const int M52 = 2100;
        const int M53 = 2101;
        const int M54 = 2102;
        const int M55 = 2103;
        const int M56 = 2104;
        const int M57 = 2105;
        const int M58 = 2106;
        const int M59 = 2107;
        const int M60 = 2108;

        const int M100 = 2148; 
        const int M101 = 2149;

        const int X0 = 1024;
        const int X1 = 1025;
        const int X2 = 1026;
        const int X3 = 1027;
        const int X4 = 1028;
        const int X5 = 1029;
        const int X6 = 1030;
        const int X7 = 1031;
        const int X10 = 1032;
        const int X11 = 1033;
        const int X12 = 1034;
        const int X13 = 1035;
        const int X21 = 1041;
        const int X23 = 1043;
        const int X25 = 1045;
        const int X27 = 1047;
        const int X31 = 1049;
        const int X33 = 1051;
        const int X20 = 1040;
        const int X22 = 1042;
        const int X24 = 1044;
        const int X26 = 1046;
        const int X30 = 1048;
        const int X32 = 1050;

        const int D4 = 4100;
        const int D6 = 4102;
        const int D8 = 4104;
        const int D10 = 4106;
        const int D12 = 4108;
        const int D14 = 4110;
        const int D16 = 4112;
        const int D18 = 4114;
        const int D20 = 4116;
        const int D30 = 4126;
        const int D40 = 4136;
        const int D100 = 4196;
        const int D101 = 4197;
        const int D102 = 4198;
        const int D103 = 4199;
        const int D400 = 4496;
        const int D405 = 4501;
        const int D410 = 4506;
        const int D415 = 4511;

        const int Y0 = 1280;
        const int Y1 = 1281;
        const int Y2 = 1282;
        const int Y3 = 1283;
        const int Y4 = 1284;
        const int Y5 = 1285;
        const int Y6 = 1286;
        const int Y7 = 1287;
        const int Y10 = 1288;
        const int Y11 = 1289;
        const int Y12 = 1290;
        const int Y13 = 1291;
        const int Y20 = 1296;
        const int Y21 = 1297;
        const int Y22 = 1298;
        const int Y23 = 1299;
        const int Y24 = 1300;
        const int Y25 = 1301;
        const int Y40 = 1312;
        const int Y41 = 1313;
        const int Y42 = 1314;
        const int Y43 = 1315;
        const int Y44 = 1316;
        const int Y45 = 1317;
        const int Y46 = 1318;
        const int Y47 = 1319;
        const int Y50 = 1320;
        const int Y51 = 1321;
        const int Y52 = 1322;
        const int Y53 = 1323;
        const int Y35 = 1309;
        const int Y36 = 1310;
        const int Y37 = 1311;
        const int FCT_CARD_NUMBER = 4;

        //Sıfırlanmamalı
        int totalCard = 0;
        int errorCard = 0;
        public string customMessageBoxTitle = "";
        string logDosyaPath = "";
        string printerName;

        //Sıfırlanmalı
        int adminTimerCounter = 0;
        int saniyeTimerCounter = 0;
        int fctSaniye = 0;

        //Sıfırlanmalı
        public int yetki = 0;
        string[] filePathTxt = new string[FCT_CARD_NUMBER + 1];
        public string[] cardStokNum = new string[FCT_CARD_NUMBER + 1];

        //Sıfırlanmalı
        bool[] cardResult = new bool[FCT_CARD_NUMBER + 1];
        bool[] arrayLEDTotal = new bool[31];
        string[] barcode50 = new string[FCT_CARD_NUMBER + 1];
        bool[] arrayRubberResult = new bool[FCT_CARD_NUMBER + 1];

        //Sabitler
        int firstRubberMin = 0;
        int firstRubberMax = 0;
        int secondRubberMin = 0;
        int secondRubberMax = 0;
        int thirdRubberMin = 0;
        int thirdRubberMax = 0;
        int fourthRubberMin = 0;
        int fourthRubberMax = 0;

        int firstRubberValue = 0;
        int secondRubberValue = 0;
        int thirdRubberValue = 0;
        int fourthRubberValue = 0;

        //OKUNAN
        SqlConnection SQLConnection;
        bool sqlConnection = false;
        string urun_id = "";
        string urun_barkod = "";
        string son_istasyon_id = "";
        string giris_zamani = "";
        string son_istasyon_zamani = "";
        string urun_durum_no = "";
        string ariza_kodu = "";
        string tamir_edildi = "";
        string son_islem_tamamlandi = "";
        string firma_no = "";
        string urun_kodu = "";
        string panacim_kodu = "";
        string parti_no = "";
        string alan_5 = "";
        string alan_6 = "";
        string alan_7 = "";
        string pcb_barkod = "";

        //OLUŞTURULAN
        string companyNo;
        string sapNo;
        string productDate;
        string indexNo;
        string productNo;
        string cardType;
        string gerberVer;
        string bomVer;
        string ictRev;
        string fctRev;
        string softwareVer;
        string softwareRev;

        const string POTA_STATION = "1";
        const string PAKETLEME_STATION = "5";
        const string ICT_STATION_ISTANBUL = "15";
        const string ICT_STATION_BOLU_1 = "19";
        const string ICT_STATION_BOLU_2 = "22";
        const string ALPPLAS_STATION_LANDIRENZO_CAMERA = "34";

        const string URUN_DURUM_HURDA = "2";
        const string URUN_DURUM_BEKLETILIYOR = "3";
        const string URUN_DURUM_TAMIR_EDILECEK = "4";
        const string URUN_DURUM_PROCESS = "5";
        const string URUN_DURUM_TAMIR_EDILDI = "6";
        const string URUN_DURUM_HAZIR = "7";
        const string URUN_DURUM_SEVK_EDILECEK = "8";

        const string ARIZA_YOK = "0";
        const string CHECKSUM_HATA = "1";
        const string ARTOUCH_FCT_HATA = "5";
        const string READ_SOFTWARE = "23";
        const string DUO_TESTI = "32";

        public bool traceabilityStatus = false;
        string binary1 = "00000000";
        string binary2 = "00000000";
        string binary3 = "00000000";
        string binary4 = "00000000";

        public Main()
        {
            this.AyarFrm = new AyarForm();
            this.AyarFrm.MainFrm = this;
            this.SifreFrm = new Sifre();
            this.SifreFrm.MainFrm = this;
            this.ErrorFrm = new Error();
            this.ErrorFrm.MainFrm = this;
            this.ProgAyarFrm = new ProgAyarForm();
            this.ProgAyarFrm.MainFrm = this;
            InitializeComponent();
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

        [DllImport("user32.dll")]
        public static extern byte ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string ClassName, string WindowName);

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (traceabilityStatus)
            {
                if (sqlConnection)
                {
                    sqlConnection = false;
                    SQLConnection.Close();
                }
            }
            if (saniyeThread != null)
            {
                saniyeThread.Abort();
            }
            if (printerTh != null)
            {
                printerTh.Abort();
            }
            modbusClientPLC.Disconnect();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.ShellHwnd = Main.FindWindow("Shell TrayWnd", (string)null);
            IntPtr shellHwnd = this.ShellHwnd;
            int num1 = (int)Main.ShowWindow(this.ShellHwnd, 0);
            traceabilityStatus = Ayarlar.Default.chBoxIzlenebilirlik;
            sqlCommonConnection();
            /*barcodeAtama();
            FCT_Success(1);*/

            if (sqlConnection)
            {
                settingGetInit();
                FCT_Clear();
                this.yetkidegistir();
                saniyeThread = new Thread(saniyeThreadFunc);
                saniyeThread.Start();
                ModBusWriteSingleCoils(M0, false);    //Güvenlik Biti
                ModBusWriteSingleCoils(M100, false);  //1.Start Biti
            }
            //barcode50[1] = "12345678901234567890123456789012345678901234567890";
            //printerFunction(barcode50[1], 1);
        }

        private void settingGetInit()
        {
            this.customMessageBoxTitle = Ayarlar.Default.projectName;
            this.projectNameTxt.Text = customMessageBoxTitle;
            this.Text = customMessageBoxTitle;
            this.logDosyaPath = Ayarlar.Default.txtLogDosya;

            modbusClientPLC = new ModbusClient(Ayarlar.Default.SerialPort2Com);
            modbusClientPLC.UnitIdentifier = 1; //Not necessary since default slaveID = 1;
            modbusClientPLC.Baudrate = Ayarlar.Default.SerialPort2Baud;   // Not necessary since default baudrate = 9600
            modbusClientPLC.Parity = Ayarlar.Default.SerialPort2Parity;
            modbusClientPLC.StopBits = Ayarlar.Default.SerialPort2stopBit;
            modbusClientPLC.ConnectionTimeout = 200;

            this.timerAdmin.Interval = Ayarlar.Default.timerAdmin;
            this.printerName = Ayarlar.Default.printerName;
            this.waitTimer.Interval = Ayarlar.Default.SerialTx1Timer;

            firstRubberMin = Convert.ToInt32(Prog_Ayarlar.Default.firstRubberMin);
            firstRubberMax = Convert.ToInt32(Prog_Ayarlar.Default.firstRubberMax);
            secondRubberMin = Convert.ToInt32(Prog_Ayarlar.Default.secondRubberMin);
            secondRubberMax = Convert.ToInt32(Prog_Ayarlar.Default.secondRubberMax);
            thirdRubberMin = Convert.ToInt32(Prog_Ayarlar.Default.thirdRubberMin);
            thirdRubberMax = Convert.ToInt32(Prog_Ayarlar.Default.thirdRubberMax);
            fourthRubberMin = Convert.ToInt32(Prog_Ayarlar.Default.fourthRubberMin);
            fourthRubberMax = Convert.ToInt32(Prog_Ayarlar.Default.fourthRubberMax);

            if (Ayarlar.Default.chBoxSerial2)  //PLC
            {
                try
                {
                    modbusClientPLC.Connect();
                    lblStatusCom2.Text = "ON";
                    lblStatusCom2.BackColor = Color.Green;
                }
                catch (Exception ex)
                {
                    int num2 = (int)MessageBox.Show("PLC Port Hatası: " + ex.ToString());
                    lblStatusCom2.Text = "OFF";
                    lblStatusCom2.BackColor = Color.Red;
                }
            }
        }

        /****************************************** MODBUS *************************************************/
        private bool ModBusReadCoils(int address, int length)
        {
            if (modbusClientPLC.Connected)
            {
                try
                {
                    return modbusClientPLC.ReadCoils(address, length)[0];
                }
                catch
                {
                    ConsoleAppendLine("ModBus Read Coil Hatası." + address, Color.Red);
                    return false;
                }
            }
            else
            {
                ConsoleAppendLine("ModBus Kapalı Hatası." + address, Color.Red);
                return false;
            }
        }

        private void ModBusWriteSingleCoils(int address, bool state)
        {
            if (modbusClientPLC.Connected)
            {
                try
                {
                    modbusClientPLC.WriteSingleCoil(address, state);
                    ConsoleAppendLine(Convert.ToString(address) + "=" + Convert.ToString(state), Color.Green);
                }
                catch
                {
                    ConsoleAppendLine("ModBus WriteSingle Coil Hatası." + address, Color.Red);
                }
            }
            else
            {
                ConsoleAppendLine("ModBus Kapalı Hatası." + address, Color.Red);
            }
        }

        private int ModBusReadHoldingRegisters(int address, int length)
        {
            if (modbusClientPLC.Connected)
            {
                try
                {
                    return modbusClientPLC.ReadHoldingRegisters(address, length)[0];
                }
                catch
                {
                    ConsoleAppendLine("ModBus ReadHoldingRegisters Coil Hatası." + address, Color.Red);
                    return 0;
                }
            }
            else
            {
                ConsoleAppendLine("ModBus Kapalı Hatası." + address, Color.Red);
                return 0;
            }
        }

        private bool ModBusReadDiscreteInputs(int address, int length)
        {
            if (modbusClientPLC.Connected)
            {
                try
                {
                    return modbusClientPLC.ReadDiscreteInputs(address, length)[0];
                }
                catch
                {
                    ConsoleAppendLine("ModBus ReadDiscreteInputs Hatası." + address, Color.Red);
                    return false;
                }
            }
            else
            {
                ConsoleAppendLine("ModBus Kapalı Hatası." + address, Color.Red);
                return false;
            }
        }

        /****************************************** SQL *************************************************/
        public void sqlCommonConnection()
        {
            if (traceabilityStatus)
            {
                if (sqlConnection == false)
                {
                    try
                    {
                        string connetionString = @"Data Source=192.168.0.8\MEYER;Initial Catalog=Alpplas_Uretim_Takip;User ID=Alpplas_user;Password=Alp-User-21*";
                        SQLConnection = new SqlConnection(connetionString);
                        SQLConnection.Open();
                        ConsoleAppendLine("SQL Baglantısı Açıldı", Color.Green);
                        sqlConnection = true;
                        lblStatusSQL.Text = "ON";
                        lblStatusSQL.BackColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        sqlConnection = false;
                        lblStatusSQL.Text = "OFF";
                        lblStatusSQL.BackColor = Color.Red;
                        ConsoleAppendLine("sqlCommonConnection Error: " + ex.Message, Color.Red);
                    }
                }
            }
            else
            {
                lblStatusSQL.Text = "OFF";
                lblStatusSQL.BackColor = Color.Red;
                sqlConnection = true;
            }
        }

        public void sqlWriteError()
        {
            sqlConnection = false;
            lblStatusSQL.Text = "OFF";
            lblStatusSQL.BackColor = Color.Red;
            ConsoleAppendLine("sqlWriteError()", Color.Red);
        }

        bool urunlerInsert(object data, string urun_durum_no)
        {
            if (traceabilityStatus)
            {
                sqlCommonConnection();
                try
                {
                    string fullproductCode = (string)data;
                    string company_no = string.Empty;
                    string sap_no = string.Empty;
                    string product_date = string.Empty;
                    string index_no = string.Empty;
                    string product_no = string.Empty;
                    string card_type = string.Empty;
                    string gerber_ver = string.Empty;
                    string bom_ver = string.Empty;
                    string ict_rev = string.Empty;
                    string fct_rev = string.Empty;
                    string software_ver = string.Empty;
                    string software_rev = string.Empty;

                    string aciklama = "LANDIRENZO EOL STATION";
                    company_no = fullproductCode.Substring(0, 2);
                    sap_no = fullproductCode.Substring(2, 10);
                    product_date = fullproductCode.Substring(12, 4);
                    index_no = fullproductCode.Substring(16, 6);
                    product_no = fullproductCode.Substring(22, 14);
                    card_type = fullproductCode.Substring(36, 2);
                    gerber_ver = fullproductCode.Substring(38, 2);
                    bom_ver = fullproductCode.Substring(40, 2);
                    ict_rev = fullproductCode.Substring(42, 2);
                    fct_rev = fullproductCode.Substring(44, 2);
                    software_ver = fullproductCode.Substring(46, 2);
                    software_rev = fullproductCode.Substring(48, 2);

                    DateTime dt = DateTime.Now;
                    string nowYear = Convert.ToString(dt.Year);
                    string nowMonth = Convert.ToString(dt.Month);
                    string nowDay = Convert.ToString(dt.Day);
                    string nowHour = Convert.ToString(dt.Hour);
                    string nowMinute = Convert.ToString(dt.Minute);
                    string nowSecond = Convert.ToString(dt.Second);
                    string mnowSecond = Convert.ToString(dt.Millisecond);
                    string lastTime = nowYear + "-" + nowMonth + "-" + nowDay + " " + nowHour + ":" + nowMinute + ":" + nowSecond + "." + mnowSecond;
                    //  string lastTime = "2021-05-03 14:41:10.587";

                    string sql2 = "INSERT INTO URUNLER (URUN_BARKOD, SON_ISTASYON_ID, GIRIS_ZAMANI, SON_ISTASYON_ZAMANI, URUN_DURUM_NO, ARIZA_KODU, TAMIR_EDILDI, SON_ISLEM_TAMAMLANDI, FIRMA_NO, URUN_KODU, PANACIM_KODU, PARTI_NO, ALAN_5, ALAN_6, ALAN_7, PCB_BARKOD) VALUES('"
                    + fullproductCode + "'," + "'" + ALPPLAS_STATION_LANDIRENZO_CAMERA + "'," + "'" + lastTime + "'," + "'" + lastTime + "'," + "'" + urun_durum_no + "'," + "'0'," + "NULL," + "'1'," + "'" + company_no + "'," + "'" + sap_no + "'," + "'" + product_no + "'," + "'" + index_no + "'," + "NULL," + "NULL," + "'" + aciklama + "'," + "NULL" + ")";
                    SqlCommand command2 = new SqlCommand(sql2, SQLConnection);
                    SqlDataReader dataReader2 = command2.ExecuteReader();
                    while (dataReader2.Read())
                    {
                        if (command2.ExecuteNonQuery() == 1)
                        {
                            ConsoleAppendLine("SQL Success 1", Color.Green);
                        }
                        else
                        {
                            ConsoleAppendLine("SQL Success 2", Color.Green);
                        }
                    }
                    ConsoleAppendLine("Kart Veritabanına Eklendi", Color.Green);
                    dataReader2.Close();
                    if (!urunlerRead(fullproductCode))
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    sqlWriteError();  //INSERT
                    ConsoleAppendLine("Kart Veritabanına Eklenemedi", Color.Red);
                    ConsoleAppendLine("urunlerInsert Error: " + ex.Message, Color.Red);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool urunlerRead(string fullproductCode)
        {
            if (traceabilityStatus)
            {
                sqlCommonConnection();
                if (sqlConnection)
                {
                    try
                    {
                        string sql1 = "SELECT URUN_ID, URUN_BARKOD, SON_ISTASYON_ID, GIRIS_ZAMANI, SON_ISTASYON_ZAMANI, URUN_DURUM_NO, ARIZA_KODU, TAMIR_EDILDI, SON_ISLEM_TAMAMLANDI, FIRMA_NO, URUN_KODU, PANACIM_KODU, PARTI_NO, ALAN_5, ALAN_6, ALAN_7, PCB_BARKOD FROM URUNLER WHERE URUN_BARKOD='" + fullproductCode + "'";
                        SqlCommand command1 = new SqlCommand(sql1, SQLConnection);
                        SqlDataReader dataReader1 = command1.ExecuteReader(CommandBehavior.CloseConnection);

                        bool findState = false;
                        dataReader1.Read();
                        findState = dataReader1.HasRows;
                        if (findState)
                        {
                            urun_id = Convert.ToString(dataReader1.GetValue(0));
                            urun_barkod = Convert.ToString(dataReader1.GetValue(1));
                            son_istasyon_id = Convert.ToString(dataReader1.GetValue(2));
                            giris_zamani = Convert.ToString(dataReader1.GetValue(3));
                            son_istasyon_zamani = Convert.ToString(dataReader1.GetValue(4));
                            urun_durum_no = Convert.ToString(dataReader1.GetValue(5));
                            ariza_kodu = Convert.ToString(dataReader1.GetValue(6));
                            tamir_edildi = Convert.ToString(dataReader1.GetValue(7));
                            son_islem_tamamlandi = Convert.ToString(dataReader1.GetValue(8));
                            firma_no = Convert.ToString(dataReader1.GetValue(9));
                            urun_kodu = Convert.ToString(dataReader1.GetValue(10));
                            panacim_kodu = Convert.ToString(dataReader1.GetValue(11));
                            parti_no = Convert.ToString(dataReader1.GetValue(12));
                            alan_5 = Convert.ToString(dataReader1.GetValue(13));
                            alan_6 = Convert.ToString(dataReader1.GetValue(14));
                            alan_7 = Convert.ToString(dataReader1.GetValue(15));
                            pcb_barkod = Convert.ToString(dataReader1.GetValue(16));
                            ConsoleAppendLine("Ürün Id: " + urun_id, Color.Black);
                            ConsoleAppendLine("Son İstasyon Id: " + son_istasyon_id, Color.Black);
                            ConsoleAppendLine("İlk Giriş Zamanı: " + giris_zamani, Color.Black);
                            ConsoleAppendLine("Son İstasyon Zamanı: " + son_istasyon_zamani, Color.Black);
                            ConsoleAppendLine("Ürün Durum No: " + urun_durum_no, Color.Black);
                            ConsoleAppendLine("Arıza Kodu: " + ariza_kodu, Color.Black);
                            ConsoleAppendLine("Tamir Edildi: " + tamir_edildi, Color.Black);
                            ConsoleAppendLine("Son İşlem Tamamlandı: " + son_islem_tamamlandi, Color.Black);
                            ConsoleNewLine();
                            urunDurum();
                            sonIstasyonDurum();
                            arizaDurum();

                            dataReader1.Close();
                            if (sqlConnection)
                            {
                                sqlConnection = false;
                                SQLConnection.Close();
                            }
                        }
                        else
                        {
                            dataReader1.Close();
                            if (sqlConnection)
                            {
                                sqlConnection = false;
                                SQLConnection.Close();
                            }
                            ConsoleNewLine();
                            ConsoleNewLine();
                            ConsoleAppendLine("YANLIŞ BARKOD YA DA ÜRÜN SİSTEM'DE KAYITLI DEĞİL!", Color.Red);
                            return false;
                        }

                        ConsoleNewLine();
                        ConsoleNewLine();
                        if (son_istasyon_id == POTA_STATION && urun_durum_no == URUN_DURUM_HAZIR && son_islem_tamamlandi == "True")
                        {
                            ConsoleAppendLine("ÜRÜN POTADAN'DAN GEÇMİŞ ICT'YE GİRMELİ", Color.Green);
                            return false;
                        }
                        else if (son_istasyon_id == POTA_STATION && urun_durum_no == URUN_DURUM_TAMIR_EDILDI && son_islem_tamamlandi == "True" && tamir_edildi == "True")
                        {
                            ConsoleAppendLine("ÜRÜN TAMİR'DEN GEÇMİŞ FCT'YE GİREBİLİR", Color.Green);
                            return true;
                        }
                        else if ((son_istasyon_id == ICT_STATION_BOLU_1 || son_istasyon_id == ICT_STATION_BOLU_2 || son_istasyon_id == ICT_STATION_ISTANBUL) && urun_durum_no == URUN_DURUM_HAZIR && son_islem_tamamlandi == "True")
                        {
                            ConsoleAppendLine("ÜRÜN ICT'DEN GEÇMİŞ FCT'YE GİREBİLİR", Color.Green);
                            return true;
                        }
                        else if ((son_istasyon_id == ICT_STATION_BOLU_1 || son_istasyon_id == ICT_STATION_BOLU_2 || son_istasyon_id == ICT_STATION_ISTANBUL) && (urun_durum_no == URUN_DURUM_PROCESS && urun_durum_no == URUN_DURUM_TAMIR_EDILECEK) && son_islem_tamamlandi == "False")
                        {
                            ConsoleAppendLine("ÜRÜN ICT'DEN KALMIŞ FCT'YE GİREMEZ", Color.Red);
                            return false;
                        }
                        else if ((son_istasyon_id == ALPPLAS_STATION_LANDIRENZO_CAMERA) && urun_durum_no == URUN_DURUM_TAMIR_EDILECEK && son_islem_tamamlandi == "True")
                        {
                            ConsoleAppendLine("KART TAMİRE GİRMELİ YA DA TEKRAR FCT'YE GİREBİLİR", Color.Green);
                            return true;
                        }
                        else if ((son_istasyon_id == ALPPLAS_STATION_LANDIRENZO_CAMERA) && urun_durum_no == URUN_DURUM_HAZIR && son_islem_tamamlandi == "True")
                        {
                            ConsoleAppendLine("ÜRÜN FCT'DEN DAHA ÖNCE GEÇTİ FCT'YE GİREBİLİR", Color.Green);
                            return true;
                        }
                        else if (son_istasyon_id == PAKETLEME_STATION)
                        {
                            ConsoleAppendLine("KART PAKETLEMEDEN GEÇMİŞ FCT-ICT'YE SOKMAYIN", Color.Orange);
                            return false;
                        }
                        else
                        {
                            ConsoleAppendLine("KART BİR ÖNCEKİ İSTASYONA GİRMELİ", Color.Red);
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlWriteError();
                        ConsoleAppendLine("urunlerRead Error: " + ex.Message, Color.Red);
                        return false;
                    }
                }
                else
                {
                    ConsoleAppendLine("SQL BAĞLANTI KAPALI", Color.Red);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void sonIstasyonDurum()
        {
            if (son_istasyon_id == POTA_STATION)
            {
                if (urun_durum_no == URUN_DURUM_HAZIR)
                {
                    ConsoleAppendLine("SON GİRDİĞİ İSTASYON: POTA", Color.Green);
                }
                else if (urun_durum_no == URUN_DURUM_TAMIR_EDILDI)
                {
                    ConsoleAppendLine("SON GİRDİĞİ İSTASYON: TAMİR", Color.Green);
                }
            }
            else if (son_istasyon_id == PAKETLEME_STATION)
            {
                ConsoleAppendLine("SON GİRDİĞİ İSTASYON: PAKETLEME", Color.Green);
            }
            else if (son_istasyon_id == ICT_STATION_BOLU_1)
            {
                ConsoleAppendLine("SON GİRDİĞİ İSTASYON: ICT-1", Color.Green);
            }
            else if (son_istasyon_id == ICT_STATION_BOLU_2)
            {
                ConsoleAppendLine("SON GİRDİĞİ İSTASYON: ICT-2", Color.Green);
            }
            else if (son_istasyon_id == ICT_STATION_ISTANBUL)
            {
                ConsoleAppendLine("SON GİRDİĞİ İSTASYON: ICT-İSTANBUL", Color.Green);
            }
            else if (son_istasyon_id == ALPPLAS_STATION_LANDIRENZO_CAMERA)
            {
                ConsoleAppendLine("SON GİRDİĞİ İSTASYON: ALPPLAS_STATION_LANDIRENZO FCT", Color.Green);
            }
        }

        private void urunDurum()
        {
            if (son_istasyon_id == PAKETLEME_STATION)
            {
                ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN SEVKİYATA HAZIR", Color.Green);
            }
            else
            {
                if (urun_durum_no == URUN_DURUM_HURDA)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN HURDA", Color.Red);
                }
                else if (urun_durum_no == URUN_DURUM_BEKLETILIYOR)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN BEKLETİLİYOR", Color.Red);
                }
                else if (urun_durum_no == URUN_DURUM_TAMIR_EDILECEK)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN TAMİR EDİLECEK", Color.Red);
                }
                else if (urun_durum_no == URUN_DURUM_PROCESS)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN TEST EDİLİYOR VEYA İŞLEM ALTINDA", Color.Green);
                }
                else if (urun_durum_no == URUN_DURUM_TAMIR_EDILDI)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN TAMİR EDİLDİ", Color.Green);
                }
                else if (urun_durum_no == URUN_DURUM_HAZIR)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN BİR SONRAKİ TESTE HAZIR", Color.Green);
                }
                else if (urun_durum_no == URUN_DURUM_SEVK_EDILECEK)
                {
                    ConsoleAppendLine("ÜRÜN DURUM: ÜRÜN SEVKİYATA HAZIR", Color.Green);
                }
            }
        }

        private void arizaDurum()
        {
            if (ariza_kodu == ARIZA_YOK)
            {
                ConsoleAppendLine("ARIZA DURUM: ARIZA_YOK", Color.Green);
            }
            else if (ariza_kodu == CHECKSUM_HATA)
            {
                ConsoleAppendLine("ARIZA DURUM: CHECKSUM_HATA", Color.Red);
            }
            else if (ariza_kodu == READ_SOFTWARE)
            {
                ConsoleAppendLine("ARIZA DURUM: YAZILIM_TESTİ_HATA", Color.Red);
            }
            else if (ariza_kodu == DUO_TESTI)
            {
                ConsoleAppendLine("ARIZA DURUM: HABERLEŞME_HATA", Color.Red);
            }
        }

        /****************************************** INIT *************************************************/
        public bool fctState = false;
        private void timerInit_Tick(object sender, EventArgs e)
        {
            if (/*ModBusReadCoils(M0, 1) && */ModBusReadDiscreteInputs(X3, 1) && ModBusReadDiscreteInputs(X5, 1) && ModBusReadDiscreteInputs(X7, 1) && ModBusReadDiscreteInputs(X11, 1) && fctState == false)  //Güvenlik Biti Kontrol ve Pistonlar Aşağıda Kontrol  (TEST BAŞLADI)
            {
                fctState = true;
                timerInit.Stop();
                timerInit.Enabled = false;
                lblTimer1.BackColor = Color.Transparent;
                lblTimer1.Text = "OFF";
                timerEmergencyStop.Start();
                lblTimer2.BackColor = Color.Green;
                lblTimer2.Text = "ON";
                FCTInit();
            }
        }

        private void timerEmergencyStop_Tick(object sender, EventArgs e)
        {/*
            if (ModBusReadDiscreteInputs(X0, 1) == false && ModBusReadDiscreteInputs(X2, 1))  //Acil Basıldı 
            {
                timerEmergencyStop.Stop();
                timerEmergencyStop.Enabled = false;
                lblTimer2.BackColor = Color.Transparent;
                lblTimer2.Text = "OFF";
                FCT_Finish();
            }*/
        }

        private void FCTInit()
        {
            if (modbusClientPLC.Connected)
            {
                ModBusWriteSingleCoils(M100, true);      // Test Başladı
                //ModBusWriteSingleCoils(M101, false);     // Test Bitti
                saniyeState = true;
                Thread.Sleep(500);
                barcodeAtama();
                for (int i = 1; i <= 4; i++)
                {
                    textCreate(i);
                    Thread.Sleep(200);
                }
                btnFCTInit.BackColor = Color.Green;
                btnFCTInit.Text = "TEST BAŞLADI";
                waitTimer.Start();
            }
            else
            {
                CustomMessageBox.ShowMessage("PLC Bağlantısını Kontrol Ediniz!", customMessageBoxTitle, MessageBoxButtons.OK, CustomMessageBoxIcon.Information, Color.Red);
                FCT_Finish();
            }
        }

        private void barcodeAtama()
        {
            companyNo = Prog_Ayarlar.Default.companyNo;
            sapNo = Prog_Ayarlar.Default.SAPNo;
            DateTime dt = DateTime.Now;
            string hafta = Convert.ToString(weekNum(dt));
            if (hafta.Length == 1)
                hafta = "0" + hafta;
            string yil = Convert.ToString(dt.Year);
            yil = yil.Substring(2, 2);
            productDate = hafta + yil;
            //index
            productNo = Prog_Ayarlar.Default.productCode;
            cardType = Prog_Ayarlar.Default.cardNo;
            gerberVer = Prog_Ayarlar.Default.gerberVer;
            bomVer = Prog_Ayarlar.Default.BOMVer;
            ictRev = Prog_Ayarlar.Default.ICTRev;
            fctRev = Prog_Ayarlar.Default.FCTRev;
            softwareVer = Prog_Ayarlar.Default.softwareVer;
            softwareRev = Prog_Ayarlar.Default.softwareRev;

            
            indexNo = indexArrange(Convert.ToString(Convert.ToInt32(Prog_Ayarlar.Default.indexNo) + 1));
            barcode50[1] = companyNo + sapNo + productDate + indexNo + productNo + cardType + gerberVer + bomVer + ictRev + fctRev + softwareVer + softwareRev;
            indexNo = indexArrange(Convert.ToString(Convert.ToInt32(Prog_Ayarlar.Default.indexNo) + 2));
            barcode50[2] = companyNo + sapNo + productDate + indexNo + productNo + cardType + gerberVer + bomVer + ictRev + fctRev + softwareVer + softwareRev;
            indexNo = indexArrange(Convert.ToString(Convert.ToInt32(Prog_Ayarlar.Default.indexNo) + 3));
            barcode50[3] = companyNo + sapNo + productDate + indexNo + productNo + cardType + gerberVer + bomVer + ictRev + fctRev + softwareVer + softwareRev;
            indexNo = indexArrange(Convert.ToString(Convert.ToInt32(Prog_Ayarlar.Default.indexNo) + 4));
            barcode50[4] = companyNo + sapNo + productDate + indexNo + productNo + cardType + gerberVer + bomVer + ictRev + fctRev + softwareVer + softwareRev;

            Prog_Ayarlar.Default.indexNo = indexNo;
            Prog_Ayarlar.Default.Save();
        }

        public string indexArrange(string index)
        {
            if(index.Length == 1)
            {
                index = "00000" + index;
            }
            else if (index.Length == 2)
            {
                index = "0000" + index;
            }
            else if (index.Length == 3)
            {
                index = "000" + index;
            }
            else if (index.Length == 4)
            {
                index = "00" + index;
            }
            else if (index.Length == 5)
            {
                index = "0" + index;
            }
            return index;
        }

        public int weekNum(DateTime tarih)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(tarih);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                tarih = tarih.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(tarih, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public void textCreate(int barcodeNum)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string nowYear = Convert.ToString(dt.Year);
                string nowMonth = Convert.ToString(dt.Month);
                string nowDay = Convert.ToString(dt.Day);
                string nowHour = Convert.ToString(dt.Hour);
                string nowMinute = Convert.ToString(dt.Minute);
                string nowSecond = Convert.ToString(dt.Second);
                string name = barcode50[barcodeNum] + "-" + nowYear + "-" + nowMonth + "-" + nowDay + "-" + nowHour + "-" + nowMinute + "-" + nowSecond;
                filePathTxt[barcodeNum] = logDosyaPath + "//" + name + ".txt"; //
                StreamWriter FileWrite = new StreamWriter(filePathTxt[barcodeNum]);
                FileWrite.Close();
            }
            catch (Exception ex)
            {
                ConsoleAppendLine("textCreate: " + ex.Message, Color.Red);
            }
        }

        /**************************************************** FCT *******************************************************************/
        private void waitTimer_Tick(object sender, EventArgs e)
        {
            waitTimer.Stop();
            waitTimer.Enabled = false;
            sonuclarıAl();
            //nextTimer.Start();
        }

        private void nextTimer_Tick(object sender, EventArgs e)
        {
            if (ModBusReadCoils(M101, 1)) //(TEST BİTTİ)
            {
                ModBusWriteSingleCoils(M101, false);     //Test Bitti
                nextTimer.Stop();
                nextTimer.Enabled = false;
                sonuclarıAl();
            }
        }

        private void sonuclarıAl()
        {
            int card1 = ModBusReadHoldingRegisters(D100, 1);
            int card2 = ModBusReadHoldingRegisters(D101, 1);
            int card3 = ModBusReadHoldingRegisters(D102, 1);
            int card4 = ModBusReadHoldingRegisters(D103, 1);
            int counter = 0;
            binary1 = Convert.ToString(card1, 2);
            binary2 = Convert.ToString(card2, 2);
            binary3 = Convert.ToString(card3, 2);
            binary4 = Convert.ToString(card4, 2);
            for (int i = binary1.Length; i < 8; i++)
            {
                binary1 = "0" + binary1;
            }
            for (int i = binary2.Length; i < 8; i++)
            {
                binary2 = "0" + binary2;
            }
            for (int i = binary3.Length; i < 8; i++)
            {
                binary3 = "0" + binary3;
            }
            for (int i = binary4.Length; i < 8; i++)
            {
                binary4 = "0" + binary4;
            }
            ConsoleAppendLine("1.Kart = " + binary1, Color.Green);
            ConsoleAppendLine("2.Kart = " + binary2, Color.Green);
            ConsoleAppendLine("3.Kart = " + binary3, Color.Green);
            ConsoleAppendLine("4.Kart = " + binary4, Color.Green);

            if (binary1.Substring(7, 1) == "1")
                cardResult[1] = true;
            else
                cardResult[1] = false;

            if (binary2.Substring(7, 1) == "1")
                cardResult[2] = true;
            else
                cardResult[2] = false;

            if (binary3.Substring(7, 1) == "1")
                cardResult[3] = true;
            else
                cardResult[3] = false;

            if (binary4.Substring(7, 1) == "1")
                cardResult[4] = true;
            else
                cardResult[4] = false;

            for (int i = 24; i > 0; i--)  //6-5-4-3-2-1
            {
                counter++;
                if (counter > 0 && counter <= 6)
                {
                    if (binary1.Substring(i - 18, 1) == "1")
                        arrayLEDTotal[counter] = true;
                    else
                        arrayLEDTotal[counter] = false;
                }
                else if (counter > 6 && counter <= 12)
                {
                    if (binary2.Substring(i - 12, 1) == "1")
                        arrayLEDTotal[counter] = true;
                    else
                        arrayLEDTotal[counter] = false;
                }
                else if (counter > 12 && counter <= 18)
                {
                    if (binary3.Substring(i - 6, 1) == "1")
                        arrayLEDTotal[counter] = true;
                    else
                        arrayLEDTotal[counter] = false;
                }
                else if (counter > 18 && counter <= 24)
                {
                    if (binary4.Substring(i, 1) == "1")
                        arrayLEDTotal[counter] = true;
                    else
                        arrayLEDTotal[counter] = false;
                }
            }
            /*
            cardResult[1] = ModBusReadCoils(M10, 1);
            arrayLEDTotal[1] = ModBusReadCoils(M11, 1);
            arrayLEDTotal[2] = ModBusReadCoils(M12, 1);
            arrayLEDTotal[3] = ModBusReadCoils(M13, 1);
            arrayLEDTotal[4] = ModBusReadCoils(M14, 1);
            arrayLEDTotal[5] = ModBusReadCoils(M15, 1);
            arrayLEDTotal[6] = ModBusReadCoils(M16, 1);
            cardResult[2] = ModBusReadCoils(M20, 1);
            arrayLEDTotal[7] = ModBusReadCoils(M21, 1);
            arrayLEDTotal[8] = ModBusReadCoils(M22, 1);
            arrayLEDTotal[9] = ModBusReadCoils(M23, 1);
            arrayLEDTotal[10] = ModBusReadCoils(M24, 1);
            arrayLEDTotal[11] = ModBusReadCoils(M25, 1);
            arrayLEDTotal[12] = ModBusReadCoils(M26, 1);
            cardResult[3] = ModBusReadCoils(M30, 1);
            arrayLEDTotal[13] = ModBusReadCoils(M31, 1);
            arrayLEDTotal[14] = ModBusReadCoils(M32, 1);
            arrayLEDTotal[15] = ModBusReadCoils(M33, 1);
            arrayLEDTotal[16] = ModBusReadCoils(M34, 1);
            arrayLEDTotal[17] = ModBusReadCoils(M35, 1);
            arrayLEDTotal[18] = ModBusReadCoils(M36, 1);
            cardResult[4] = ModBusReadCoils(M40, 1);
            arrayLEDTotal[19] = ModBusReadCoils(M41, 1);
            arrayLEDTotal[20] = ModBusReadCoils(M42, 1);
            arrayLEDTotal[21] = ModBusReadCoils(M43, 1);
            arrayLEDTotal[22] = ModBusReadCoils(M44, 1);
            arrayLEDTotal[23] = ModBusReadCoils(M45, 1);
            arrayLEDTotal[24] = ModBusReadCoils(M46, 1);
            */
            firstRubberValue = ModBusReadHoldingRegisters(D400, 1);
            secondRubberValue = ModBusReadHoldingRegisters(D405, 1);
            thirdRubberValue = ModBusReadHoldingRegisters(D410, 1);
            fourthRubberValue = ModBusReadHoldingRegisters(D415, 1);
            testResult();
        }

        private void testResult()
        {
            LED_Result();
            Rubber_Result();
            cardShowPicture();
            if (Prog_Ayarlar.Default.card1 == true)
            {
                if (cardResult[1] == true && arrayRubberResult[1] == true)
                {
                    CardPaint(Color.Green, card1);
                    FCT_Success(1);
                    ModBusWriteSingleCoils(M54, true);  //1.Piston
                }
                else
                {
                    CardPaint(Color.Red, card1);
                    FCT_Fail(1);
                }
                Thread.Sleep(750);
            }
            if (Prog_Ayarlar.Default.card2 == true)
            {
                if (cardResult[2] == true && arrayRubberResult[2] == true)
                {
                    CardPaint(Color.Green, card2);
                    FCT_Success(2);
                    ModBusWriteSingleCoils(M56, true);  //2.Piston
                }
                else
                {
                    CardPaint(Color.Red, card2);
                    FCT_Fail(2);
                }
                Thread.Sleep(750);
            }
            if (Prog_Ayarlar.Default.card3 == true)
            {
                if (cardResult[3] == true && arrayRubberResult[3] == true)
                {
                    CardPaint(Color.Green, card3);
                    FCT_Success(3);
                    ModBusWriteSingleCoils(M58, true);  //3.Piston
                }
                else
                {
                    CardPaint(Color.Red, card3);
                    FCT_Fail(3);
                }
                Thread.Sleep(750);
            }
            if (Prog_Ayarlar.Default.card4 == true)
            {
                if (cardResult[4] == true && arrayRubberResult[4] == true)
                {
                    CardPaint(Color.Green, card4);
                    FCT_Success(4);
                    ModBusWriteSingleCoils(M60, true);  //4.Piston
                }
                else
                {
                    CardPaint(Color.Red, card4);
                    FCT_Fail(4);
                }
                Thread.Sleep(750);
            }
            saniyeState = false;
            if (((cardResult[1] == true && arrayRubberResult[1] == true) || Prog_Ayarlar.Default.card1 == false) && ((cardResult[2] == true && arrayRubberResult[2] == true) || Prog_Ayarlar.Default.card2 == false) && ((cardResult[3] == true && arrayRubberResult[3] == true) || Prog_Ayarlar.Default.card3 == false) && ((cardResult[4] == true && arrayRubberResult[4] == true) || Prog_Ayarlar.Default.card4 == false))
            {
                CustomMessageBox.ShowMessage("Tüm kartlar Başarıyla Sonlandı. Lütfen Tekrar Başlayın!", customMessageBoxTitle, MessageBoxButtons.OK, CustomMessageBoxIcon.Information, Color.Green);
            }
            else
            {
                //CustomMessageBox.ShowMessage("FCT Testi Başarısız Sonlandı. Lütfen Tekrar Başlayın!", customMessageBoxTitle, MessageBoxButtons.OK, CustomMessageBoxIcon.Information, Color.Red);
                try { int num = (int)this.ErrorFrm.ShowDialog(); }
                catch (Exception) { }  
            }
            FCT_Finish();
        }

        private void LED_Result()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    string cardNumber = Convert.ToString(i * 6 + j);
                    if (arrayLEDTotal[i * 6 + j] == true)
                        logTut(i + 1, i + 1 + ".Kart " + j + ".LED Sonuç", ":Passed ", " Dönüş");
                    else
                        logTut(i + 1, i + 1 + ".Kart " + j + ".LED Sonuç", ":Failed ", " Dönüş");
                }
            }
        }

        private void Rubber_Result()
        {
            if (Prog_Ayarlar.Default.card1 == true)
            {
                if (firstRubberValue >= firstRubberMin && firstRubberValue <= firstRubberMax)
                {
                    arrayRubberResult[1] = true;
                    logTut(1, "1.Kart Rubber Sonuç ", ":Passed ", " Dönüş");
                }
                else
                {
                    arrayRubberResult[1] = false;
                    logTut(1, "1.Kart Rubber Sonuç ", ":Failed ", " Dönüş");
                }
            }

            if (Prog_Ayarlar.Default.card2 == true)
            {
                if (secondRubberValue >= secondRubberMin && secondRubberValue <= secondRubberMax)
                {
                    arrayRubberResult[2] = true;
                    logTut(2, "2.Kart Rubber Sonuç ", ":Passed ", " Dönüş");
                }
                else
                {
                    arrayRubberResult[2] = false;
                    logTut(2, "2.Kart Rubber Sonuç ", ":Failed ", " Dönüş");
                }
            }

            if (Prog_Ayarlar.Default.card3 == true)
            {
                if (thirdRubberValue >= thirdRubberMin && thirdRubberValue <= thirdRubberMax)
                {
                    arrayRubberResult[3] = true;
                    logTut(3, "3.Kart Rubber Sonuç ", ":Passed ", " Dönüş");
                }
                else
                {
                    arrayRubberResult[3] = false;
                    logTut(3, "3.Kart Rubber Sonuç ", ":Failed ", " Dönüş");
                }
            }

            if (Prog_Ayarlar.Default.card4 == true)
            {
                if (fourthRubberValue >= fourthRubberMin && fourthRubberValue <= fourthRubberMax)
                {
                    arrayRubberResult[4] = true;
                    logTut(4, "4.Kart Rubber Sonuç ", ":Passed ", " Dönüş");
                }
                else
                {
                    arrayRubberResult[4] = false;
                    logTut(4, "4.Kart Rubber Sonuç ", ":Failed ", " Dönüş");
                }
            }
        }

        private void cardShowPicture()  
        {
            if (Prog_Ayarlar.Default.card1 == true)
            {
                if (arrayLEDTotal[1] == true)
                    let1.BackColor = Color.Green;
                else
                    let1.BackColor = Color.Red;

                if (arrayLEDTotal[2] == true)
                    let2.BackColor = Color.Green;
                else
                    let2.BackColor = Color.Red;

                if (arrayLEDTotal[3] == true)
                    let3.BackColor = Color.Green;
                else
                    let3.BackColor = Color.Red;

                if (arrayLEDTotal[4] == true)
                    let4.BackColor = Color.Green;
                else
                    let4.BackColor = Color.Red;

                if (arrayLEDTotal[5] == true)
                    let5.BackColor = Color.Green;
                else
                    let5.BackColor = Color.Red;

                if (arrayLEDTotal[6] == true)
                    let6.BackColor = Color.Green;
                else
                    let6.BackColor = Color.Red;
            }
            if (Prog_Ayarlar.Default.card2 == true)
            {
                if (arrayLEDTotal[7] == true)
                    let7.BackColor = Color.Green;
                else
                    let7.BackColor = Color.Red;

                if (arrayLEDTotal[8] == true)
                    let8.BackColor = Color.Green;
                else
                    let8.BackColor = Color.Red;

                if (arrayLEDTotal[9] == true)
                    let9.BackColor = Color.Green;
                else
                    let9.BackColor = Color.Red;

                if (arrayLEDTotal[10] == true)
                    let10.BackColor = Color.Green;
                else
                    let10.BackColor = Color.Red;

                if (arrayLEDTotal[11] == true)
                    let11.BackColor = Color.Green;
                else
                    let11.BackColor = Color.Red;

                if (arrayLEDTotal[12] == true)
                    let12.BackColor = Color.Green;
                else
                    let12.BackColor = Color.Red;
            }
            if (Prog_Ayarlar.Default.card3 == true)
            {
                if (arrayLEDTotal[13] == true)
                    let13.BackColor = Color.Green;
                else
                    let13.BackColor = Color.Red;

                if (arrayLEDTotal[14] == true)
                    let14.BackColor = Color.Green;
                else
                    let14.BackColor = Color.Red;

                if (arrayLEDTotal[15] == true)
                    let15.BackColor = Color.Green;
                else
                    let15.BackColor = Color.Red;

                if (arrayLEDTotal[16] == true)
                    let16.BackColor = Color.Green;
                else
                    let16.BackColor = Color.Red;

                if (arrayLEDTotal[17] == true)
                    let17.BackColor = Color.Green;
                else
                    let17.BackColor = Color.Red;

                if (arrayLEDTotal[18] == true)
                    let18.BackColor = Color.Green;
                else
                    let18.BackColor = Color.Red;
            }
            if (Prog_Ayarlar.Default.card4 == true)
            {
                if (arrayLEDTotal[19] == true)
                    let19.BackColor = Color.Green;
                else
                    let19.BackColor = Color.Red;

                if (arrayLEDTotal[20] == true)
                    let20.BackColor = Color.Green;
                else
                    let20.BackColor = Color.Red;

                if (arrayLEDTotal[21] == true)
                    let21.BackColor = Color.Green;
                else
                    let21.BackColor = Color.Red;

                if (arrayLEDTotal[22] == true)
                    let22.BackColor = Color.Green;
                else
                    let22.BackColor = Color.Red;

                if (arrayLEDTotal[23] == true)
                    let23.BackColor = Color.Green;
                else
                    let23.BackColor = Color.Red;

                if (arrayLEDTotal[24] == true)
                    let24.BackColor = Color.Green;
                else
                    let24.BackColor = Color.Red;
            }
            if (Prog_Ayarlar.Default.card1 == true)
            {
                if (arrayRubberResult[1] == true)
                    btn1.BackColor = Color.Green;
                else
                    btn1.BackColor = Color.Red;
            }

            if (Prog_Ayarlar.Default.card2 == true)
            {
                if (arrayRubberResult[2] == true)
                    btn2.BackColor = Color.Green;
                else
                    btn2.BackColor = Color.Red;
            }

            if (Prog_Ayarlar.Default.card3 == true)
            {
                if (arrayRubberResult[3] == true)
                    btn3.BackColor = Color.Green;
                else
                    btn3.BackColor = Color.Red;
            }

            if (Prog_Ayarlar.Default.card4 == true)
            {
                if (arrayRubberResult[4] == true)
                    btn4.BackColor = Color.Green;
                else
                    btn4.BackColor = Color.Red;
            }
        }

        private void FCT_Success(int cardNo)
        {
            ConsoleAppendLine(cardNo + ".Kart Başarılı", Color.Green);
            if(urunlerInsert(barcode50[cardNo], "7"))
            {
                printAction(barcode50[cardNo], cardNo);
                //printerFunction(barcode50[cardNo], cardNo);  
            }
        }

        public void FCT_Fail(int cardNo)
        {
            ConsoleAppendLine(cardNo + ".Kart Başarısız", Color.Red);
            //urunlerInsert(barcode50[cardNo], "5");   //Teste Girdim
            errorCardTxt.Text = Convert.ToString(++errorCard);
        }

        /**************************************************** FCT-RESULT *******************************************************************/
        public void FCT_Finish()
        {
            Register_Clear();
            FCT_Clear();
            Verim();
        }

        private void Register_Clear()
        {
            //ModBusWriteSingleCoils(M0, false);       //Güvenlik Biti
            ModBusWriteSingleCoils(M100, false);     //Test Başladı
                                                     //ModBusWriteSingleCoils(M101, false);     //Test Bitti

            // Ölçüm Değerleri
            /*
            ModBusWriteSingleCoils(M10, false);
            ModBusWriteSingleCoils(M11, false);
            ModBusWriteSingleCoils(M12, false);
            ModBusWriteSingleCoils(M13, false);
            ModBusWriteSingleCoils(M14, false);
            ModBusWriteSingleCoils(M15, false);
            ModBusWriteSingleCoils(M16, false);

            ModBusWriteSingleCoils(M20, false);
            ModBusWriteSingleCoils(M21, false);
            ModBusWriteSingleCoils(M22, false);
            ModBusWriteSingleCoils(M23, false);
            ModBusWriteSingleCoils(M24, false);
            ModBusWriteSingleCoils(M25, false);
            ModBusWriteSingleCoils(M26, false);

            ModBusWriteSingleCoils(M30, false);
            ModBusWriteSingleCoils(M31, false);
            ModBusWriteSingleCoils(M32, false);
            ModBusWriteSingleCoils(M33, false);
            ModBusWriteSingleCoils(M34, false);
            ModBusWriteSingleCoils(M35, false);
            ModBusWriteSingleCoils(M36, false);

            ModBusWriteSingleCoils(M40, false);
            ModBusWriteSingleCoils(M41, false);
            ModBusWriteSingleCoils(M42, false);
            ModBusWriteSingleCoils(M43, false);
            ModBusWriteSingleCoils(M44, false);
            ModBusWriteSingleCoils(M45, false);
            ModBusWriteSingleCoils(M46, false);
            */
            modbusClientPLC.WriteSingleRegister(D100, 0);
            modbusClientPLC.WriteSingleRegister(D101, 0);
            modbusClientPLC.WriteSingleRegister(D102, 0);
            modbusClientPLC.WriteSingleRegister(D103, 0);

            ModBusWriteSingleCoils(M54, true);    //1.Piston  //M54-M56-M58-M60
            ModBusWriteSingleCoils(M56, true);    //2.Piston
            ModBusWriteSingleCoils(M58, true);    //3.Piston
            ModBusWriteSingleCoils(M60, true);    //4.Piston

            ModBusWriteSingleCoils(M1, false);     //1.LEDLER  //M1-M3-M5-M7-M50-M52
            ModBusWriteSingleCoils(M3, false);     //2.LEDLER
            ModBusWriteSingleCoils(M5, false);     //3.LEDLER
            ModBusWriteSingleCoils(M7, false);     //4.LEDLER
            ModBusWriteSingleCoils(M50, false);    //5.LEDLER
            ModBusWriteSingleCoils(M52, false);    //6.LEDLER
        }

        private void FCT_Clear()
        {
            timersClear();
            variablesClear();
            componentsClear();
        }

        private void Verim()
        {
            totalCard = totalCard + 4;
            totalCardTxt.Text = Convert.ToString(totalCard);
            verimTxt.Text = Convert.ToString(100 - ((float)((float)errorCard / totalCard)) * 100);
        }

        private void timersClear()
        {
            timerAdmin.Stop();
            timerAdmin.Enabled = false;
            timerEmergencyStop.Stop();
            timerEmergencyStop.Enabled = false;
            nextTimer.Stop();
            nextTimer.Enabled = false;
            lblTimer2.BackColor = Color.Transparent;
            lblTimer2.Text = "OFF";
            timerInit.Start();
            lblTimer1.BackColor = Color.Green;
            lblTimer1.Text = "ON";
        }

        private void variablesClear()
        {
            fctState = false;
            saniyeState = false;
            adminTimerCounter = 0;
            saniyeTimerCounter = 0;
            fctSaniye = 0;
            yetki = 0;
            for (int i = 1; i <= FCT_CARD_NUMBER; i++)
            {
                filePathTxt[i] = "";
                barcode50[i] = "";
                cardStokNum[i] = "";
                cardResult[i] = true;
                arrayRubberResult[i] = true;
            }

            for (int i = 1; i <= 30; i++)
            {
                arrayLEDTotal[i] = true;
            }
        }

        private void componentsClear()
        {
            btnFCTInit.BackColor = Color.Yellow;
            btnFCTInit.Text = "TESTİ YENİDEN BAŞLATABİLİRSİNİZ.";
            progressBarFCT.Value = 0;
            CardPaint(Color.Gray, card1);
            CardPaint(Color.Gray, card2);
            CardPaint(Color.Gray, card3);
            CardPaint(Color.Gray, card4);
            let11.BackColor = Color.Transparent;
            let7.BackColor = Color.Transparent;
            let8.BackColor = Color.Transparent;
            let9.BackColor = Color.Transparent;
            let10.BackColor = Color.Transparent;
            let12.BackColor = Color.Transparent;
            let5.BackColor = Color.Transparent;
            let1.BackColor = Color.Transparent;
            let2.BackColor = Color.Transparent;
            let3.BackColor = Color.Transparent;
            let4.BackColor = Color.Transparent;
            let6.BackColor = Color.Transparent;
            let17.BackColor = Color.Transparent;
            let13.BackColor = Color.Transparent;
            let14.BackColor = Color.Transparent;
            let15.BackColor = Color.Transparent;
            let16.BackColor = Color.Transparent;
            let18.BackColor = Color.Transparent;
            let23.BackColor = Color.Transparent;
            let19.BackColor = Color.Transparent;
            let20.BackColor = Color.Transparent;
            let21.BackColor = Color.Transparent;
            let22.BackColor = Color.Transparent;
            let24.BackColor = Color.Transparent;
            btn1.BackColor = Color.Transparent;
            btn2.BackColor = Color.Transparent;
            btn3.BackColor = Color.Transparent;
            btn4.BackColor = Color.Transparent;
        }

        /**************************************************** PRINTER *******************************************************************/
        private Thread printerTh = null;
        private bool printAction(object data, int cardNumber)  //PRINTER AKSİYON
        {
            printerTh = new Thread(()=>printerFunction(data, cardNumber));
            printerTh.Start();
            return true;
        }
        private void printerFunction(object data, int cardNumber)  //PRINTER AKSİYON
        {
            try
            {
                string fullproductCode = (string)data;
                string company_no = string.Empty;
                string sap_no = string.Empty;
                string product_date = string.Empty;
                string index_no = string.Empty;
                string product_no = string.Empty;
                string card_type = string.Empty;
                string gerber_ver = string.Empty;
                string bom_ver = string.Empty;
                string ict_rev = string.Empty;
                string fct_rev = string.Empty;
                string software_ver = string.Empty;
                string software_rev = string.Empty;

                company_no = fullproductCode.Substring(0, 2);
                sap_no = fullproductCode.Substring(2, 10);
                product_date = fullproductCode.Substring(12, 4);
                index_no = fullproductCode.Substring(16, 6);
                product_no = fullproductCode.Substring(22, 14);
                card_type = fullproductCode.Substring(36, 2);
                gerber_ver = fullproductCode.Substring(38, 2);
                bom_ver = fullproductCode.Substring(40, 2);
                ict_rev = fullproductCode.Substring(42, 2);
                fct_rev = fullproductCode.Substring(44, 2);
                software_ver = fullproductCode.Substring(46, 2);
                software_rev = fullproductCode.Substring(48, 2);

                string test = "";
                string start = "^XA" + "^LH" + Ayarlar.Default.printerPos;
                string qr = "^BQN,2,2" + "^FDQA," + fullproductCode + "^FS";
                string s1 = company_no + index_no.Substring(0, 2);
                string s2 = index_no.Substring(2, 4);
                string s3 = product_no.Substring(0, 4);
                string s4 = product_no.Substring(4, 4);
                string s5 = product_no.Substring(8, 4);
                string s6 = product_no.Substring(12, 2) + card_type;
            
                string veri1 = "^FO60,10" + "^A0,15,15" + "^FD" + "P/N: " + sap_no + "^FS";   //İlki Pozisyon //İkincisi Boy-En
                string veri2 = "^FO60,35" + "^A0,15,15" + "^FD" + "S/N: " + s1 + "-" + s2 + "-" + s3 + "^FS";   //İlki Pozisyon //İkincisi Boy-En
                string veri3 = "^FO60,60" + "^A0,15,15" + "^FD" + "       " + s4 + "-" + s5 + "-" + s6 + "^FS";   //İlki Pozisyon //İkincisi Boy-En
                string veri4 = "^FO60,85" + "^A0,15,15" + "^FD" + "VER: " + software_ver + "." + software_rev + " G:" + gerber_ver + " B:" + bom_ver + " T:" + product_date + "^FS";   //İlki Pozisyon //İkincisi Boy-En
                string veri5 = "^FO110,110" + "^A0,30,30" + "^FD" + Convert.ToString(cardNumber) + "^FS";   //İlki Pozisyon //İkincisi Boy-En
                string end = "^XZ";

                test = start + qr + veri1 + veri2 + veri3 + veri4 + veri5 + end;

                //Get local print server
                var server = new LocalPrintServer();

                //Load queue for correct printer
                PrintQueue pq = server.GetPrintQueue(printerName, new string[0] { });
                PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();
                foreach (PrintSystemJobInfo job in jobs)
                {
                    job.Cancel();
                }

                if (!RawPrinterHelper.SendStringToPrinter(printerName, test))
                {
                    ConsoleAppendLine("Printer Error1: ", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ConsoleAppendLine("Printer Error2: " + ex.Message, Color.Red);
            }
        }

        /**************************************************** LOG *******************************************************************/
        private void logTut(int cardId, string testName, string testResult, string testState)
        {
            try
            {
                if (logDosyaPath != "")
                {
                    List<string> lines = new List<string>();
                    lines = File.ReadAllLines(filePathTxt[cardId]).ToList();
                    lines.Add(testName + testResult + testState);
                    ConsoleAppendLine(testName + testResult + testState + "Eklendi", Color.Green);
                    File.WriteAllLines(filePathTxt[cardId], lines);
                }
                else
                {
                    ConsoleAppendLine("Dosya Yolu Boş Kalamaz", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ConsoleAppendLine("sqlTextYaz: " + ex.Message, Color.Red);
            }
        }

        /**************************************************** CONSOLE TEXT *******************************************************************/
        private void rtbConsole_TextChanged(object sender, EventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            rtb.SelectionStart = rtb.Text.Length;
            rtb.ScrollToCaret();
        }

        /*Kullanıcı Arayüzüne Yazı Yazılır*/
        public void ConsoleAppendLine(string text, Color color)
        {
            if (rtbConsole.InvokeRequired)
            {
                rtbConsole.Invoke(new Action(delegate ()
                {
                    rtbConsole.Select(rtbConsole.TextLength, 0);
                    rtbConsole.SelectionColor = color;
                    rtbConsole.AppendText(text + Environment.NewLine);
                    rtbConsole.Select(rtbConsole.TextLength, 0);
                    rtbConsole.SelectionColor = Color.White;
                }));
            }
            else
            {
                rtbConsole.Select(rtbConsole.TextLength, 0);
                rtbConsole.SelectionColor = color;
                rtbConsole.AppendText(text + Environment.NewLine);
                rtbConsole.Select(rtbConsole.TextLength, 0);
                rtbConsole.SelectionColor = Color.White;
            }
        }

        /*Kullanıcı Arayüzünde Bir Satır Boşluk Bırakılır*/
        public void ConsoleNewLine()
        {
            if (rtbConsole.InvokeRequired)
            {
                rtbConsole.Invoke(new Action(delegate ()
                {
                    rtbConsole.AppendText(Environment.NewLine);
                }));
            }
            else
            {
                rtbConsole.AppendText(Environment.NewLine);
            }
        }

        public void ConsoleClean()
        {
            if (rtbConsole.InvokeRequired)
            {
                rtbConsole.Invoke(new Action(delegate ()
                {
                    rtbConsole.Text = "";
                    rtbConsole.Select(rtbConsole.TextLength, 0);
                    rtbConsole.SelectionColor = Color.White;
                }));
            }
            else
            {
                rtbConsole.Text = "";
                rtbConsole.Select(rtbConsole.TextLength, 0);
                rtbConsole.SelectionColor = Color.White;
            }
        }

        public void CardPaint (Color color1,params TableLayoutPanel[] tableLayouts)
        {
            for (int i = 0; i < tableLayouts.Length; i++)
            {
                tableLayouts[i].BackColor = color1;
            }
           
        }

        /**************************************************** PAGE CHANGE *******************************************************************/
        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAyar_Click(object sender, EventArgs e)
        {
            int num = (int)this.AyarFrm.ShowDialog();
        }

        private void btnProgAyar_Click(object sender, EventArgs e)
        {
            int num = (int)this.ProgAyarFrm.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)    //YENİ
        {
            if (keyData != Keys.L)
                return false;
            if (this.yetki != 0)
            {
                timerAdmin.Stop();
                this.yetki = 0;
                this.yetkidegistir();
            }
            else
            {
                try { int num = (int)this.SifreFrm.ShowDialog(); }
                catch (Exception) { }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void yetkidegistir()
        {
            if (this.yetki == 0)  //Kapanan
            {
                this.btnCikis.Enabled = false;
                this.btnAyar.Enabled = false;
                this.btnProgAyar.Enabled = false;

                this.btnCikis.BackColor = Color.Beige;
                this.btnAyar.BackColor = Color.Beige;
                this.btnProgAyar.BackColor = Color.Beige;
            }
            if (this.yetki == 1)  //Admin Tüm Ayarlar
            {
                this.btnCikis.Enabled = true;
                this.btnAyar.Enabled = true;
                this.btnProgAyar.Enabled = true;

                ProgAyarFrm.operatorSettings.Enabled = true;
                ProgAyarFrm.iniSettings.Enabled = true;
                ProgAyarFrm.rubberSettings.Enabled = true;
                ProgAyarFrm.barkodSettings.Enabled = true;
                ProgAyarFrm.cardSettings.Enabled = true;

                this.btnCikis.BackColor = Color.Red;
                this.btnAyar.BackColor = Color.Red;
                this.btnProgAyar.BackColor = Color.Red;
                timerAdmin.Start();
            }
            if (this.yetki == 2)  //Operatör Kısıtlı Ayarlar
            {
                this.btnCikis.Enabled = true;
                this.btnProgAyar.Enabled = true;

                ProgAyarFrm.operatorSettings.Enabled = true;
                ProgAyarFrm.iniSettings.Enabled = true;
                ProgAyarFrm.rubberSettings.Enabled = false;
                ProgAyarFrm.barkodSettings.Enabled = false;
                ProgAyarFrm.cardSettings.Enabled = false;

                this.btnCikis.BackColor = Color.Red;
                this.btnAyar.BackColor = Color.Beige;
                this.btnProgAyar.BackColor = Color.Red;
                timerAdmin.Start();
            }
            if (this.yetki == 3)  //Admin Piston Yukarı
            {
                timerAdmin.Start();
            }
        }

        /**************************************************** TIMER *******************************************************************/
        private void timerAdmin_Tick_1(object sender, EventArgs e)
        {
            adminTimerCounter++;
            if (adminTimerCounter == 1)
            {
                adminTimerCounter = 0;
                timerAdmin.Stop();
                this.yetki = 0;
                this.yetkidegistir();
            }
        }

        private void saniyeTimer_Tick(object sender, EventArgs e)
        {
            saniyeTimerCounter++;
            if (saniyeTimerCounter == 1)
            {
                saniyeTimerCounter = 0;
                fctTimerTxt.Text = Convert.ToString(++fctSaniye);
            }
        }

        bool saniyeState = false;
        int second = 0;
        int oldSecond = 0;
        private void saniyeThreadFunc()
        {
            for (; ; )
            {
                if (saniyeState)
                {
                    DateTime dt = DateTime.Now;
                    second = dt.Second;
                    if (second != oldSecond)
                    {
                        oldSecond = second;
                    fctTimerTxt.Invoke(new Action(delegate ()
                    {
                        fctTimerTxt.Text = Convert.ToString(++fctSaniye);
                    }));
                }
                    Thread.Sleep(1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModBusWriteSingleCoils(M0, true);  //1.Güvenlik Biti
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register_Clear();
        }


    }
}

