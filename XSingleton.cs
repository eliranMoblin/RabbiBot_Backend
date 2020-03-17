using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbiBot_Backend
{
    public class XSingleton
    {
        public static bool NavBarDisplay = true;
        private static XSingleton instance = null;

        private XSingleton()
        {
        }

        public static XSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XSingleton();
                }
                return instance;
            }
        }
        //public Blazor_Dashboard.Shared.__NavMenu NavMenu;

    }
}
