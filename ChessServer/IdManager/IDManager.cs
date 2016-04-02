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
        public IDManager()
        {
            count = 0;
            freedID = new Stack<int>();
        }

        private int count;
        private Stack<int> freedID;
        private object lck = new object();

        public int GetId()
        {
            lock (lck)
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
        }
        public void Delete(int id)
        {
            lock (lck)
            {
                freedID.Push(id);
            }
        }
    }
}
