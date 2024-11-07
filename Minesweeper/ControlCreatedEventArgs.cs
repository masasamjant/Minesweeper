namespace Minesweeper
{
    public sealed class ControlCreatedEventArgs<T> : EventArgs 
        where T : Control
    {
        public ControlCreatedEventArgs(T control)
        {
            Control = control;
        }

        public T Control { get; }
    }
}
