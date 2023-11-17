using System.Globalization;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;
using System.IO;

namespace Add_programs_to_the_start_menu {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void kısayolOluştur(string dosyaAdi, string dosyaYolu, string dosyaIconuYolu = null) {
            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string appStartMenuPath = Path.Combine(commonStartMenuPath);

            if (!Directory.Exists(appStartMenuPath)) Directory.CreateDirectory(appStartMenuPath);

            string shortcutLocation = Path.Combine(appStartMenuPath, dosyaAdi + ".lnk");
            WshShell shell = new();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = dosyaAdi;
            if (dosyaIconuYolu != null) shortcut.IconLocation = dosyaIconuYolu;
            shortcut.TargetPath = dosyaYolu;
            shortcut.Save();
            MessageBox.Show("Kısayol Başarıyla Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e) {
            string[] args = Environment.GetCommandLineArgs();
            if (args != null) {
                foreach (string arg in args) {
                    if (arg == "-k" | arg == "--konsol") {
                        AllocConsole();
                        Console.WriteLine("Привет сука блять");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e) {
            if (System.IO.File.Exists(textBox1.Text)) {
                if (textBox3.Text == "") {
                    if (textBox2.Text != "") {
                        kısayolOluştur($"{textBox2.Text}", textBox1.Text);
                    }
                    else {
                        MessageBox.Show("Hata: Dosya Adı Belirtmediniz.", "HATA!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else {
                    if (System.IO.File.Exists(textBox3.Text)) {
                        if (textBox2.Text != "") {
                            kısayolOluştur($"{textBox2.Text}", textBox1.Text, textBox3.Text);
                        }
                        else {
                            MessageBox.Show("Hata: Dosya Adı Belirtmediniz.", "HATA!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else {
                        MessageBox.Show("Hata: İkon Dosyası Bulunamadı.", "HATA!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else {
                MessageBox.Show("Hata: Dosya Bulunamadı.", "HATA!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (openFileDialog2.ShowDialog() == DialogResult.OK) textBox3.Text = openFileDialog2.FileName;
        }
    }
}