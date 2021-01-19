using System;

namespace ReadWriteHandler.Menu
{
    public class MenuOption
    {
        public string Label { get; }
        public Action OnSelected { get; }

        public MenuOption(string label, Action onSelected)
        {
            Label = label;
            OnSelected = onSelected;
        }
    }
}