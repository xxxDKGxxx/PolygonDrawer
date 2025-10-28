using Newtonsoft.Json;
using PolygonDrawer.Core;
using PolygonDrawer.Core.Edges;
using PolygonDrawer.Core.Edges.EdgeTypes;
using PolygonDrawer.Core.Rendering;
using PolygonDrawer.EdgeVisitors;
using PolygonDrawer.Renderers;

namespace PolygonDrawer
{
    public partial class PolygonDrawer : Form
    {
        private readonly GdiRenderer _gdiRenderer = new();
        private readonly CustomRenderer _customRenderer = new();
        private List<IGdiRenderer> Renderers => [_gdiRenderer, _customRenderer];
        private Polygon _polygon = new();

        private Core.Point? _selectedVert = null;
        private Edge? _selectedEdge = null;

        private bool _isPolygonDragged = false;
        private bool _isPointDragged = false;
        private System.Drawing.Point? _lastMousePosition = null;

        public PolygonDrawer()
        {
            InitializeComponent();
            InitializeCanvas();
            SetButtonEvents();
            LoadPolygonFromJson("predefined-scene.json");
        }

        private void SetButtonEvents()
        {
            resetButton.Click += (s, e) =>
            {
                _polygon.Clear();
                mainCanvas.Invalidate();
            };
            deleteVertexButton.Click += (s, e) =>
            {
                if (_selectedVert is null)
                {
                    return;
                }

                _polygon.RemoveVertex(_selectedVert);
                _selectedVert = null;
                selectedVertexLabel.Text = "None";
                deleteVertexButton.Enabled = false;

                SetDefaultRenderer();
                mainCanvas.Invalidate();
            };
            splitEdgeButton.Click += (s, e) =>
            {
                if (_selectedEdge is null)
                {
                    return;
                }
                _polygon.SplitEdge(_selectedEdge);
                _selectedEdge = null;
                selectedEdgeLabel.Text = "None";
                splitEdgeButton.Enabled = false;
                SetDefaultRenderer();
                mainCanvas.Invalidate();
            };

            verticalRadioButton.CheckedChanged += (s, e) =>
            {
                ChangeEdge<VerticalEdge>(verticalRadioButton);
            };

            obliqueRadioButton.CheckedChanged += (s, e) =>
            {
                ChangeEdge<Deg45Edge>(obliqueRadioButton);
            };

            normalRadioButton.CheckedChanged += (s, e) =>
            {
                ChangeEdge<Edge>(normalRadioButton);
            };

            fixedRadioButton.CheckedChanged += (s, e) =>
            {
                ChangeEdge<FixedLengthEdge>(fixedRadioButton);
            };

            setLengthButton.Click += (s, e) =>
            {
                if (_selectedEdge is not FixedLengthEdge fle)
                {
                    return;
                }
                if (int.TryParse(edgeLengthTextBox.Text, out var length) && length > 0)
                {
                    _polygon.ChangeLength(fle, length);
                    mainCanvas.Invalidate();
                }
            };

            bezierRadioButton.CheckedChanged += (s, e) =>
            {
                ChangeEdge<BezierEdge>(bezierRadioButton);
            };

            circularRadioButton.CheckedChanged += (s, e) =>
            {
                ChangeEdge<CircleEdge>(circularRadioButton);
            };

            g0RadioButton.CheckedChanged += (s, e) =>
            {
                SetContinuuity(g0RadioButton, ContinuuityType.G0);
            };

            g1RadioButton.CheckedChanged += (s, e) =>
            {
                SetContinuuity(g1RadioButton, ContinuuityType.G1);
            };

            c1RadioButton.CheckedChanged += (s, e) =>
            {
                SetContinuuity(c1RadioButton, ContinuuityType.C1);
            };

            gdiRenderrRadioButton.CheckedChanged += (s, e) =>
            {
                SetRenderer(gdiRenderrRadioButton, _gdiRenderer);
            };

            customRenderingRadioButton.CheckedChanged += (s, e) =>
            {
                SetRenderer(customRenderingRadioButton, _customRenderer);
            };
        }

        private void SetContinuuity(RadioButton radioButton, ContinuuityType continuityType)
        {
            if (_selectedVert is null
                || _selectedVert.Type == continuityType
                || !radioButton.Checked)
            {
                return;
            }

            _polygon.SetVertexContinuity(_selectedVert, continuityType);
            mainCanvas.Invalidate();
        }

        private void SetRenderer(RadioButton radioButton, IRenderer renderer)
        {
            if (_selectedVert is null && _selectedEdge is null || !radioButton.Checked)
            {
                return;
            }

            if (_selectedEdge is not null)
            {
                _selectedEdge.SetRenderer(renderer);

                return;
            }

            _selectedVert?.SetRenderer(renderer);
            mainCanvas?.Invalidate();
        }

        private void ChangeEdge<T>(RadioButton radioButton) where T : Edge
        {
            if (_selectedEdge is null || _selectedEdge.GetType() == typeof(T) || !radioButton.Checked)
            {
                return;
            }

            if (Activator.CreateInstance(typeof(T), _selectedEdge) is not Edge newEdge)
            {
                return;
            }

            _polygon.ReplaceEdge(_selectedEdge, newEdge);

            _selectedEdge = newEdge;

            SetDefaultRenderer();

            mainCanvas.Invalidate();
        }

        private void SetDefaultRenderer()
        {
            foreach (var edge in _polygon.Edges.Where(e => e.Renderer is null))
            {
                edge.Renderer = _gdiRenderer;
            }

            foreach (var vertex in _polygon.Vertices.Where(v => v.Renderer is null))
            {
                vertex.Renderer = _gdiRenderer;
            }

            mainCanvas.Invalidate();
        }

        private void InitializeCanvas()
        {
            mainCanvas.Image = new Bitmap(mainCanvas.Width, mainCanvas.Height);
            mainCanvas.BackColor = Color.White;
            mainCanvas.Paint += MainCanvas_Paint;
            mainCanvas.MouseMove += MainCanvas_MouseMove;
            mainCanvas.MouseClick += MainCanvas_MouseClick;
            mainCanvas.MouseDown += MainCanvas_MouseDown;
            mainCanvas.MouseUp += MainCanvas_MouseUp;
        }

        private void MainCanvas_MouseUp(object? sender, MouseEventArgs e)
        {
            _isPointDragged = false;
            _isPolygonDragged = false;
            _lastMousePosition = null;
        }

        private void MainCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            var shiftPressed = (ModifierKeys & Keys.Shift) == Keys.Shift;
            var ctrlPressed = (ModifierKeys & Keys.Control) == Keys.Control;

            if (shiftPressed && _polygon.Vertices.Count > 0)
            {
                _isPolygonDragged = true;
                _lastMousePosition = new System.Drawing.Point(e.X, e.Y);
                return;
            }

            _selectedEdge = ctrlPressed
                    && _selectedVert is not null
                    && _polygon.GetVertexNear(e.X, e.Y) is Core.Point otherVert
                    && otherVert != _selectedVert
                ? _polygon.GetEdgeByPoints(_selectedVert, otherVert)
                : null;

            selectedEdgeLabel.Text = _selectedEdge is null ? "None" : _selectedEdge.ToString();
            splitEdgeButton.Enabled = _selectedEdge is not null;

            verticalRadioButton.Enabled = _selectedEdge is not null
                && !_polygon.GetEdgeNeighbors(_selectedEdge)
                    .Any(e => e is VerticalEdge);

            verticalRadioButton.Checked = _selectedEdge is VerticalEdge;
            obliqueRadioButton.Enabled = _selectedEdge is not null;
            obliqueRadioButton.Checked = _selectedEdge is Deg45Edge;
            normalRadioButton.Enabled = _selectedEdge is not null;
            normalRadioButton.Checked = _selectedEdge?.GetType() == typeof(Edge);
            fixedRadioButton.Enabled = _selectedEdge is not null;
            fixedRadioButton.Checked = _selectedEdge is FixedLengthEdge;
            bezierRadioButton.Enabled = _selectedEdge is not null;
            bezierRadioButton.Checked = _selectedEdge is BezierEdge;
            circularRadioButton.Enabled = _selectedEdge is not null;
            circularRadioButton.Checked = _selectedEdge is CircleEdge;

            edgeLengthTextBox.Enabled = _selectedEdge is FixedLengthEdge;
            edgeLengthTextBox.Text = _selectedEdge is FixedLengthEdge fle
                ? fle.FixedLength.ToString()
                : string.Empty;

            gdiRenderrRadioButton.Enabled = _selectedEdge is not null || _selectedVert is not null;
            gdiRenderrRadioButton.Checked = _selectedEdge is not null
                ? _selectedEdge.Renderer == _gdiRenderer
                : _selectedVert?.Renderer == _gdiRenderer;

            customRenderingRadioButton.Enabled = _selectedEdge is not null || _selectedVert is not null;
            customRenderingRadioButton.Checked = _selectedEdge is not null
                ? _selectedEdge.Renderer == _customRenderer
                : _selectedEdge?.Renderer == _customRenderer;

            _selectedVert = _polygon.GetVertexNear(e.X, e.Y);

            selectedVertexLabel.Text = _selectedVert is null ? "None" : _selectedVert.ToString();
            deleteVertexButton.Enabled = _selectedVert is not null;
            g0RadioButton.Enabled = _selectedVert is not null;
            g0RadioButton.Checked = _selectedVert?.Type == ContinuuityType.G0;
            g1RadioButton.Enabled = _selectedVert is not null && !_polygon.GetEdgesByPoint(_selectedVert)
                .Any(e => e is CircleEdge ce
                    && ce.GetPoints()
                        .Any(p => p != _selectedVert
                            && p.Type == ContinuuityType.G1));

            g1RadioButton.Checked = _selectedVert?.Type == ContinuuityType.G1;
            c1RadioButton.Enabled = _selectedVert is not null && !_polygon.GetEdgesByPoint(_selectedVert)
                .Any(e => e is CircleEdge);
            c1RadioButton.Checked = _selectedVert?.Type == ContinuuityType.C1;

            _isPointDragged = true;

            mainCanvas.Invalidate();
        }

        private void MainCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isPolygonDragged && _lastMousePosition.HasValue)
            {
                var dx = e.X - _lastMousePosition.Value.X;
                var dy = e.Y - _lastMousePosition.Value.Y;

                if (dx == 0 && dy == 0)
                {
                    return;
                }

                _polygon.Translate(dx, dy);

                _lastMousePosition = new System.Drawing.Point(e.X, e.Y);

                mainCanvas.Invalidate();
                return;
            }

            if (_selectedVert is null || _isPointDragged is false)
            {
                return;
            }

            _polygon.MovePoint(_selectedVert, e.X, e.Y);

            mainCanvas.Invalidate();
        }

        private void MainCanvas_MouseClick(object? sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (_polygon.IsClosed)
                    {
                        break;
                    }

                    var vertex = new Core.Point(e.X, e.Y);
                    _polygon.AddVertex(vertex);
                    SetDefaultRenderer();
                    mainCanvas.Invalidate();
                    break;
                case MouseButtons.Right:
                    if (_polygon.IsClosed)
                    {
                        break;
                    }

                    _polygon.ClosePolygon();
                    SetDefaultRenderer();
                    mainCanvas.Invalidate();
                    break;
            }
        }

        private void MainCanvas_Paint(object? sender, PaintEventArgs e)
        {
            var polygonCenter = _polygon.GetCenter();

            if (polygonCenter.x > mainCanvas.Width || polygonCenter.y > mainCanvas.Height)
            {
                _polygon.Translate(
                    mainCanvas.Width / 2 - polygonCenter.x,
                    mainCanvas.Height / 2 - polygonCenter.y);
            }

            var graphics = e.Graphics;
            var renderers = Renderers;

            renderers.ForEach(r => r.SetGraphics(graphics));

            using var font = new Font(SystemFonts.DefaultFont.FontFamily, 8f);

            foreach (var vertex in _polygon.Vertices)
            {
                var brush = ReferenceEquals(vertex, _selectedVert)
                        || (_selectedEdge?.GetPoints()
                            .Contains(vertex) ?? false)
                    ? Brushes.Lime
                    : Brushes.Red;

                renderers.ForEach(r => r.SetPointBrush(brush));

                vertex.Render();

                var text = $"{vertex.VertexNum} ({vertex.Type})";
                var textPos = new PointF(vertex.X + 6, vertex.Y - 8);

                graphics.DrawString(text, font, Brushes.Black, textPos);
            }

            renderers.ForEach(r => r.SetPointBrush(Brushes.Black));

            var edgeLabelRenderer = new GdiEdgeLabelRenderer(graphics);

            foreach (var edge in _polygon.Edges)
            {
                edge.Render();
                edge.Accept(edgeLabelRenderer);
            }
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new HelpForm();

            dialog.ShowDialog();
        }

        private void SerializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki json (.json)|*.json"
            };

            var dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            var path = saveFileDialog.FileName;

            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            var serialized = JsonConvert.SerializeObject(_polygon, settings);

            File.WriteAllText(path, serialized);
        }

        private void DeserializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki json (.json)|*.json"
            };

            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            var path = openFileDialog.FileName;

            bool flowControl = LoadPolygonFromJson(path);

            if (!flowControl)
            {
                return;
            }
        }

        private bool LoadPolygonFromJson(string path)
        {
            var serialized = File.ReadAllText(path);

            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            var polygon = JsonConvert.DeserializeObject<Polygon>(serialized, settings);

            if (polygon is null)
            {
                MessageBox.Show("Serialization failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _polygon = polygon;
            SetDefaultRenderer();
            mainCanvas.Invalidate();
            return true;
        }
    }
}