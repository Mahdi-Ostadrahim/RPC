using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrpcGreeter.Models
{
    public interface IHuman
    {
        public string GetName();
        public int GetAge();
        public string GetNationalID();
        public void SetAge(int age);
        public bool IsMale();
    }
    public interface IWorker : IHuman
    {
        public bool IsEmployeed();
        public int GetWorkerID();
        public string GetCompany();
        public int GetExperience();
        public void SetExperience(int experience);
        public void SetEmployment(string companyname, int workerID);
        public void EndofEmployment();
        public string GetPerson();
    }
    public class Human : IHuman
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        private string _nationalID { get; set; }
        //[BsonElement("Name")]
        private string _name { get; set; }
        //[BsonElement("Age")]
        private int _age { get; set; }
        //[BsonElement("IsMale")]
        private bool _isMale { get; set; }
        public Human(string name, int age, string nationalID, bool isMale)
        {
            _name = name;
            _age = age;
            _nationalID = nationalID;
            _isMale = isMale;
        }
        public int GetAge()
        {
            return _age;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetNationalID()
        {
            return _nationalID;
        }

        public bool IsMale()
        {
            return _isMale;
        }

        public void SetAge(int age)
        {
            this._age = age;
        }
    }
    public class Worker : IWorker
    {
        [BsonId]
        public string _nationalID { get; set; }
        [BsonElement("Name")]
        private string _name { get; set; }
        [BsonElement("Age")]
        private int _age { get; set; }
        [BsonElement("IsMale")]
        private bool _isMale { get; set; }
        [BsonElement("CompanyName")]
        private string _companyname { get; set; }
        [BsonElement("Years of Experience")]
        private int _experience { get; set; }
        [BsonElement("Is Employeed")]
        private bool _isEmployeed { get; set; }
        [BsonElement("Worker ID")]
        private int _workerID { get; set; }
        public Worker(string name, int age, string nationalID, bool isMale, bool isEmployeed, int experience = 0, string companyname = "", int workerID = 0)
        {
            _nationalID = nationalID;
            _name = name;
            _age = age;
            _isMale = isMale;
            _companyname = companyname;
            _workerID = workerID;
            _experience = experience;
            _isEmployeed = isEmployeed;
        }
        public string GetCompany()
        {
            return this._companyname;
        }

        public int GetExperience()
        {
            return this._experience;
        }

        public int GetAge()
        {
            return _age;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetNationalID()
        {
            return _nationalID;
        }

        public bool IsEmployeed()
        {
            return this._isEmployeed;
        }

        public bool IsMale()
        {
            return _isMale;
        }

        public void SetAge(int age)
        {
            this._age = age;
        }

        public int GetWorkerID()
        {
            return this._workerID;
        }

        public void SetExperience(int experience)
        {
            this._experience = experience;
        }

        public void SetEmployment(string companyname, int workerID)
        {
            this._companyname = companyname;
            this._workerID = workerID;
            this._isEmployeed = true;
        }

        public void EndofEmployment()
        {
            this._isEmployeed = false;
            this._companyname="";
            this._workerID = 0;
        }

        public string GetPerson()
        {
            string s = "";
            s += $"{this._name} is ";
            if (_isMale)
                s += "male";
            else
                s += "female";
            s += $" and {this._age} years old and has {this._experience} years of experience\n";
            if (this._isMale)
                s += "He ";
            else
                s += "She ";
            if (this._isEmployeed)
                s += $"is currently employeed at {this._companyname} with worker ID of {this._workerID}";
            else
                s += "is currently not employeed";
            return s;
        }
    }
}
