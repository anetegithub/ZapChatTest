using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZapChatTest.DataLayer
{
    public class Contact
    {
        public Contact()
        {
            _Name = Guid.NewGuid().ToString().Substring(0, 14);
        }

        public Contact(string Name)
        {
            _Name = Name;
        }

        public int Id { get; set; }
        private string _Name { get; set; }
        public string Name
        {
            get
            {
                return _Name;
            }

            private set
            {
                _Name = value;
            }
        }        
        private static Random r = new Random();
        public string Talk
        {
            get
            {
                _LastReplica = Speech[r.Next(4)];
                return _LastReplica;
            }
        }
        private string _LastReplica { get; set; }
        public string LastReplica
        {
            get
            {
                return _LastReplica;
            }
        }
        protected List<string> Speech = new List<string>()
        {
            "Hi!",
            "How are u?",
            "What you think about spam-bots?",
            "Electric funeral?",
            "TODO : add some repl*",
        };
    }
}