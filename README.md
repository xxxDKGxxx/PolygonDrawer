# PolygonDrawer

A professional polygon drawing and editing application developed for the Computer Graphics course at the Faculty of Mathematics and Information Science (MiNI), Warsaw University of Technology (WUT).

## Overview

PolygonDrawer is a C# .NET Windows Forms application that allows users to create, modify, and manage complex polygons with various edge types and geometric constraints. The project focuses on core computer graphics algorithms, constraint resolution, and efficient rendering techniques.

## Features

- **Polygon Manipulation**
  - Interactive vertex addition and removal.
  - Drag-and-drop functionality for individual vertices and entire polygons.
  - Edge splitting to introduce new vertices.
- **Advanced Edge Types & Constraints**
  - **Standard Edges:** Basic linear connections.
  - **Vertical Edges:** Constraints edges to be perfectly vertical.
  - **Oblique Edges (45°):** Constraints edges to 45-degree increments.
  - **Fixed Length Edges:** Maintains a user-specified length during transformations.
  - **Bezier Curves:** Support for cubic Bezier segments.
  - **Circular Edges:** Support for arcs and circular segments.
- **Continuity Constraints**
  - Implementation of G0, G1, and C1 continuity for Bezier curve transitions.
- **Rendering Engines**
  - **GDI+ Renderer:** Utilizes built-in system libraries for drawing.
  - **Custom Renderer:** A custom implementation of rasterization algorithms (e.g., Bresenham's line algorithm) for educational and performance purposes.
- **Persistence**
  - Ability to save and load scenes from JSON files.

## Project Structure

- **PolygonDrawer**: The main Windows Forms project containing the UI components, event handling, and GDI-specific rendering implementations.
- **PolygonDrawer.Core**: A library containing the platform-independent logic, including:
  - Data structures for Polygons, Edges, and Points.
  - Constraint resolution engine.
  - Abstract rendering interfaces.

## Technologies

- **Language:** C#
- **Framework:** .NET 6.0 / 7.0+ (Windows Forms)
- **Serialization:** System.Text.Json
- **Architecture:** Decoupled Core logic with Visitor pattern for edge processing and Strategy pattern for rendering.

## Getting Started

### Prerequisites

- Visual Studio 2022 or higher.
- .NET SDK.

### Running the Application

1. Open `PolygonDrawer.sln` in Visual Studio.
2. Build the solution (Ctrl+Shift+B).
3. Run the `PolygonDrawer` project (F5).

## Academic Context

This project was developed as part of the Computer Graphics course at the Warsaw University of Technology. It demonstrates the application of geometric algorithms, interactive UI design, and software engineering principles in the context of graphical systems.
