namespace Minesweeper
{
    /// <summary>
    /// Arguments for event notifying when instance of <typeparamref name="T"/> control is about to be disposed.
    /// </summary>
    /// <typeparam name="T">The type of the control.</typeparam>
    public sealed class ControlDisposingEventArgs<T> : EventArgs
        where T : Control
    {
        /// <summary>
        /// Initializes new <see cref="ControlCreatedEventArgs{T}"/> instance.
        /// </summary>
        /// <param name="control">The instance of <typeparamref name="T"/> to be disposed.</param>
        public ControlDisposingEventArgs(T control)
        {
            Control = control;
        }

        /// <summary>
        /// Gets the instance of <typeparamref name="T"/> to be disposed.
        /// </summary>
        public T Control { get; }
    }
}
