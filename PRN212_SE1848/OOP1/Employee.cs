using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1
{
    public class Employee
    {
        #region Nhóm các thuộc tính của employees
        private int _id;
        private string _name;
        private string _id_card;
        private string _email;
        private string _phone;
        #endregion

        
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string IdCard { get { return _id_card; } set { _id_card = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }

        public void PrintInfor()
        {
            string msg = $"Id: {Id}\tName: {Name}\tIdCard: {IdCard}\tEmail: {Email}\tPhone: {Phone}";
            Console.WriteLine(msg);
        }

        #region Nhóm các constructors
        public Employee()
        {
            this._id = 0;
            this._name = "000";
            this._id_card = "OBAMA";
            this._email = "Chưa xác định";
            this._phone = "Chưa xác định";
        }

        public Employee(int id, string name, string id_card, string email, string phone)
        {
            this._id = id;
            this._name = name;
            this._id_card = id_card;
            this._email = email;
            this._phone = phone;
        }
        #endregion

        override public string ToString()
        {
            return $"Id: {Id}\tName: {Name}\tIdCard: {IdCard}\tEmail: {Email}\tPhone: {Phone}";
        }
    }
}
