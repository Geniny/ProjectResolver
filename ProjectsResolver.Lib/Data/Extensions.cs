using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Lib.Data
{
    public static class Extensions
    {
        public static bool GetState(this string state)
        {
            return state.ToLower() == "started" ? true : false;
        }
    }
}
