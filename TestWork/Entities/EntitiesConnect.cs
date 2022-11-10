using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Entities;

namespace TestWork
{
    public static class EntitiesConnect
    {
        public static MainEntities DB = new MainEntities();
        internal static Users userAuth = new Users();
    }
}
