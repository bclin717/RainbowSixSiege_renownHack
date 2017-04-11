using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Runtime.ExceptionServices;

namespace R6Shack {
    public partial class Form1 : Form {
        static private string NameOfGame = "RainbowSixGame";
        static private long baseAddressOfScoresInGame = 0x04064140;
        static private long[] offsets = { 0x170, 0x280, 0x0, 0xd8, 0xb8 };

        private long tmpAdd;
        private bool addIsDoneflag = false;

        private static Form1 form ;

        private Thread t1;
        private Thread calAdd;


        public Form1() {
            Closing += new CancelEventHandler(Form1_Closing);
            Form1.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            form = this;
            stopBEService();
            ProcessModule module = FindModule(NameOfGame + ".exe");
            calAdd = new Thread(getTheAddScore);
            calAdd.Start();
        }

        public static void stopBEService() {
            try {
                string m_ServiceName = "BEService";
                ServiceController service = new ServiceController(m_ServiceName);
                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 60);

                if (service.Status != ServiceControllerStatus.Stopped &&
                    service.Status != ServiceControllerStatus.StopPending) {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                form.label_BEService.Text = "BattleEye已停止";
            } catch (Exception ex) {
                form.label_BEService.Text = "停止BattleEye失敗";
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void getTheAddScore() {
            ProcessModule module = FindModule(NameOfGame + ".exe");
            long baseAddressOfGame = module.BaseAddress.ToInt64();
            while (true) {
                try {
                    tmpAdd = baseAddressOfGame + baseAddressOfScoresInGame;
                    foreach (long offset in offsets) {
                        tmpAdd = tmpAdd = Helper.ReadMemoryValue(tmpAdd, NameOfGame);
                        tmpAdd = tmpAdd + offset;
                    }
                    if (Helper.ReadMemoryValue(tmpAdd, NameOfGame) == 0) {
                        addIsDoneflag = true;
                        break;
                    }
                } catch (AccessViolationException) {
                    Thread.Sleep(500);
                    continue;
                }     
            }
        }

        public static ProcessModule FindModule(string name) {
            Process[] process = Process.GetProcessesByName(NameOfGame);
            while(process.Length <= 0) {
                process = Process.GetProcessesByName(NameOfGame);
                Thread.Sleep(1000);
            }
            form.label_ifGameRunning.Text = "已偵測到遊戲";

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            foreach (ProcessModule module in process[0].Modules) {
                if (module.ModuleName.ToLower() == name.ToLower())
                    return module;
            }
            return null;
        }

        private void checkBox_lockScroe_CheckedChanged(object sender, EventArgs e) {
            if(checkBox_lockScroe.Checked) {
                t1 = new Thread(editScore);
                t1.Start();
            } else if(!checkBox_lockScroe.Checked) {
                t1.Abort();
            }
        }

        private void editScore() {
            while (true) {
                if(addIsDoneflag == true) {
                    Helper.WriteMemoryValue(tmpAdd - 28, "RainbowSixGame", 20000);
                    Helper.WriteMemoryValue(tmpAdd, "RainbowSixGame", 20000);
                    Thread.Sleep(500);
                }
            }
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DialogResult dr = MessageBox.Show(this, "確定退出？", "刷好刷滿", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) {
                try {
                    t1.Abort();
                    System.Environment.Exit(System.Environment.ExitCode);
                } catch(Exception ex) {
                    System.Environment.Exit(System.Environment.ExitCode);
                }
            } else {
                e.Cancel = true;
            }
        }
    }
}
