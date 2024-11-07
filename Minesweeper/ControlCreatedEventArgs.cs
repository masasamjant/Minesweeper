namespace Minesweeper
{
    /// <summary>
    /// Arguments for event notifying when new instance of <typeparamref name="T"/> control is created.
    /// </summary>
    /// <typeparam name="T">The type of the control.</typeparam>
    public sealed class ControlCreatedEventArgs<T> : EventArgs 
        where T : Control
    {
        /// <summary>
        /// Initializes new <see cref="ControlCreatedEventArgs{T}"/> instance.
        /// </summary>
        /// <param name="control">The created <typeparamref name="T"/> instance.</param>
        public ControlCreatedEventArgs(T control)
        {
            Control = control;
        }

        /// <summary>
        /// Gets the created <typeparamref name="T"/> instance.
        /// </summary>
        public T Control { get; }
    }
}
