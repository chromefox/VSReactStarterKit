namespace ExperimentApplication.ConceptClasses.QueueConcept
{
    public class WorkItem
    {
        public string Guid;

        private bool _isDone;

        public WorkItem(string guid)
        {
            Guid = guid;
            _isDone = false;
        }

        public void MarkAsDone()
        {
            _isDone = true;
        }

        public bool IsDone => _isDone;
    }
}
