namespace Minesweeper
{
    public sealed class ControlCache<T> where T : Control, new()
    {
        private readonly List<T> controls = new List<T>();
        private int index = 0;

        /// <summary>
        /// Notifies when new control is created instead using cached one. 
        /// This is where created control should be initialized for properties that do not change when control is used.
        /// </summary>
        public event EventHandler<ControlCreatedEventArgs<T>>? ControlCreated;

        public event EventHandler<ControlDisposingEventArgs<T>>? ControlDisposing;

        public T GetControl()
        {
            if (controls.Count == 0 || index == controls.Count)
            {
                var control = CreateControl();
                controls.Add(control);
                index++;
                return control;
            }
            else
            {
                var control = controls[index];
                index++;
                return control;
            }
        }

        public void Reset()
        {
            index = 0;
        }

        public void Clear()
        {
            if (controls.Count == 0)
                return;

            foreach (var control in controls)
            {
                ControlDisposing?.Invoke(this, new ControlDisposingEventArgs<T>(control));
                control.Dispose();
            }

            controls.Clear();
            index = 0;
        }

        private T CreateControl()
        {
            var control = new T();
            ControlCreated?.Invoke(this, new ControlCreatedEventArgs<T>(control));
            return control;
        }
    }
}
