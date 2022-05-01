using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class MsgQueue
    {
        private int size;
        private int seqNo;

        // id - (status, msg)
        private SortedDictionary<int, List<string>> dicMsg; 

        public MsgQueue(int size)
        {
            this.size = size;
            this.seqNo = 0;
            dicMsg = new SortedDictionary<int, List<string>>();
        }

        public string MsgEnqueue(string msg)
        {
            if (dicMsg.Count == size)
                return "Queue Full";

            List<string> listMsg = new List<string>();
            listMsg.Add("A"); // status : available
            listMsg.Add(msg); // message
            dicMsg.Add(seqNo++, listMsg);

            return "Enqueued";
        }

        public string MsgDequeue()
        {
            if (dicMsg.Count == 0)
                return "Queue Empty";

            int key = dicMsg.First().Key;

            string res = dicMsg[key][1] + "(" + key + ")";

            dicMsg.Remove(key);

            return res;
        }

        public string MsgGet()
        {
            if (dicMsg.Count > 0)
                foreach(var item in dicMsg)
                {
                    if (item.Value[0] == "A")
                    {
                        item.Value[0] = "U";
                        return item.Value[1] + "(" + item.Key + ")";
                    }
                }

            return "No Msg";
        }

        public string MsgSet(int id)
        {
            if (dicMsg.Count > 0)
            {
                if (dicMsg.ContainsKey(id))
                {
                    dicMsg[id][0] = "A"; 
                    return "Msg Set";
                }
            }

            return "Set Fail";
        }

        public string MsgDel(int id)
        {
            if (dicMsg.Count > 0)
            {
                if (dicMsg.ContainsKey(id))
                {
                    dicMsg.Remove(id);
                    return "Deleted";
                }
            }

            return "Not Deleted";
        }
    }
}
