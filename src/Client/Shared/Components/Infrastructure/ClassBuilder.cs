namespace Trailblazor.Client.Shared.Components
{
    public sealed class ClassBuilder
    {
        private readonly List<string> _classes = new();

        public string Build => string.Join('\u0020', _classes);

        public ClassBuilder()
        {
        }

        public ClassBuilder(params string[] startingClasses)
        {
            _classes.AddRange(startingClasses);
        }

        public ClassBuilder AddClass(string className)
        {
            if (!_classes.Contains(className))
                _classes.Add(className);

            return this;
        }

        public ClassBuilder RemoveClass(string className)
        {
            if (_classes.Contains(className))
                _classes.Remove(className);

            return this;
        }

        public ClassBuilder ToggleClass(string className)
        {
            if (_classes.Contains(className))
                _classes.Remove(className);
            else
                _classes.Add(className);

            return this;
        }
    }
}
