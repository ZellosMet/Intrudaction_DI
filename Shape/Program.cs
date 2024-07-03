using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Rectangle rec = new Rectangle(5, 6);
            //Circle cir = new Circle(5);
            PrintShapeToConsole psc = new PrintShapeToConsole(new Rectangle(5, 6));
            psc.PrintNameShape();
            psc.PrintImageShape();
            SaveShapeToFile ssf = new SaveShapeToFile(new Rectangle(5, 6));
            //ssf.SaveNameShape();
            ssf.SaveImageShape();

            Console.WriteLine("Сохранение массива фигур");
            Shape[] arr_shape = new Shape[] {new Rectangle(3,6), new Circle(4), new Triangle(5)};
            foreach (Shape shape in arr_shape)
                shape.PrintNameShape();
            SaveArrayShapeToFile sasf = new SaveArrayShapeToFile(arr_shape, "doc");
            sasf.SaveArrayShape();


            Console.WriteLine("Загрузка массива фигур");
            LoadArrayShapeToFile lasf = new LoadArrayShapeToFile("doc");
            Shape[] loading_array_shape = lasf.LoadArrayShape();
            foreach (Shape shape in loading_array_shape)
                shape.PrintImageShape();
        }
    }
}
