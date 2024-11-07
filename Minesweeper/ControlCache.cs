namespace Minesweeper
{
    /// <summary>
    /// Represents component that cache instances of <typeparamref name="T"/> controls. The purpose of this
    /// class is to reduce need to create new instances of <typeparamref name="T"/> and instead re-use existing instances.
    /// </summary>
    /// <typeparam name="T">The type of the control.</typeparam>
    public sealed class ControlCache<T> where T : Control, new()
    {
        private readonly List<T> controls = new List<T>();
        private int index = 0;

        /// <summary>
        /// Notifies when new control is created instead using cached one. 
        /// This is where created control should be initialized for properties that do not change when control is used.
        /// </summary>
        public event EventHandler<ControlCreatedEventArgs<T>>? ControlCreated;

        /// <summary>
        /// Notifies when control is disposing.
        /// </summary>
        public event EventHandler<ControlDisposingEventArgs<T>>? ControlDisposing;

        /// <summary>
        /// Gets  cached  instance of <typeparamref name="T"/> control or
        /// creates new instance and caches it, if no instance in cache.
        /// </summary>
        /// <returns>A instance of <typeparamref name="T"/> control</returns>
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

        /// <summary>
        /// Resets internal state.
        /// </summary>
        public void Reset()
        {
            index = 0;
        }

        /// <summary>
        /// Clears cache from all instances.
        /// </summary>
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

        /// <summary>
        /// Creates new instance of <typeparamref name="T"/> control.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="T"/> control.</returns>
        private T CreateControl()
        {
            var control = new T();
            ControlCreated?.Invoke(this, new ControlCreatedEventArgs<T>(control));
            return control;
        }
    }
}
