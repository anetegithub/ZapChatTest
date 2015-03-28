using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace ZapChatTest.Logic
{
    public class ContactManager
    {
        private int MaxId { get; set; }
        public ContactManager()
        {
            this.ContactList = new List<Contact>();
            this.ContactList.Add(new Contact() { Id = 0 });
            MaxId++;
            this.ContactList.Add(new Contact("Paul") { Id = 1 });
            ++MaxId;
            this.ContactList.Add(new Contact("Melony") { Id = 2 });
            MaxId += 1;
        }
        public List<Contact> ContactList { get; set; }
        private ListView _ListUI { get; set; }
        public void SetListUI(ListView ListUI)
        {
            _ListUI = ListUI;
        }
        public void Add(Contact NewContact)
        {
            MaxId++;
            ContactList.Add(NewContact);
            this.OnAdd(NewContact, _ListUI);
        }
        public event OnAddItemHandler OnAdd;
        public delegate void OnAddItemHandler(Contact AddedContact, ListView ListUI);
    }
}
