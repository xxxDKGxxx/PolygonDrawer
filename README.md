# PolygonDrawer

A professional vector polygon drawing, editing, and geometric constraint resolution application developed for the Computer Graphics course at the Faculty of Mathematics and Information Science (MiNI), Warsaw University of Technology (WUT).

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-0078d7.svg)
![Framework](https://img.shields.io/badge/.NET-9.0--windows-512bd4.svg)
![Language](https://img.shields.io/badge/language-C%23-239120.svg)
![GUI](https://img.shields.io/badge/gui-Windows%20Forms-0078d7.svg)

---

## 🚀 Overview

**PolygonDrawer** is a desktop application that enables creating, editing, and rasterizing complex polygons with custom geometric edge constraints and continuity conditions. The architecture cleanly separates domain algorithms and constraint solvers into a platform-agnostic Core library, utilizing design patterns such as **Visitor** (for edge processing) and **Strategy** (for rendering backends).

---

## ✨ Features

### ✏️ Polygon Manipulation
- **Interactive Editing**: Add and remove vertices dynamically.
- **Drag-and-Drop**: Smooth drag operations for individual vertices, edge segments, or whole polygon shapes.
- **Edge Splitting**: Subdivide edges to insert new control points into existing shapes.

### 📐 Edge Constraints & Geometry Types
- **Standard Edges**: Straight linear connections between control points.
- **Vertical Edges**: Constrains edges to strictly vertical orientations ($x_1 = x_2$).
- **Oblique Edges (45°)**: Constrains edge angles to 45-degree increments.
- **Fixed-Length Edges**: Preserves exact edge lengths during vertex movements.
- **Cubic Bézier Curves**: Multi-point control curves with interactive handle manipulation.
- **Circular Arc Edges**: Curving arc segments defined by radius and sweep angle.

### 🔗 Smooth Continuity Constraints
- **$G^0$ / $G^1$ / $C^1$ Continuity**: Enforces positional, geometric tangency ($G^1$), and parametric velocity ($C^1$) continuity across adjacent Bézier curve segments.

### 🖌️ Rendering Engines
- **GDI+ Renderer**: Utilizes native System.Drawing hardware-accelerated 2D graphics API.
- **Custom Rasterizer**: Educational, low-level implementation of fundamental graphics algorithms (e.g., Bresenham's line algorithm and midpoint circle algorithm).

### 💾 Scene Persistence
- **JSON Scene Import/Export**: Save and load complete polygon configurations, constraint setups, and Bézier curves using Newtonsoft.Json.

---

## 🛠️ Project Architecture & Tech Stack

- **Target Framework**: .NET 9.0 (Windows Forms)
- **Language**: C#
- **Libraries**: `Newtonsoft.Json`

### Solution Structure
- `PolygonDrawer.Core`: Core domain models, geometric algorithms, constraint resolution engine, and abstract rendering interfaces.
- `PolygonDrawer`: Windows Forms UI application, event handlers, canvas controls, and rendering context bindings.

---

## 💻 Getting Started

### Prerequisites

- **Visual Studio 2022** (v17.12 or newer with .NET 9 SDK)
- **Windows OS** (required for WinForms runtime)

### Running the Application

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/PolygonDrawer.git
   cd PolygonDrawer
   ```

2. **Open & Build via Visual Studio**:
   - Open `PolygonDrawer.sln`.
   - Press **Ctrl + Shift + B** to build.
   - Press **F5** to launch `PolygonDrawer`.

3. **Or run via .NET CLI**:
   ```bash
   dotnet run --project PolygonDrawer/PolygonDrawer.csproj
   ```

---

## 📜 License

This project is licensed under the [MIT License](LICENSE).
