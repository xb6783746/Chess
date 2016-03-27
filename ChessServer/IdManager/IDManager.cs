using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessServer.Interfaces;

namespace ChessServer.IdManager 
{
    public class IDManager : IIDManager
    {
        private int count;
        private Stack<int> freedID;

        public IDManager()
        {
            count = 0;
            freedID = new Stack<int>();
        }

        public int GetId()
        {
            if (freedID.Count != 0)
            {
                return freedID.Pop();
            }
            else
            {
                count++;
                return count;
            }
        }
        public void Delete(int id)
        {
            freedID.Push(id);
        }
    }
}
