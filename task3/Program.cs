using System;

public interface IRenderer
{
    void Render(string shapeName);
}

public class VectorRenderer : IRenderer
{
    public void Render(string shapeName)
    {
        Console.WriteLine($"Drawing {shapeName} as vectors");
    }
}

public class RasterRenderer : IRenderer
{
    public void Render(string shapeName)
    {
        Console.WriteLine($"Drawing {shapeName} as pixels");
    }
}

public abstract class Shape
{
    protected IRenderer _renderer;

    protected Shape(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public abstract void Draw();
}

public class Circle : Shape
{
    public Circle(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        _renderer.Render("Circle");
    }
}

public class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        _renderer.Render("Square");
    }
}

public class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        _renderer.Render("Triangle");
    }
}

class Program
{
    static void Main(string[] args)
    {
        IRenderer vector = new VectorRenderer();
        IRenderer raster = new RasterRenderer();

        Shape circle = new Circle(vector);
        Shape square = new Square(raster);
        Shape triangle = new Triangle(vector);

        circle.Draw();
        square.Draw();
        triangle.Draw();

        Shape rasterCircle = new Circle(raster);
        rasterCircle.Draw();
    }
}