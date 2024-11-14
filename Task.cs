using System.Collections.Immutable;

namespace Homework5
{
	public class Task
	{
        private string _name;
        private string _description;
        private int _timeToComplete;
        private bool _isComplete;
        private ImmutableDictionary<Skill, int> _skills;


        public Task(string name, string description, int timeToComplete, ImmutableDictionary<Skill, int> skills)
        {
            this._name = name;
            this._description = description;
            this._timeToComplete = timeToComplete;
            this._isComplete = false;
            this._skills = skills;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int BaseTime
        {
            get { return _timeToComplete; }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
        }

        public ImmutableDictionary<Skill, int> Skills
        {
            get { return _skills; }
        }

        // Precondition: none.
        /* Postcondition: The _isComplete property is set to true, indicating that the task is marked true.
         */
        public void MarkAsComplete()
        {
            _isComplete = true;
        }
	}
}

