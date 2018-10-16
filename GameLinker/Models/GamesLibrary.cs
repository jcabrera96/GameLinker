using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GameLinker.Models
{
    [Serializable]
    class GamesLibrary
    {
        private ArrayList library;
        public GamesLibrary()
        {
            library = new ArrayList();
        }

        public void AddGame(Game item)
        {
            library.Add(item);
        }

        public void RemoveGame(Game item)
        {
            library.Remove(item);
        }
    }
}
