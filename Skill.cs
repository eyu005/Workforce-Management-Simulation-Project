using System;
namespace Homework5
{
    /* Class Invarients
     * - Two different instances of the Skill class should have different combinations of the name and description 
     * - Cost calculated in the CalculateCost method should always be non-negative. 
     * - Proficiency should not be zero in the SubSkill.CalculateCost and RandomizedSkill.CalculateCost. 
     * - The use of the randomNumberGenerator should ensure consistent and reproducible results. 
     */
    public class Skill
    {
        private readonly string _name;          //Immuntable variable name 
        private readonly string _description;   //Immutable variable description

        //Pre-condition: This is a constructor that takes in a string name and a description
        /*Post-condition: This method creates an instance of a class Skill by setting _name equal
		 * to name and _description equal to description.
		 */
        public Skill(string name, string description)
        {
            this._name = name;
            this._description = description;
        }
        //Pre-condition: none
        //Post condition: This is a getter that allows the user to get name. 
        public string Name
        {
            get { return _name; }
        }
        //Pre-condition: none
        //Post condition: This is a getter that allows the user to get description. 
        public string Description
        {
            get { return _description; }
        }

        //Pre-condition: Object is not null.
        /*Post-condition: Returns true if the provided object is not null and is of the same type as the 
         * current object. otherwise, return false. If the provided object is of the same type, compares the name and description 
         * properties of the current object and the provided object. Returns true if they are equal, false otherwise.
		 */
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Skill otherSkill = (Skill)obj;
            return _name == otherSkill._name && _description == otherSkill._description;
        }

        //Pre-condition: None.
        /*Post-condition: Returns a hash code based on the values of the _name and _description. 
         * Two skill objects that are considered equal should return the same hash code. 
		 */
        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _description);
        }

        //Pre-condition: None
        /*Post-condition: Returns the calculated cost based on the provided parameters. 
         * This method does not modify the state of the object. 
		 */
        public virtual int CalculateCost(int baseCost, int difficulty, int proficiency)
        {
            return baseCost + (difficulty - proficiency);
        }
    }


    public interface IRandomNumberGenerator
    {
        int Next(int minValue, int maxValue);
    }
        public class RandomNumberGenerator : IRandomNumberGenerator
        {
            private Random random;
            //Pre-condition: None 
            /*Post-condition: Initializes the new instance of the RandomNumberGenerator class
             * with a new instance of the Random class. 
            */
            public RandomNumberGenerator()
            {
                random = new Random();
            }

            public int Next(int minValue, int maxValue)
            {
                return random.Next(minValue, maxValue);
            }
        }
        
        public class MockRandomNumberGenerator : IRandomNumberGenerator
        {
            private readonly int mockValue;
            //Pre-condition: None 
            /*Post-condition: Initializes a new instance of the MockRandomNumberGenerator class 
             * with the specified mock value.
            */
            public MockRandomNumberGenerator(int mockValue)
            {
                this.mockValue = mockValue;
            }
            //Pre-condition: None 
            /*Post-condition: Returns the mock value specified during object instantiation. 
            */
            public int Next(int minValue, int maxValue)
            {
                // Always returns the same mock value. 
                return mockValue;
            }
        }
        
        public class SubSkill : Skill
        {
            //Pre-condition: None
            /*Post-condition: Initializes a new instance of the SubSkill class.
             * The SubSkill inherits the construcotr of the base class Skill.
		     */
            public SubSkill(string name, string description) : base(name, description) { }

        //Pre-condition: None 
        /*Post-condition: Calculates and returns the cost of the SubSkill.
         * If proficiency is 0, throws an ArgumentException with a message indicating that proficiency cannot be zero.
         */
        public override int CalculateCost(int baseCost, int difficulty, int proficiency)
            {
                if (proficiency == 0)
                {
                    throw new ArgumentException("proficiency cannot be zero", nameof(proficiency));
                }
                return baseCost * (difficulty / proficiency);

            }
        }
       
        public class RandomizedSkill : Skill
        {
            private readonly IRandomNumberGenerator randomNumberGenerator;

        //Pre-condition: None.
        /*Post-condition: Initializes a new instance of the RandomizedSkill class. 
         * randomNumberGenerator is used to generate a random factor for cost calculation.
         * The RandomizedSkill inherits the constructor of the base class Skill.
         */
        public RandomizedSkill(string name, string description, IRandomNumberGenerator randomNumberGenerator) : base(name, description)
            {
                this.randomNumberGenerator = randomNumberGenerator;
            }
        //Pre-condition: None
        /*Post-condition: Calculates and returns the cost of the RandomizedSkill based on the provided parameters:
         * baseCost, difficulty, and proficiency. If proficiency is 0, throws an ArgumentException with a message indicating that proficiency cannot be zero.
         * Otherwise, generates a random factor between 0.8 and 1.2 using the randomNumberGenerator, and adjusts it to be at least 1.
         */
        public override int CalculateCost(int baseCost, int difficulty, int proficiency)
            {
                if (proficiency == 0)
                {
                    throw new ArgumentException("Proficiency cannot be zero", nameof(proficiency));
                }
                int randomFactor = randomNumberGenerator.Next(80, 120) / 100;

                randomFactor = Math.Max(randomFactor, 1);
                return baseCost * randomFactor * difficulty / proficiency;
            }
        }
        
        public class CombineSkill : Skill
        {
            private readonly RandomizedSkill randomizedSkilll;

        //Pre-condition: None
        /*Post-condition: Initializes a new instance of the CombineSkill class with the specified name, description, and 
         * a randomizedSkill.The CombineSkill inherits the constructor of the base class Skill.
         */
        public CombineSkill(string name, string description, RandomizedSkill randomizedSkill) : base(name, description)
            {
                this.randomizedSkilll = randomizedSkill;
            }

        //Pre-condition: None.
        /*Post-condition: Calculates and returns the cost of the CombineSkill based on the provided parameters.
         * 
         */
        public override int CalculateCost(int baseCost, int difficulty, int proficiency)
            {
                int combineSkillCost = randomizedSkilll.CalculateCost(baseCost, difficulty, proficiency);
                return baseCost * (difficulty + proficiency) + combineSkillCost;
            }
        }
 }


