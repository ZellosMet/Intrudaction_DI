using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shape
{
    //Интерфейс для сохранения информации о фигуре в файл
    public interface IWorkerWithShape
    {
        void SaveImageToFile();
        void SaveNameToFile();
        void PrintNameShape();
        void PrintImageShape();
    }
    //Клласс реализующий сохранение информации о фигурах в файл
    public class SaveShapeToFile
    {
        IWorkerWithShape shape;
        public SaveShapeToFile(IWorkerWithShape shape)
        {
            this.shape = shape;
        }
        public void SaveNameShape()
        {
            shape.SaveNameToFile();
        }
        public void SaveImageShape()
        {
            shape.SaveImageToFile();
        }
    }
    //Клласс реализующий вывод информации о фигурах в консоль
    public class PrintShapeToConsole
    {
        IWorkerWithShape shape;
        public PrintShapeToConsole(IWorkerWithShape shape)
        {
            this.shape = shape;        
        }

        public void PrintNameShape()
        {
            shape.PrintNameShape();
        }
        public void PrintImageShape()
        { 
            shape.PrintImageShape();
        }
    }
    //Абстрактный класс фигур
    public abstract class Shape : IWorkerWithShape
    {
        protected string path = "shape.txt";
        public string Name { get; protected set; }
        public int SideA { get; }
        public int SideB { get; }
        public Shape(int side_a, int side_b)
        {
            SideA = side_a;
            SideB = side_b;
        }
        public Shape(int side)
        {
            SideA = side;
        }
        public abstract void PrintNameShape();
        public abstract void PrintImageShape();
        public abstract void SaveImageToFile();
        public virtual void SaveNameToFile()
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine(Name);
            }
        }
    }
    //Класс прямоугольник
    internal class Rectangle : Shape
    {
        public Rectangle(int side_a, int side_b) : base(side_a, side_b) { Name = "Rectangle"; }
        public override void PrintNameShape()
        {
            Console.WriteLine(Name+"\n");   
        }
        public override void PrintImageShape() 
        {
            for (int i = 0; i < SideB; i++)
            {
                for (int j = 0; j < SideA; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }
        public override void SaveImageToFile() 
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < SideB; i++)
                {
                    for (int j = 0; j < SideA; j++)
                        writer.Write("*");
                    writer.WriteLine();
                }
            }
        }
    }
    //Класс триугольник 
    internal class Triangle : Shape
    {
        int Height { get; set; }

        public Triangle(int height) : base(height) { Name = "Triangle"; }

        public override void PrintNameShape()
        {
            Console.WriteLine("Triangle\n");
        }
        public override void PrintImageShape()
        {
            int space = 0;
            for (int i = SideA; i > 0; i--)
            {
                for (int j = 0; j < i; j++)                
                    Console.Write(" ");
                for (int k = 0; k <= space*2; k++)
                    Console.Write("*");
                Console.WriteLine();
                space++;
            }
        }
        public override void SaveImageToFile()
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                int space = 0;
                for (int i = SideA; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                        writer.Write(" ");
                    for (int k = 0; k <= space * 2; k++)
                        writer.Write("*");
                    writer.WriteLine();
                    space++;
                }
            }
        }
    }
    //Класс круг
    internal class Circle : Shape
    {
        public Circle(int radius) : base(radius) { Name = "Circle"; }

        public override void PrintNameShape()
        {
            Console.WriteLine("Circle\n");
        }
        public override void PrintImageShape()
        {
            double a = SideA * 2;
            double b = SideA;
            for (int i = -SideA; i <= SideA; i++)
            {
                for (double j = -a; j <= a; j++)
                {
                    double d = (j/a)*(j/a)+(i/b)*(i/b);
                    if(d > 0.9 && d < 1.2) Console.Write("*");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        public override void SaveImageToFile()
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                double a = SideA * 2;
                double b = SideA;
                for (int i = -SideA; i <= SideA; i++)
                {
                    for (double j = -a; j <= a; j++)
                    {
                        double d = (j / a) * (j / a) + (i / b) * (i / b);
                        if (d > 0.9 && d < 1.2) writer.Write("*");
                        else writer.Write(" ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
    //Класс сохранения массива фигур в файл
    class SaveArrayShapeToFile
    {
        string path = "array_shape.";
        IWorkerWithShape[] array_shape;
        public SaveArrayShapeToFile(IWorkerWithShape[] shape, string format) 
        {
            array_shape = shape;
            path += format;
        }
        public void SaveArrayShape()
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            { 
                foreach (Shape shape in array_shape)
                    writer.Write($"{shape.Name} {shape.SideA},{shape.SideB}\n");                
            }
        }
    }
    //Класс чтения массива фигур из файла
    class LoadArrayShapeToFile
    {
        string path = "array_shape.";
        IWorkerWithShape[] array_shape;
        public LoadArrayShapeToFile(string format)
        {
            path += format;
        }
        public Shape[] LoadArrayShape()
        {
            List<Shape> list_shape = new List<Shape>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    switch (line.Split(' ')[0]) 
                    {
                        case "Rectangle":
                            list_shape.Add(new Rectangle(Convert.ToInt32(line.Split(' ')[1].Split(',')[0]), Convert.ToInt32(line.Split(' ')[1].Split(',')[1])));
                        break;
                        case "Triangle":
                            list_shape.Add(new Triangle(Convert.ToInt32(line.Split(' ')[1].Split(',')[0])));
                        break;
                        case "Circle":
                            list_shape.Add(new Circle(Convert.ToInt32(line.Split(' ')[1].Split(',')[0])));
                        break;
                    }
                }
                return list_shape.ToArray();
            }
        }
    }
}


