namespace NationalsRegistration.Objects
{
    public class CategoryValidation
    {
        public string Name;

        private int MaxNumber
        {
            get
            {
                switch (Category)
                {
                    case Category.Rpg:
                        return 2;
                    case Category.Gauntlet:
                        return 1;
                    default:
                        return 3;
                }
            }
        }
        public int CurrentNumber;
        public Category Category;
        public bool Valid => CurrentNumber <= MaxNumber;
    }
}