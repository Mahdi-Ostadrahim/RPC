namespace GrpcGreeter.Models
{
    public class Database
    {
        private List<IWorker> workers;
        public Database()
        {
            string[] names = {"mahdi","ali","reza","morteza","hossein","hassan","kianoosh","behnam","zahra","nazanin","pooya","mahya"};
            workers = new List<IWorker>();
            for (int i = 0; i < names.Length; i++)
            {
                bool gender = true;
                if (i == 8 || i == 9 || i == 11)
                    gender = false;
                IHuman human = new Human(names[i], 18+i, $"10000000{i}", gender);
                if (i % 2 == 0)
                    workers.Add(new Worker(human, false, Math.Max(0, i - 5)));
                else
                    workers.Add(new Worker(human, true, Math.Max(0, i - 5),names[i].Substring(1),1000+i));
            }
        }
        public void AddWorker(IWorker worker)
        {
            workers.Add(worker);
        }
        public void DeleteWorker(string worker)
        {
            for (int i = 0; i < workers.Count; i++)
                if (workers[i].GetNationalID() == worker)
                {
                    workers.RemoveAt(i);
                    break;
                }
        }
        public IWorker GetWorker(string name)
        {
            for (int i = 0; i < workers.Count;i++)
                if (workers[i].GetName()==name)
                    return workers[i];
            return null;
        }
    }
}
