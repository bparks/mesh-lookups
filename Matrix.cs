using System;
using System.Collections.Generic;
using System.Linq;

namespace brianparks.Cherwell
{
    public static class Matrix
    {
        /*
         * This function returns a face (typically three points, ideally listed in
         * clockwise order). This function uses screen pixel orientation (that is,
         * the top left corner is the origin)
         */
        private static Face GetFaceFor(char row, int column)
        {
            row = char.ToUpper(row);
            if (row < 'A' || row > 'F')
                throw new ArgumentException("row", "Row must be between A and F inclusive");
            if (column < 1 || column > 12)
                throw new ArgumentException("column", "Column must be between 1 and 12 inclusive");
            int rowIndex = row - 'A'; //Turn the A-F into a 0-based row index
            int colIndex = column - 1; //Turn the (1-based) column specifier into a (0-based) column index

            Face face = new Face();

            //Get the top-left point
            face.Add(new Point(x: (colIndex / 2) * 10, y: rowIndex * 10));

            //In order to maintain clockwise winding, we must treat triangles with odd columns (at this
            //point, odd means the triangle points up to the right) differently than even columns (those
            //that point down and to the left)
            if (colIndex % 2 == 1)
            {
                //Top-right point
                face.Add(new Point(x: (colIndex / 2 + 1) * 10, y: rowIndex * 10));
                //Bottom-right point
                face.Add(new Point(x: (colIndex / 2 + 1) * 10, y: (rowIndex + 1) * 10));
            }
            else
            {
                //Bottom-right point
                face.Add(new Point(x: (colIndex / 2 + 1) * 10, y: (rowIndex + 1) * 10));
                //Bottom-left point
                face.Add(new Point(x: (colIndex / 2) * 10, y: (rowIndex + 1) * 10));
            }

            return face;
        }

        public static string GetRowColumnFor(Point pt1, Point pt2, Point pt3)
        {
            if (pt1 == pt2 || pt2 == pt3 || pt3 == pt1)
            {
                throw new ArgumentException("Do not specify identical points, as MULTIPLE faces may match");
            }

            //NOTE: There IS a purely numeric solution to this problem given the
            //specific set of triangles; however it WILL NOT SCALE to meshes of
            //arbitrary size and shapes. Therefore, the best way to accomplish
            //this is to search the set of known faces for a match

            //First, build the list. For this exercise, we'll use a list of
            //`KeyValuePair`s
            List<KeyValuePair<string, Face>> faces = new List<KeyValuePair<string, Face>>();
            for (char r = 'A'; r <= 'F'; r++)
                for (int c = 1; c <= 12; c++)
                    faces.Add(new KeyValuePair<string, Face>("" + r + c, GetFaceFor(row: r, column: c)));

            //Then we can just use some LINQ and let the framework do the work for us
            var result = faces.FirstOrDefault(f => {
                return f.Value.Vertices.Any(v => v == pt1) &&
                    f.Value.Vertices.Any(v => v == pt2) &&
                    f.Value.Vertices.Any(v => v == pt3);
            });
            if (result.Key == null)
                return "NONE";
            return result.Key;
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                //First part; show coordinates for each triangle
                for (char r = 'A'; r <= 'F'; r++)
                    for (int c = 1; c <= 12; c++)
                        Console.WriteLine("" + r + c + " : " + GetFaceFor(row: r, column: c));
            }
            else if (args.Length == 6)
            {
                //Second part; get the triangle that corresponds to the given points
                int x1, y1, x2, y2, x3, y3;
                if (int.TryParse(args[0], out x1) &&
                    int.TryParse(args[1], out y1) &&
                    int.TryParse(args[2], out x2) &&
                    int.TryParse(args[3], out y2) &&
                    int.TryParse(args[4], out x3) &&
                    int.TryParse(args[5], out y3))
                {
                    Console.WriteLine(GetRowColumnFor(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)));
                }
            }
            else
            {
                Console.WriteLine("Usage: matrix.exe [x1 y1 x2 y2 x3 y3]");
                Console.WriteLine("With no arguments, prints the full list of points defining faces");
                Console.WriteLine("With SIX arguments, prints the matching row/column or \"NONE\"");
            }
        }
    }
}
