using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

using Gma.System.MouseKeyHook;
using WindowsInput;
using WindowsInput.Native;

namespace ProgInz_MajasDarbs2
{
    public partial class frmMain : Form
    {
		//DP princips Nr.1 - Mainīgajiem/metodēm izmantot nosaukumus, kas nepārprotami raksturo to būtību. Izmantots visā programmā.
        //DP princips Nr.2 - Komentāru rakstīšana ir viens no principiem. Tas palīdz aprakstīt koda gabala būtību, izvairoties no liekiem komentāriem, piem., lai nekomentētu katru metodes rindu.
        //DP princips Nr.3 - Izmantot [TAB] atstarpēm. Nerakstīt visu vienā stabiņā, lai skaidri būtu pārredzams, kas, piem., atrodas ciklā un kas nē.
	
        // Lietotāja ievades simulācijai
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        InputSimulator _InputSimulator;
        IKeyboardMouseEvents MouseKey_Hook;

        // Peles klikšķu kodi (priekš Windows OS)
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        // Izsekojam ierkasta/atkārtošanas stāvoklim/progresam
        int _CurrentRowIndex, _LoopCount;
        UInt64 _PlaybackTime, _RecordTime;
        bool _IsRecording, _StoppedWithKey;

        // Papildus pavedieni fona procesiem - nodrošina klikšķu izpildi un lietotāja saskarnes vienlaicīgu darbību.
        static BackgroundWorker _bWorker;
        System.Timers.Timer tmrPlay;

        public frmMain()
        {
            InitializeComponent();
            _InputSimulator = new InputSimulator();
        }

        private void SubscribeHooks()
        {
            MouseKey_Hook = Hook.GlobalEvents();

            MouseKey_Hook.KeyDown += MouseKey_Hook_KeyDown;
            MouseKey_Hook.MouseClick += MouseKey_Hook_MouseClick;
        }

        private void UnsubscribeHooks()
        {
            if (MouseKey_Hook == null) return;
            MouseKey_Hook.KeyDown -= MouseKey_Hook_KeyDown;
            MouseKey_Hook.MouseClick -= MouseKey_Hook_MouseClick;

            MouseKey_Hook.Dispose();
        }

        private void btnStartRecord_Click(object sender, EventArgs e)
        {
            SubscribeHooks();

            RepeatCountControl.Value = 1;
            dgvActions.Rows.Clear();

            _RecordTime = 0;
            _IsRecording = true;

            btnStartRecord.Enabled = false;
            btnStopRecord.Enabled = true;
            btnPlayRecord.Enabled = false;
            RepeatCountControl.Enabled = false;

            tmrRecord.Start();
            this.WindowState = FormWindowState.Minimized;
        }

        private void tmrRecord_Tick(object sender, EventArgs e)
        {
            _RecordTime++;
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            UnsubscribeHooks();

            if (_IsRecording)
            {
                if (!_StoppedWithKey)
                    dgvActions.Rows.RemoveAt(dgvActions.Rows.Count - 1);
                _IsRecording = false;
                btnSaveToFile.Enabled = true;
            }
            else
            {
                if (_bWorker.IsBusy)
                {
                    _bWorker.CancelAsync();
                    tmrPlay.Dispose();
                    _bWorker.Dispose();
                }
            }

            tmrRecord.Stop();
            if (_RecordTime > 0)
            {
                btnPlayRecord.Enabled = true;
                RepeatCountControl.Enabled = true;
            }

            btnStartRecord.Enabled = true;
            btnSaveToFile.Enabled = true;
            btnLoadFromFile.Enabled = true;
            btnStopRecord.Enabled = false;

            this.WindowState = FormWindowState.Normal;
        }

        private void btnPlayRecord_Click(object sender, EventArgs e)
        {
            SubscribeHooks();

            _LoopCount = (int)RepeatCountControl.Value;
            btnStopRecord.Enabled = true;
            RepeatCountControl.Enabled = false;
            btnPlayRecord.Enabled = false;
            btnStartRecord.Enabled = false;
            btnSaveToFile.Enabled = false;
            btnLoadFromFile.Enabled = false;

            // Fona darbinieka inicializācija - atļaujam pārtraukt tā darbu,
            // norādam tā darba notikumus
            _bWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bWorker.DoWork += _bWorker_DoWork;
            _bWorker.RunWorkerCompleted += _bWorker_RunWorkerCompleted;

            _bWorker.RunWorkerAsync();
            this.WindowState = FormWindowState.Minimized;
        }

        void _bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) { _LoopCount = 0; tmrPlay.Dispose(); _bWorker.Dispose(); }
            else if (e.Error != null) { _LoopCount = 0; tmrPlay.Dispose(); _bWorker.Dispose(); } // tmrPlay.Dispose(); }
            else
            {
                _LoopCount--;
                if (_LoopCount > 0)
                {
                    _bWorker.RunWorkerAsync();
                }
            }
            if (_LoopCount < 0) _LoopCount = 0; // Normālā gadījumā, tā nevajadzētu būt. Negatīvs rezultāts iespējams vien tad, ja dators "noraustās".
            if (_LoopCount == 0) btnStopRecord.PerformClick();
            RepeatCountControl.Value = _LoopCount;
        }

        void _bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _PlaybackTime = 0;
            _CurrentRowIndex = 0;
            _RecordTime = UInt64.Parse(dgvActions.Rows[dgvActions.Rows.Count - 1].Cells["colTime"].Value.ToString()) + 10;

            tmrPlay = new System.Timers.Timer();
            tmrPlay.Interval = 1;
            tmrPlay.Elapsed += tmrPlay_Elapsed;
            tmrPlay.Start();

            while(_PlaybackTime < _RecordTime)
            {
                if(_bWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            };
            tmrPlay.Dispose();
        }

        void tmrPlay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_PlaybackTime != _RecordTime)
            {
                if (_CurrentRowIndex < dgvActions.Rows.Count)
                {
                    if (UInt64.Parse(dgvActions.Rows[_CurrentRowIndex].Cells["colTime"].Value.ToString()) == _PlaybackTime)
                    {
                        // Nolasa datus no tabulas
                        dgvActions.Rows[_CurrentRowIndex].Selected = true;
                        int x = int.Parse(dgvActions.Rows[_CurrentRowIndex].Cells["colX"].Value.ToString());
                        int y = int.Parse(dgvActions.Rows[_CurrentRowIndex].Cells["colY"].Value.ToString());
                        string buttonClicked = dgvActions.Rows[_CurrentRowIndex].Cells["colPress"].Value.ToString();
                        bool useMouse = Convert.ToBoolean(dgvActions.Rows[_CurrentRowIndex].Cells["colMouse"].Value);
                        string buttonModifier= dgvActions.Rows[_CurrentRowIndex].Cells["colModifier"].Value.ToString();

                        //Simulē peles klikšķi
                        if (useMouse)
                        {
                            Point cursorPosition = new Point(x, y);

                            // pārvieto kursoru
                            Cursor.Position = cursorPosition;
                            DoMouseClick(buttonClicked, cursorPosition);
                        }
                        else
                        {
                            DoKeyPress(buttonClicked, buttonModifier);
                        }

                        // Pāriet uz nākamo rindiņu
                        _CurrentRowIndex++;
                    }
                }
                _PlaybackTime++;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnStopRecord.Enabled = false;
            btnPlayRecord.Enabled = false;
            btnSaveToFile.Enabled = false;
            RepeatCountControl.Enabled = false;

            _IsRecording = false;
        }

        private void MouseKey_Hook_MouseClick(object sender, MouseEventArgs e)
        {
            if (_IsRecording)
            {
                _StoppedWithKey = false;
                string x = e.X.ToString();
                string y = e.Y.ToString();
                string click = e.Button.ToString();
                dgvActions.Rows.Add(x, y, click, _RecordTime, true, "None");
            }
        }

		//DP princips Nr.4 - Veidot universālas metodes lietām, kuras nepieciešams veikt vairākkārt (ar niecīgām izmaiņām, piem., mainīgajiem).
        //Palīdz izvairīties no koda dublēšanās
        void DoMouseClick(string button, Point location)
        {
            switch (button)
            {
                case "Left":
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, location.X, location.Y, 0, 0);
                    break;
                case "Right":
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, location.X, location.Y, 0, 0);
                    break;
                default:
                    break;
            }
        }

        void DoKeyPress(string button, string modifier)
        {
            Keys btn = (Keys)Enum.Parse(typeof(Keys), button);
            switch (modifier)
            {
                case "Shift":
                    _InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, (VirtualKeyCode)btn);
                    break;
                case "Control":
                    _InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, (VirtualKeyCode)btn);
                    break;
                case "Alt":
                    _InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, (VirtualKeyCode)btn);
                    break;
                default:
                    _InputSimulator.Keyboard.KeyPress((VirtualKeyCode)btn);
                    break;
            }
        }


        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            // Izveido faila izveides logu
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "output.mkdf";
            saveDialog.Filter = "MouseKey Recorder Data (*.mkdf)|*.mkdf";
            saveDialog.OverwritePrompt = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                List<MouseAction> actionList = new List<MouseAction>();
                actionList.Clear();

                // Saglabā visus ierakstītos peles notikumus dinamiskā masīvā
                foreach (DataGridViewRow row in dgvActions.Rows)
                {
                    MouseAction action = new MouseAction();
                    action.X = int.Parse(row.Cells["colX"].Value.ToString());
                    action.Y = int.Parse(row.Cells["colY"].Value.ToString());
                    action.Click = row.Cells["colPress"].Value.ToString();
                    action.Time = UInt64.Parse(row.Cells["colTime"].Value.ToString());
                    action.Mouse = Convert.ToBoolean(row.Cells["colMouse"].Value);
                    action.Modifier = row.Cells["colModifier"].Value.ToString();

                    actionList.Add(action);
                }

                // Eksportē masīvu ar saglabātajiem notikumiem, iepriekš norādītajā failā (XML formātā)
                XmlSerializer serializer = new XmlSerializer(typeof(List<MouseAction>));
                using (TextWriter writer = new StreamWriter(saveDialog.FileName, false, Encoding.UTF8))
                {
                    serializer.Serialize(writer, actionList);
                    writer.Close();
                }

                MessageBox.Show("Fails veiksmīgi saglabāts!", "Apsveicu!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            // Izveido faila izvēles logu
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "MouseKey Recorder Data (*.mkdf)|*.mkdf";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                dgvActions.Rows.Clear();

                // Nolasa iepriekš sagatavotu XML failu
                XmlSerializer deserializer = new XmlSerializer(typeof(ArrayOfMouseAction));
                TextReader reader = new StreamReader(openDialog.FileName);
                object obj = deserializer.Deserialize(reader);
                ArrayOfMouseAction XmlData = (ArrayOfMouseAction)obj;
                reader.Close();

                // Attēlo darbības tabulā
                foreach (MouseAction action in XmlData.actionList)
                {
                    dgvActions.Rows.Add(action.X, action.Y, action.Click, action.Time, action.Mouse, action.Modifier);
                }

                btnPlayRecord.Enabled = true;
                btnStopRecord.Enabled = true;
                RepeatCountControl.Enabled = true;
                RepeatCountControl.Value = 1;

                MessageBox.Show("Fails veiksmīgi ielādēts!", "Apsveicu!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RepeatCountControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void MouseKey_Hook_KeyDown(object sender, KeyEventArgs e)
        {
            // Nodrošina ieraksta/atkārtošanas pārtraukšanu ar F12
            if (e.KeyCode == Keys.F12)
            {
                _StoppedWithKey = true;
                btnStopRecord.PerformClick();
                e.Handled = true;
            }
            else if (_IsRecording)
            {
                _StoppedWithKey = false;
                string x = 0 + "";
                string y = 0 + "";
                dgvActions.Rows.Add(x, y, e.KeyCode.ToString(), _RecordTime, false, e.Modifiers);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnsubscribeHooks();
        }

    }
}
