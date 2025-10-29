namespace PolygonDrawer.Core.Rendering;

public interface IRenderable
{
    void Render();
    void SetRenderer(IRenderer renderer);
}