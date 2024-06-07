using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Grade
    {
        public double Reading { get; set; } = 0.0;
        public double Listening { get; set; } = 0.0;
        public double Speaking { get; set; } = 0.0;
        public double Writing { get; set; } = 0.0;

        public double Total
        {
            get
            {
                return Reading + Listening + Speaking + Writing;
            }
        }

        public bool Passed
        {
            get
            {
                if (Reading >= 0.5 * 60 && Writing >= 0.5 * 60 && Listening >= 0.5 * 40 && Speaking >= 0.5 * 50)
                {
                    if ((Reading + Writing + Listening + Speaking) >= 160)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
