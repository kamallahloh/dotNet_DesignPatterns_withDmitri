using static System.Console;
namespace SOLID.LSP
{
    // using a classic example LISKOV  substitution
    public class Rectangle
    {
        //public int Width { get; set; }
        //public int Height { get; set; }

        public virtual int Width { get; set; } // virtual so it can be later overridden
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString() // when print console.write 
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        // don't use new keyword

        //public new int Width
        //{
        //  set { base.Width = base.Height = value; }
        //}

        //public new int Height
        //{ 
        //  set { base.Width = base.Height = value; }
        //}

        public override int Width // nasty side effects
        {
            set { base.Width = base.Height = value; } // setter
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    public class Demo
    {
        static public int Area(Rectangle r) => r.Width * r.Height;

        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 3);
            WriteLine($"{rc} has area {Area(rc)}");

            // should be able to substitute a base type for a subtype
            /*Square*/
            Rectangle sq = new Square(); // up-cast to base type, sq should stay sq even we subs. it with rec reference
            sq.Width = 4; // with virtual and override this will call the appropriate setter 
            WriteLine($"{sq} has area {Area(sq)}");
            Console.ReadLine();
        }
    }
}


