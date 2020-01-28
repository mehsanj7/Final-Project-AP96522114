using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_NoteBook_Linear_Equations
{
    public static class Equations
    {
        static double determinantOfMatrix(double[,] mat)
        {
            double ans;
            ans = mat[0, 0] * (mat[1, 1] * mat[2, 2] - mat[2, 1] * mat[1, 2])
                - mat[0, 1] * (mat[1, 0] * mat[2, 2] - mat[1, 2] * mat[2, 0])
                + mat[0, 2] * (mat[1, 0] * mat[2, 1] - mat[1, 1] * mat[2, 0]);
            return ans;
        }

        public static string findSolution(double[,] zarib)
        {
            double[,] d = {
        { zarib[0,0], zarib[0,1], zarib[0,2] },
        { zarib[1,0], zarib[1,1], zarib[1,2] },
        { zarib[2,0], zarib[2,1], zarib[2,2] },
        };

            double[,] d1 = {
        { zarib[0,3], zarib[0,1], zarib[0,2] },
        { zarib[1,3], zarib[1,1], zarib[1,2] },
        { zarib[2,3], zarib[2,1], zarib[2,2] },
        };

            double[,] d2 = {
        { zarib[0,0], zarib[0,3], zarib[0,2] },
        { zarib[1,0], zarib[1,3], zarib[1,2] },
        { zarib[2,0], zarib[2,3], zarib[2,2] },
        };

            double[,] d3 = {
        { zarib[0,0], zarib[0,1], zarib[0,3] },
        { zarib[1,0], zarib[1,1], zarib[1,3] },
        { zarib[2,0], zarib[2,1], zarib[2,3] },
        };

            double D = determinantOfMatrix(d);
            double D1 = determinantOfMatrix(d1);
            double D2 = determinantOfMatrix(d2);
            double D3 = determinantOfMatrix(d3);

            if (D != 0)
            {
                double x = D1 / D;
                double y = D2 / D;
                double z = D3 / D;
                return "x= " + x + " * " + "y= " + y + " * " + "z= " + z;
            }

            else
            {
                if (D1 == 0 && D2 == 0 && D3 == 0)
                    return "Infinite solutions";
                else if (D1 != 0 || D2 != 0 || D3 != 0)
                    return "No solutions";
            }
            return "";
        }


    }
}
