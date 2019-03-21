using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Text.RegularExpressions;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using static Chrome_Cache_Extract.DataProcessing;

namespace Chrome_Cache_Extract
{
    public partial class MainWindow : Form
    {
        DataProcessing dataProcessor = new DataProcessing();

        public MainWindow()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void button_select_folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            DialogResult a = browserDialog.ShowDialog();
            if (a == DialogResult.OK)
            {
                dataProcessor.SetUserPATH(browserDialog.SelectedPath);
                txtBox_savePath.Text = browserDialog.SelectedPath;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            StreamReader local = File.OpenText("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Google\\Chrome\\User Data\\Local State");
            string file = local.ReadToEnd();
            int beg = Regex.Match(file, "\"info_cache\":{").Index + 14;
            int end = Regex.Match(file, "},\"last_active_profiles\"").Index;
            file = file.Substring(beg, end - beg);
            MatchCollection users = Regex.Matches(file, "(\"[0-z]{1,}\":{)|(\"[0-z]{1,} [0-z]{1,}\":{)");
            for (int i = 0; i < users.Count; i++)
            {
                clb_kullaniciListesi.Items.Add(users[i].Value.Split('"')[1]);
            }
        }
        //######################## These two are related ##########################################
        private void Calistir(object sender, DoWorkEventArgs e)
        {
            foreach (string user in clb_kullaniciListesi.CheckedItems)
            {
                progressBar.Value = 0;
                List<string> hexes = Directory.GetFiles(dataProcessor.PATH1(user), "*f_*").ToList();
                List<string> addr = new List<string>(hexes.Count);
                //Dosya isminden hex değerlerini alma
                for (int i = 0; i < hexes.Count; i++)
                    hexes[i] = hexes[i].Split('_')[1];
                progressBar.Value += 10;

                //Alınan hex degerlerini little endian icin 32 bite donusturme
                string ToBinary(string hexString, int j)
                {
                    return Convert.ToString(Convert.ToInt32(hexString[j].ToString(), 16), 2).PadLeft(4, '0');
                }
                string ToHex(string b)
                {
                    return Convert.ToInt32(b, 2).ToString("x");
                }
                foreach (string hex in hexes)
                {
                    string b = "10000000";
                    for (int j = 0; j < 6; j++)
                        b += ToBinary(hex, j);
                    addr.Add(dataProcessor.LittleEndian(ToHex(b)));
                }
                progressBar.Value += 10;

                //Data_1 dosyasında adresleri bulup dosya adı, url ve url sıralaması için gereken hex uzantılarını alma
                List<partt> parts = dataProcessor.ExportURLs(addr, hexes, user);
                progressBar.Value += 10;

                // "parts" listesindeki partların URL'lerini gruplandırmak için aynı olanları teke dusurup "URLs" listesine aktarma
                List<string> URLs = new List<string>();
                for (int i = 0; i < parts.Count; i++)
                {
                    for (int p = 0; p < parts.Count; p++)
                    {
                        if (!(parts[p].url == parts[i].url) & !URLs.Contains(parts[p].url))
                        // "parts" daki her URL'i diger butun URL'ler ile karsilastir ve URLs icinde yoksa ekle
                        {
                            //listBox2.Items.Add(u[p].url);
                            URLs.Add(parts[p].url);
                        }
                        //else clean.Add(u[p].url);
                    }
                }

                Dictionary<string, ArrayList> URL_Dict = new Dictionary<string, ArrayList>();
                Dictionary<string, List<int[,]>> HexDict = new Dictionary<string, List<int[,]>>();

                foreach (string url in URLs)
                {
                    URL_Dict[url] = new ArrayList();
                }

                progressBar.Value += 10;
                foreach (KeyValuePair<string, ArrayList> item in URL_Dict)
                {
                    
                    string URL = item.Key;

                    for (int i = 0; i < parts.Count; i++)
                    {
                        if (URL == parts[i].url)
                        {
                            URL_Dict[URL].Add(i);
                        }
                    }

                    HexDict[URL] = new List<int[,]>();
                    foreach (int partFileIndex in URL_Dict[URL])
                    {
                        if (!(URL_Dict[URL].Count == 0))
                        {
                            try
                            {
                                HexDict[URL].Add(new int[,] { { partFileIndex, Convert.ToInt32(parts[partFileIndex].binary_data, 16) } });
                            }
                            catch { continue; }
                        }
                    }
                    for (int i = 0; i < HexDict[URL].Count; i++)
                        for (int p = 0; p < HexDict[URL].Count; p++)
                        {
                            int[,] first = HexDict[URL][i];
                            int[,] second = HexDict[URL][p];
                            if (second[0, 1] > first[0, 1])
                            {
                                int tmp = second[0, 1];
                                int tmp2 = second[0, 0];
                                second[0, 1] = first[0, 1];
                                second[0, 0] = first[0, 0];
                                first[0, 1] = tmp;
                                first[0, 0] = tmp2;
                            }
                        }

                    string MediaName = URL.Substring(Regex.Match(URL, "/", RegexOptions.RightToLeft).Index + 1, Regex.Match(URL, "\\.mp4|php|mp3|webm|m4a").Index - Regex.Match(URL, "/", RegexOptions.RightToLeft).Index - 1);
                    //if (!(item.Value.Count == 1)) listBox2.Items.Add(MediaName);
                    if (!File.Exists(dataProcessor.UserPATH(user) + "\\Media Cache\\" + MediaName + ".mp4"))
                    {
                        FileStream writer = new FileStream(dataProcessor.UserPATH(user) + "\\Media Cache\\" + MediaName + ".mp4", FileMode.Append);
                        foreach (int[,] i in HexDict[URL])
                        {
                            if (!(item.Value.Count == 1))
                            {
                                //listBox2.Items.Add(parts[i[0, 0]].file + " " + parts[i[0, 0]].binary_data);

                                File.Copy(dataProcessor.PATH1(user) + "\\f_" + parts[i[0, 0]].file, dataProcessor.UserPATH(user) + "\\tmp", true);
                                byte[] file = File.ReadAllBytes(dataProcessor.UserPATH(user) + "\\tmp");
                                writer.Write(file, 0, file.Length);

                            }
                        }
                        writer.Close();
                    }
                    else if (!File.Exists(dataProcessor.UserPATH(user) + "\\Media Cache\\" + MediaName + "(2).mp4"))
                    {
                        FileStream writer = new FileStream(dataProcessor.UserPATH(user) + "\\Media Cache\\" + MediaName + "(2).mp4", FileMode.Append);
                        foreach (int[,] i in HexDict[URL])
                        {
                            if (!(item.Value.Count == 1))
                            {

                                File.Copy(dataProcessor.PATH1(user) + "\\f_" + parts[i[0, 0]].file, dataProcessor.UserPATH(user) + "\\tmp", true);
                                byte[] file = File.ReadAllBytes(dataProcessor.UserPATH(user) + "\\tmp");
                                writer.Write(file, 0, file.Length);

                            }
                        }
                        writer.Close();

                    }
                    else
                    {
                        FileStream writer = new FileStream(dataProcessor.UserPATH(user) + "\\Media Cache\\" + MediaName + ".mp4", FileMode.Append);
                        foreach (int[,] i in HexDict[URL])
                        {
                            if (!(item.Value.Count == 1))
                            {
                                //listBox2.Items.Add(parts[i[0, 0]].file + " " + parts[i[0, 0]].binary_data);

                                File.Copy(dataProcessor.PATH1(user) + "\\f_" + parts[i[0, 0]].file, dataProcessor.UserPATH(user) + "\\tmp", true);
                                byte[] file = File.ReadAllBytes(dataProcessor.UserPATH(user) + "\\tmp");
                                writer.Write(file, 0, file.Length);

                            }
                        }
                        writer.Close();
                    }

                    File.Delete(dataProcessor.UserPATH(user) + "\\tmp");
                    progressBar.Value += 60 / URL_Dict.Count;
                }

                File.Delete(dataProcessor.UserPATH(user) + "\\data_1");
            }
            progressBar.Value = 100;
            clb_kullaniciListesi.Enabled = true;
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                progressBar.Value = 0;
                clb_kullaniciListesi.Enabled = false;
                if (txtBox_savePath.Text == string.Empty | clb_kullaniciListesi.CheckedItems.Count == 0)
                    MessageBox.Show("İlk önce kayıt yerini seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else if (!checkBox_multiMedia.Checked & !checkBox_normal.Checked)
                    MessageBox.Show("Çıkarılacak çerez türünü/türlerini seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    progressBar.Visible = true;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else MessageBox.Show("Şu an bir işlem sürüyor.");
        }
        //#########################################################################################       
    }
}
