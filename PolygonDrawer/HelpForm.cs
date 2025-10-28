using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonDrawer
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            InitializeHelpText();
        }

        private void InitializeHelpText()
        {
            mainTextBox.AppendText("Klawiszologia:\n");
            mainTextBox.AppendText("\nDefiniowanie wielokąta:\n");
            mainTextBox.AppendText("LPM - dodanie kolejnego wierzchołka\n");
            mainTextBox.AppendText("RPM - zakończenie wielokąta (ostatni dodany wierzchołek łączy się z pierwszym)\n");
            mainTextBox.AppendText("\nEdycja wielokąta:\n");
            mainTextBox.AppendText("LPM hold + wierzchołek + ruch myszy - zmiana pozycji wierzchołka\n");
            mainTextBox.AppendText("Shift + LPM hold + ruch myszy - zmiana pozycji całego wielokąta\n");
            mainTextBox.AppendText("LPM + wierzchołek + click - zaznaczanie wierzchołka\n");
            mainTextBox.AppendText(
                "Ctrl + LPM + click - zaznaczanie krawędzi "
                + "(należy z ctrl zaznaczyć dwa wierzchołki definiujące krawędź)\n");
        }
    }
}