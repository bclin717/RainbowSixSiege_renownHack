using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace R6Shack {
    public partial class Form1 : Form {
        static private string NameOfGame = "RainbowSixGame";
        static private long baseAddressOfScoresInGame = 0x04064140;
        static private long[] offsets = { 0x170, 0x280, 0x0, 0xd8, 0xb8 };

        private long tmpAdd;
        private long nowScores;

        Thread t1;

        private static Form1 form ;

        public Form1() {
            Form1.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            form = this;
            stopBEService();
            ProcessModule module = FindModule(NameOfGame + ".exe");
            getTheAddScore();
            t1 = new Thread(editScore); 
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

        private void getTheAddScore() {
            ProcessModule module = FindModule(NameOfGame + ".exe");
            long baseAddressOfGame = module.BaseAddress.ToInt64();
            tmpAdd = baseAddressOfGame + baseAddressOfScoresInGame;
            foreach (long offset in offsets) {
                tmpAdd = tmpAdd = Helper.ReadMemoryValue(tmpAdd, NameOfGame);
                tmpAdd = tmpAdd + offset;
            }
            //nowScores = Helper.ReadMemoryValue(tmpAdd, "RainbowSixGame");
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
                Helper.WriteMemoryValue(tmpAdd - 28, "RainbowSixGame", 20000);
                Helper.WriteMemoryValue(tmpAdd, "RainbowSixGame", 20000);
                Thread.Sleep(500);
            }
        }
    }
}
